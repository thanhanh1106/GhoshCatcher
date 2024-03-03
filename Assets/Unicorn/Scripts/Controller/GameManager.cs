
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using Unicorn.GhostCather;
using Unicorn.GhostCather.Data;
using Unicorn.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

/***********************************************************


        ╭╮ ╭╮╭━╮ ╭╮╭━━╮╭━━━╮╭━━━╮╭━━━╮╭━╮ ╭╮
        ┃┃ ┃┃┃┃╰╮┃┃╰┫┣╯┃╭━╮┃┃╭━╮┃┃╭━╮┃┃┃╰╮┃┃
        ┃┃ ┃┃┃╭╮╰╯┃ ┃┃ ┃┃ ╰╯┃┃ ┃┃┃╰━╯┃┃╭╮╰╯┃
        ┃┃ ┃┃┃┃╰╮┃┃ ┃┃ ┃┃ ╭╮┃┃ ┃┃┃╭╮╭╯┃┃╰╮┃┃
        ┃╰━╯┃┃┃ ┃┃┃╭┫┣╮┃╰━╯┃┃╰━╯┃┃┃┃╰╮┃┃ ┃┃┃
        ╰━━━╯╰╯ ╰━╯╰━━╯╰━━━╯╰━━━╯╰╯╰━╯╰╯ ╰━╯
        ╭━━━━┳━━━╮╭━━━━┳╮ ╭┳━━━╮╭━━━━┳━━━┳━━━╮
        ┃╭╮╭╮┃╭━╮┃┃╭╮╭╮┃┃ ┃┃╭━━╯┃╭╮╭╮┃╭━╮┃╭━╮┃
        ╰╯┃┃╰┫┃ ┃┃╰╯┃┃╰┫╰━╯┃╰━━╮╰╯┃┃╰┫┃ ┃┃╰━╯┃
          ┃┃ ┃┃ ┃┃  ┃┃ ┃╭━╮┃╭━━╯  ┃┃ ┃┃ ┃┃╭━━╯
          ┃┃ ┃╰━╯┃  ┃┃ ┃┃ ┃┃╰━━╮  ┃┃ ┃╰━╯┃┃
          ╰╯ ╰━━━╯  ╰╯ ╰╯ ╰┻━━━╯  ╰╯ ╰━━━┻╯
                                            
                                                            
                                         .^^                
                                       ^?J~                 
                      :J5YJ?7~~!7?7.:7Y57.                  
                 .^7JY55P5YJ?J5GBP?YP5?:                    
             .~?5PPYJ?777!!77??7?YBBY~                      
          .!YGBGY?!!777!!!777777~??Y57                      
         ^?JP#GY?7777!!^~7777777~77?JPJ.                    
          :YGY7!777!^J5:!777777777???7JY!                   
         7BPYJ7777~:5#B~:~!!!!77777777!7YY!.                
        ?#GGG7777!.J#B#BY77777777!!!!777!7YP7.              
       !#BG#?!777:^B#BB###?!!!!!!!!7?7!7777YY5              
      .GP:YG7777!:~BBBBB#G          .!?!!!77YP              
      ^5. 5P7J77!:^B#BBB#B.           ^^~77!~^              
      ..  YGYG777^.J#BBBB#?                                 
          ~BG#Y!7!::5##BBB#?                   :.           
           Y###Y77!^:JB##B##P~               ^!:            
           .PPJBG?77~:~YB#####PJ~:.     .:^7J?.             
            .? :YGPJ7!~^^75GB####BGPP555PP5?:               
                 :75P5Y?7!~~!7?JY55555YY?~.                 
                    .~7JY5YYYJJJ????7!^.                    
                         ..:^^^^::.                                 

 ***********************************************************/

namespace Unicorn
{
    /// <summary>
    /// Quản lý load scene và game state
    /// </summary>
    public partial class GameManager : SerializedMonoBehaviour
    {

        public static GameManager Instance;

        [Space]
        [BoxGroup("Level")]
        [SerializeField] private LevelConstraint levelConstraint;


        [FoldoutGroup("Persistant Component", false)]
        [SerializeField] private UiController uiController;
        [FoldoutGroup("Persistant Component")]
        [SerializeField] private CameraController mainCamera;

