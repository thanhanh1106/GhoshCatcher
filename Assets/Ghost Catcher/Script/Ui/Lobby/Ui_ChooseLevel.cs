using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather.Utility;
using Unicorn.GhostCather.Data;
using UnityEngine;
using UnityEngine.UI;
using Unicorn.GhostCather.UI;

namespace Unicorn
{
    public class Ui_ChooseLevel : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Button")] private Button btnBackToLobby;

        [SerializeField, FoldoutGroup("RectTransform")] private RectTransform contentRect;

        private List<Ui_PackLevel> levelList;
        private List<DataLevel> dataLevels;

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void Start()
        {
            btnBackToLobby.onClick.AddListener(ClickBtnBackLobby);
        }

        public void Initialized(List<DataLevel> dataLevels)
        {
            this.dataLevels = dataLevels;
            levelList = new List<Ui_PackLevel>();
            foreach (DataLevel data in dataLevels)
            {
                Ui_PackLevel levelPack = Instantiate(PrefabStorage.Instance.packLevel,contentRect);
                levelPack.Initialized(data);
                levelPack.OnChooseLevel = HandlerChooseLevel; 
                levelList.Add(levelPack);
            }
        }

        private void HandlerChooseLevel(DataLevel data)
        {
            GameManager.Instance.ChooseLevel(data.Level);
            //Debug.Log("choose level " + data.Level);
        }

        private void ClickBtnBackLobby()
        {
            GameAction.OnClickButton(btnBackToLobby);
            SoundManager.Instance.PlaySoundButton();
            GameManager.Instance.GameStateController.ChangeState(GameState.LOBBY);
        }
    }
}
