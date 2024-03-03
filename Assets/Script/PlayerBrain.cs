using System;
using System.Collections;
using System.Collections.Generic;
using Unicorn.Examples;
using UnityEngine;

namespace Unicorn
{
    public class PlayerBrain : MonoBehaviour
    {
        public List<GameObject> Golds;
        int goldsCount;
        private void Start()
        {
            goldsCount = Golds.Count;
            foreach (GameObject g in Golds)
            {
                g.GetComponent<Gold>().OnTakeGold = HandlerTakeGold;
            }
        }

        private void HandlerTakeGold()
        {
            goldsCount--;
            if(goldsCount <= 0) 
            {
                LobbyManager.Instance.EndGameCall();              
            }
        }
    }
}
