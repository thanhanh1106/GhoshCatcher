using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

namespace Unicorn.GhostCather.Utility
{
    public static class GameAction
    {

        // bắt tất cả sự kiện bấm button trên ui trong game
        private static event Action<Button,Action> clickButton;
        public static void OnClickButton(this Button btn,Action callBack = null) => clickButton?.Invoke(btn,callBack);
        public static void AddListenerClickButton(Action<Button, Action> action) => clickButton += action;
        public static void RemoveListenerClickButton(Action<Button, Action> action) => clickButton -= action;


        private static Action deleteShapeInPanel;
        public static void OnDeleteShapeInPanel() => deleteShapeInPanel.Invoke();
        public static void AddListenerDeleteShapeInPanel(Action action) => deleteShapeInPanel = action; 

    }
}
