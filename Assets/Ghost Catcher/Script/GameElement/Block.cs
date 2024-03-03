using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Unicorn.GhostCather.GamePlay
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private BlockDisplay display;
        [ReadOnly] public int Row;
        [ReadOnly] public int Column;

        public RectTransform rectTransform => rect;
        public Sprite Sprite => display.Sprite;
        public bool IsFilled => display.IsFilled;
        public bool IsLitArea { get;private set; }
        public Dictionary<(int, int), (Sprite, bool)> dataShape;

        public void Init()
        {
            string[] nameSplit = transform.name.Split('-');
            if (nameSplit.Length >= 3)
            {
                Row = nameSplit[1].TryParseInt();
                Column = nameSplit[2].TryParseInt();
            }
            else Debug.Log("sai định dạng tên: " + transform.name);
        }
        
        public void SetBlock(Dictionary<(int, int), (Sprite,bool)> dataShape)
        {
            this.dataShape = dataShape;
            Sprite sprite = dataShape[(Row, Column)].Item1;
            IsLitArea = dataShape[(Row, Column)].Item2;
            display.SetBlock(sprite);
        }
        public void Show() => display.Show();
        public void Hide() => display.Hide();
    }
}