        [FoldoutGroup("Persistant Component")]
        [SerializeField] private IapController iap;


        private LevelManager currentLevelManager;

        private IDataLevel dataLevel;

        public event Action GamePaused;
        public event Action GameResumed;

        public bool IsLevelLoading { get; private set; }

        public ILevelInfo DataLevel => dataLevel;
        public int CurrentLevel => DataLevel.GetCurrentLevel();
        public GameFSM GameStateController { get; private set; }
        public PlayerDataManager PlayerDataManager => PlayerDataManager.Instance;
        public CameraController MainCamera => mainCamera;
        public UiController UiController => uiController;
        public LevelManager LevelManager
        {
            get => currentLevelManager;
            private set => currentLevelManager = value;
        }
        public IapController IapController => iap;
        public Profile Profile { get; private set; }

        #region Thanh anh
        [SerializeField] private List<DataLevelSO> dataLevelSOs;

        public List<DataLevelSO> DataLevelSOs => dataLevelSOs;

        public List<DataLevel> DataLevels { get; private set; }
        private LevelGameManager levelGameManager;
        public LevelGameManager LevelGameManager => levelGameManager;

        public bool FistTimeEnterGame { get; private set; }


        #region Liên quan đến level và data hiện tại
        public int CurrentLevelChoose { get; private set; }
        public DataLevel CurentDataLevel { get => DataLevels[CurrentLevelChoose - 1]; }
        public DataLevel NextDataLevel
        {
            get
            {
                if(CurrentLevelChoose >= DataLevels.Count) return null;
                return DataLevels[CurrentLevelChoose];
            }
        }
        public DataLevelSO CurrentDataLevelSO { get => dataLevelSOs[CurrentLevelChoose - 1]; }
        #endregion
        #endregion

        private void Awake()
        {
            Instance = this;
            GameStateController = new GameFSM(this);
            Profile = new Profile();

            DOTween.Init().SetCapacity(200, 125);
#if FINAL_BUILD
            Debug.unityLogger.logEnabled = false;
#endif

            dataLevel = PlayerDataManager.GetDataLevel(levelConstraint);
            dataLevel.LevelConstraint = levelConstraint;
        }

        private void Start()
        {
            FistTimeEnterGame = !PlayerDataManager.IsEnteredGame;
            LoadDataLevel();
            UiController.Init();
            GameStateController.ChangeState(GameState.LOBBY);
            //LoadLevel();
        }

        /// <summary>
        /// Load level mới và xóa level hiện tại
        /// </summary>
        public void LoadLevel()
        {
            int buildIndex = dataLevel.GetBuildIndex();

#if UNITY_EDITOR
            buildIndex = GetForcedBuildIndex(buildIndex);
#endif

            bool isBuildIndexValid = buildIndex > gameObject.scene.buildIndex
                                     && buildIndex < SceneManager.sceneCountInBuildSettings;
            if (!isBuildIndexValid)
            {
                Debug.LogError("No valid scene is found! \nFailed build index: " + buildIndex);
                GameStateController.ChangeState(GameState.LOBBY);
                return;
            }

            IsLevelLoading = true;
            if (CurrentLevel != 0 && SceneManager.sceneCount != 1)
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));

            SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
            uiController.OpenLoading(true);
        }


        // load scene theo index truyền vào
        public void LoadScene(int buildIndex)
        {
            bool isBuildIndexValid = buildIndex > gameObject.scene.buildIndex
                                     && buildIndex < SceneManager.sceneCountInBuildSettings;
            if (!isBuildIndexValid)
            {
                Debug.LogError("No valid scene is found! \nFailed build index: " + buildIndex);
                GameStateController.ChangeState(GameState.LOBBY);
                return;
            }

            IsLevelLoading = true;
            if (CurrentLevel != 0 && SceneManager.sceneCount != 1)
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));

            SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
            uiController.OpenLoading(true);
        }


        /// <summary>
        /// Đưa game về state Lobby và khởi tạo lại các giá trị cần thiết cho mỗi level mới.
        /// <remarks>
        /// LevelManager khi đã load xong thì PHẢI gọi hàm này.
        /// </remarks>
        /// </summary>
        /// <param name="levelManager"></param>
        public void RegisterLevelManager(LevelManager levelManager)
        {
            LevelManager = levelManager;
            GameStateController.ChangeState(GameState.IN_GAME);
            uiController.OpenLoading(false);
            IsLevelLoading = false;
        }

        /// <summary>
        /// Bắt đầu level, đưa game vào state <see cref="GameState.IN_GAME"/>
        /// </summary>
        public void StartLevel()
        {
            Analytics.LogTapToPlay();
            GameStateController.ChangeState(GameState.IN_GAME);
        }

        /// <summary>
        /// Kết thúc game sau một khoảng thời gian
        /// </summary>
        /// <param name="result"></param>
        /// <param name="delayTime"></param>
        public void DelayedEndgame(LevelResult result, float delayTime = 0.5f)
        {
            StartCoroutine(DelayedEndgameCoroutine(result, delayTime));
        }

        private IEnumerator DelayedEndgameCoroutine(LevelResult result, float delayTime)
        {
            yield return Yielders.Get(delayTime);
            EndLevel(result);
        }

        /// <summary>
        /// Kết thúc game
        /// </summary>
        /// <param name="result"></param>
        public void EndLevel(LevelResult result)
        {
            GameStateController.ChangeState(GameState.END_GAME);

            if (result == LevelResult.Win)
            {
                IncreaseLevel();
            }

        }

        /// <summary>
        /// Tăng level
        /// </summary>
        public void IncreaseLevel()
        {
            CurentDataLevel.StarNumber = levelGameManager.Data.Step.GetStar();
            if (NextDataLevel != null) NextDataLevel.IsLocked = false;
        }


        /// <summary>
        /// Hồi sinh
        /// </summary>
        public void Revive()
        {
            LevelManager.ResetLevelState();
            // TODO: Revive code
        }

        public void Pause()
        {
            Time.timeScale = 0;
            GamePaused?.Invoke();
        }

        public void Resume()
        {
            Time.timeScale = 1;
            GameResumed?.Invoke();
        }

        private void Update()
        {
            if (!IsLevelLoading)
                GameStateController.Update();
        }

        private void FixedUpdate()
        {
            if (!IsLevelLoading)
                GameStateController.FixedUpdate();
        }

        private void LateUpdate()
        {
            if (!IsLevelLoading)
                GameStateController.LateUpdate();
        }

        private void OnApplicationQuit()
        {
            PlayerDataManager.SaveDataLevel(DataLevels);
        }

        #region Thanh anh
        /// <summary>
        /// gọi ở start của LevelGameManager để lấy tham chiếu
        /// </summary>
        public void RegisterLevelGameManager(LevelGameManager levelGameManager)
        {
            this.levelGameManager = levelGameManager;
            levelGameManager.Data = CurrentDataLevelSO;

        }
        public void ChooseLevel(int Level)
        {
            Analytics.LogTapToPlay();
            CurrentLevelChoose = Level;
            LoadScene(2); // load lại scene

        }

        public void Replay()
        {
            ChooseLevel(CurrentLevelChoose);
        }
        public void NextLevel()
        {
            CurrentLevelChoose++;
            if(CurrentLevelChoose >= dataLevelSOs.Count)
            {
                int levelRand;
                do
                {
                    levelRand = UnityEngine.Random.Range(1, dataLevelSOs.Count + 1);
                }
                while (levelRand == CurrentLevelChoose && dataLevelSOs.Count > 1);
                CurrentLevelChoose = levelRand;
            }
            Analytics.LogTapToPlay();
            LoadScene(2);
        }

        public void BackHome()
        {
            GameStateController.ChangeState(GameState.LOBBY);
        }

        public void LoadDataLevel()
        {
            if (FistTimeEnterGame)
            {
                DataLevels = new List<DataLevel>();
                for(int index = 0; index < dataLevelSOs.Count; index++)
                {
                    DataLevel data = new DataLevel(index + 1, true);
                    if (index == 0) data.IsLocked = false;
                    DataLevels.Add(data);
                }
                PlayerDataManager.SaveDataLevel(DataLevels);
            }
            else
            {
                DataLevels = PlayerDataManager.GetDataLevel();
            }
        }

        [Button("Unlock All Level")]
        public void UnloclAllLevel()
        {
            foreach (var item in DataLevels)
            {
                item.IsLocked = false;
            }
        }
        #endregion
    }
}

