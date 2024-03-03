using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather.Data;
using Unicorn.GhostCather.GamePlay;
using Unicorn.GhostCather.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Unicorn.GhostCather
{
    public class LevelGameManager : LevelManager
    {
        public DataLevelSO Data;
        [FoldoutGroup("Reference")] public GamePlay.Grid Grid;
        [FoldoutGroup("Reference")] public ShapeDrag ShapeDrag;
        [FoldoutGroup("Reference")] public ShapePanel ShapePanel;

        [SerializeField, FoldoutGroup("Temporary")] private Image bgTable;

        public new static LevelGameManager Instance => LevelManager.Instance as LevelGameManager;
        public int Step
        {
            get => Data.Step.CurrentStep;
            set => Data.Step.CurrentStep = value;
        }

        private GameManager gameManager => GameManager.Instance;

        protected override void Start()
        {
            base.Start();
            GameManager.Instance.RegisterLevelGameManager(this);
            GameAction.AddListenerDeleteShapeInPanel(CheckWin);
            Data.Step.Init();
            bgTable.sprite = LoadBackGround();
            StartLevel();
        }
        public override void StartLevel()
        {
            Grid.Init();
            ShapePanel.Init();
            ShapeDrag.Init(Grid.CellSize, ShapePanel.Shapes[0].ShapeInfo.Size);
            gameManager.UiController.UiInGame.SetData(gameManager.CurrentLevelChoose, Data.Step);
        }
        public override void ExitLevel()
        {

        }

        private Sprite LoadBackGround()
        {
            Sprite sprite;
            sprite = Data.BackGroundTable;
            if(sprite == null) sprite = GameUtility.LoadResources<Sprite>($"Ingame/BackGroundTable/{gameManager.CurrentLevelChoose}");
            if (sprite == null) sprite = GameUtility.LoadResources<Sprite>($"Ingame/BackGroundTable/{GameUtility.RandomInt(1,12)}"); // 12 => số lượng bg hiện có trong thư mục
            return sprite;
        }
        private void CheckWin()
        {
            bool isWin = false;
            List<Ghost> ghosts = Grid.Ghosts;
            foreach (Ghost ghost in ghosts)
            {
                if (ghost.IsBelowLit) isWin = true;
                else
                {
                    isWin = false;
                    break;
                }
            }
            if (isWin)
            {
                List<Shape> shapes = ShapePanel.Shapes;
                foreach (var shape in shapes)
                {
                    if (shape.gameObject.activeSelf)
                    {
                        isWin = false;
                        break;
                    }
                }
            }
            if (isWin)
            {
                EndGame(LevelResult.Win);
            }
        }

        // khi người chơi thao tác làm mất 1 step
        public void OnStep()
        {
            Step--;
            gameManager.UiController.UiInGame.SetStep(Step);
            if (Step <= 0) EndGame(LevelResult.Lose);
        }
    }
}
