using Common.FSM;
using System.Collections;
using System.Collections.Generic;
using Unicorn.FSM;
using UnityEngine;

namespace Unicorn
{
    public class ChooseLevelAction : UnicornFSMAction
    {
        public ChooseLevelAction(GameManager gameManager, FSMState owner) : base(gameManager, owner)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            GameManager.UiController.UiChooseLevel.Show();
        }
        public override void OnExit()
        {
            base.OnExit();
            GameManager.UiController.UiChooseLevel.Hide();
        }
    }
}
