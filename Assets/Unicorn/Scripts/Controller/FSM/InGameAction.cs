using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using Unicorn.Examples;
using UnityEngine;

namespace Unicorn.FSM
{
    public class InGameAction : UnicornFSMAction
    {
        
        public InGameAction(GameManager gameManager, FSMState owner) : base(gameManager, owner)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Enter ingame");
            base.OnEnter();
            GameManager.UiController.UiInGame.Show();
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnExit()
        {
            base.OnExit();
            GameManager.UiController.UiInGame.Hide();
        }
    }
}