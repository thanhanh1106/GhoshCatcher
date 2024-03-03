using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using Unicorn.GhostCather.Utility;

namespace Unicorn.GhostCather.GamePlay
{
    /// <summary>
    /// dùng để hiểm thị shape drag thay cho block, mục đích để dùng doScale không bị lỗi hàm check
    /// </summary>
    public class BlockDisplay : MonoBehaviour
    {

        [SerializeField] private RectTransform rect;
        [SerializeField] private Image img;
        private bool isFilled;

        public Sprite Sprite => img.sprite;
        public bool IsFilled => isFilled;

        public void SetBlock(Sprite sprite)
        {
            if (sprite == null)
            {
                img.color = new Color(1, 1, 1, 0);
                img.sprite = null;
                isFilled = false;
            }
            else
            {
                img.color = new Color(1, 1, 1, 1);
                img.sprite = sprite;
                isFilled = true;
            }

        }
        public void Show()
        {
            if (isFilled) img.color = new Color(1, 1, 1, 1);
            else img.color = new Color(1, 1, 1, 0);
        }
        public void Hide() => img.color = new Color(1, 1, 1, 0);


    }
}
