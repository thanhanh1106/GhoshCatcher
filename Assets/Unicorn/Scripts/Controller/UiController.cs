using Unicorn.GhostCather.UI;
using Unicorn.UI.Shop;
using UnityEngine;

namespace Unicorn.UI
{
    public class UiController : MonoBehaviour
    {
        public ShopCharacter ShopCharacter;
        public PopupRewardEndGame PopupRewardEndGame;
        public PopupChestKey PopupChestKey;
        public LuckyWheel LuckyWheel;
        public GameObject Loading;

        public Ui_Lobby UiLobby;
        public Ui_ChooseLevel UiChooseLevel;
        public Ui_Ingame UiInGame;
        public Ui_Win UiWin;
        public Ui_Lose UiLose;

        public void Init()
        {
            ShopCharacter.Configure(
                PlayerDataManager.Instance,
                PlayerDataManager.Instance.DataTextureSkin);

            UiChooseLevel.Initialized(GameManager.Instance.DataLevels);
        }

        public void OpenUiLose() => UiLose.Show();

        public void OpenUiWin(int starNunber)
        {
            UiWin.Initialized(starNunber);
            UiWin.Show();
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void OpenPopupReward(RewardEndGame reward, TypeDialogReward type)
        {
            if (PopupRewardEndGame.IsShow)
                return;

            PopupRewardEndGame.Show(true);
            PopupRewardEndGame.Init(reward, type);
        }


        public void OpenLuckyWheel()
        {
            LuckyWheel.Show(true);
        }

        public void OpenLoading(bool isLoading)
        {
            Loading.SetActive(isLoading);
        }
    }
}

