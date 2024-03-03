using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unicorn
{
    public class Gold : MonoBehaviour
    {
        public Action OnTakeGold;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnTakeGold?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
