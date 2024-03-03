using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unicorn.GhostCather.Data
{
    /// <summary>
    /// class lưu thông tin của level và để lưu vào es3
    /// </summary>
    [System.Serializable]
    public class DataLevel
    {
        public int Level;
        [SerializeField]
        private bool isLocked;
        [SerializeField]
        private int starNumber;

        public DataLevel(int level, bool isLocked)
        {
            Level = level;
            this.isLocked = isLocked;
        }
        
        public bool IsLocked
        {
            get { return isLocked; }
            set
            {
                isLocked = value;
                OnValueChanged?.Invoke();
            }
        }

        /// <summary>
        /// số sao mới được thêm vào sẽ không nhỏ hơn số sao đã được lưu sẵn 
        /// </summary>
        public int StarNumber
        {
            get => isLocked ? 0 : starNumber;
            set
            {
                if(value > starNumber) 
                {
                    starNumber = value;
                    if (!isLocked) OnValueChanged?.Invoke();
                }

                
            }
        }

        public Action OnValueChanged;
    }
}
