using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unicorn.GhostCather.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Unicorn.GhostCather.UI
{
    public class Ui_Lobby : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Button")] private Button btnSetting;
        [SerializeField, FoldoutGroup("Button")] private Button btnPlay;

        public void Show() => transform.gameObject.SetActive(true);
        public void Hide() => transform.gameObject.SetActive(false);

        private void Start()
        {
            btnSetting.onClick.AddListener(ClickBtnSetting);
            btnPlay.onClick.AddListener(ClickBtnPlay);
        }

        private void ClickBtnPlay()
        {
            GameAction.OnClickButton(btnPlay);
            SoundManager.Instance.PlaySoundButton();
            GameManager.Instance.GameStateController.ChangeState(GameState.CHOOSE_LEVEL);
        }

        private void ClickBtnSetting()
        {
            GameAction.OnClickButton(btnSetting);
            SoundManager.Instance.PlaySoundButton();
        }
    }
}
