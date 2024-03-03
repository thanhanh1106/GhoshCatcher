using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace Unicorn
{
    public class LoadingStartManager : MonoBehaviour
    {
        [SerializeField] CanvasGroup group;
        [SerializeField] private Image imgLoading;
        [SerializeField] private float timeLoading = 5;

        private AsyncOperation loadSceneAsync;
        private AppOpenAdManager appOpenAdManager;
        public static LoadingStartManager Instance { get; set; }

        [SerializeField] private PopupGDPR popupGDPR;

        private void Awake()
        {
            appOpenAdManager = AppOpenAdManager.Instance;
            Instance = this;
        }

        void Start()
        {
            // khi mà tắt Popup thank for playing thì gọi method khởi tạo
            popupGDPR.ActionClose = Init; 

            AppStateEventNotifier.AppStateChanged += OnAppStateChanged;

            // trường hợp người chơi vào game lần 2 thì tự khởi tạo
            if (popupGDPR.IsChecked())
            {
                Init();
            }
        }

        public void Init()
        {
            Debug.Log("Init Splash");
            UnicornAdManager.Init();
            LoadAppOpen();
            DontDestroyOnLoad(gameObject); // LoadingStartManager sẽ không bị hủy khi chuyển scenes
            LoadMasterLevel(); // Async nên dòng dưới vẫn được chạy
            RunLoadingBar(); // chạy thanh load ảo lòi
        }

        private void LoadAppOpen()
        {
#if UNITY_EDITOR
            return;
#endif
            MobileAds.Initialize(initStatus => { appOpenAdManager.LoadAd(); });
        }

        private void LoadMasterLevel()
        {
            loadSceneAsync = SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }

        // load 90% trong vòng 5s, 10% còn lại sẽ chạy tiếp khi hoàn thành loadScenes
        // load xong thì cho mờ dần và hủy luôn ảnh load 
        private void RunLoadingBar()
        {
            imgLoading.DOFillAmount(0.9f, timeLoading)
                .SetEase(Ease.OutQuint)
                .OnComplete(() => { StartCoroutine(Fade()); });
        }

        private IEnumerator Fade()
        {
            yield return new WaitUntil(() => loadSceneAsync.isDone);
            imgLoading.DOFillAmount(1f, 0.1f);
            group.DOFade(0, 0.2f)
                .OnComplete(() => { Destroy(group.gameObject); });
        }



        private void OnAppStateChanged(AppState state)
        {
            // Display the app open ad when the app is foregrounded.
            Debug.Log("App State is " + state);
            if (state == AppState.Foreground)
            {
                appOpenAdManager.ShowAdIfAvailable();
            }
        }

    }
}