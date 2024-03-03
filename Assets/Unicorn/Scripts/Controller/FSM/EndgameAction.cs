using Common.FSM;
using Unicorn.Utilities;
using UnityEngine;
using System.Collections;

namespace Unicorn.FSM
{
    public class EndgameAction : UnicornFSMAction
    {
        public EndgameAction(GameManager gameManager, FSMState owner) : base(gameManager, owner)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            ProcessWinLose();
        }

        private void ProcessWinLose()
        {
            Debug.Log("CurrentLevel_" + GameManager.Instance.CurrentLevel);

            switch (GameManager.LevelManager.Result)
            {
                case LevelResult.Win:
                    int star = GameManager.LevelGameManager.Data.Step.GetStar();
                    GameManager.UiController.OpenUiWin(star);
                    Analytics.LogEndGameWin(GameManager.Instance.CurrentLevel);
                    break;


                case LevelResult.Lose:
                    GameManager.UiController.OpenUiLose();
                    Analytics.LogEndGameLose(GameManager.Instance.CurrentLevel);
                    break;
                default:
                    break;
            }

            GameManager.Instance.StartCoroutine(IEShowInter());
        }

        private IEnumerator IEShowInter()
        {
            yield return new WaitForSeconds(0.4f);

            switch (GameManager.LevelManager.Result)
            {
                case LevelResult.Win:
                    UnicornAdManager.ShowInterstitial(Helper.inter_end_game_win);
                    break;
                case LevelResult.Lose:
                    UnicornAdManager.ShowInterstitial(Helper.inter_end_game_lose);
                    break;
                default:
                    break;
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            GameManager.UiController.UiInGame.Hide();
            SoundManager.Instance.StopSound(GameManager.LevelManager.Result);
        }
    }
}