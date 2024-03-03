using Common.FSM;
using UnityEngine;

namespace Unicorn.FSM
{
    public class LobbyAction : UnicornFSMAction
    {
        public LobbyAction(GameManager gameManager, FSMState owner) : base(gameManager, owner)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            GameManager.UiController.UiLobby.Show();
            SoundManager.Instance.PlayFxSound(soundEnum: SoundManager.GameSound.Lobby);
        }

        public override void OnUpdate()
        {

            base.OnUpdate();

        }

        public override void OnExit()
        {
            base.OnExit();
            GameManager.UiController.UiLobby.Hide();
            SoundManager.Instance.StopSound(SoundManager.GameSound.Lobby);
        }


    }
}