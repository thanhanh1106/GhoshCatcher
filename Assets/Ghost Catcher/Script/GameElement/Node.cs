using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Unicorn.GhostCather.GamePlay
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private BoxCollider2D boxCollider;
        [ReadOnly] public int Row;
        [ReadOnly] public int Column;
        public Image BlockImage;

        private List<(Block, Node)> dataShape; // list này lưu thông tin của những block tương ứng với node
        public Dictionary<(int, int), (Sprite, bool)> dataSpriteShape { get; private set; }// list này lưu thông tin của shape

        // trạng thái của node
        [ReadOnly] public bool IsFilled = false;
        public Ghost Ghost { get; set; }
        public ShapeInfo Shape { get; private set; } // tham chiếu đến hình dạng lắp lên node này

        public void Init(Vector2 size)
        {
            string[] nameSplit = transform.name.Split('-');
            if (nameSplit.Length >= 3)
            {
                Row = nameSplit[1].TryParseInt();
                Column = nameSplit[2].TryParseInt();
            }
            else Debug.Log("sai định dạng tên: " + transform.name);

            boxCollider.size = new Vector2(size.x, size.y);
        }


        public void SetHightLightImage(Sprite sprite)
        {
            BlockImage.sprite = sprite;
            BlockImage.color = new Color(1, 1, 1, 0.5f);
        }

        public void StopHighLighting()
        {
            BlockImage.sprite = null;
            BlockImage.color = new Color(1, 1, 1, 0);
        }

        public void SetBlockImage(Sprite sprite)
        {
            BlockImage.sprite = sprite;
            BlockImage.color = new Color(1, 1, 1, 1);
        }
        public void RemoveBlockImage()
        {
            BlockImage.sprite = null;
            BlockImage.color = new Color(1, 1, 1, 0);
        }
        public void ApplyNode((Block, Node) data, List<(Block, Node)> dataShape,ShapeInfo shapeReference)
        {
            this.dataShape = new List<(Block, Node)>(dataShape);
            dataSpriteShape = new Dictionary<(int, int), (Sprite, bool)>(data.Item1.dataShape);
            SetBlockImage(data.Item1.Sprite);
            IsFilled = true;
            Shape = shapeReference;

            // check xem đèn chiếu vào ma không ở đây
            bool isLitArea = data.Item1.IsLitArea;
            if (isLitArea && Ghost != null)
            {
                Ghost.SetBelowLit(true);
            }
        }
        public void RemoveShape()
        {
            if (dataShape == null) return;
            foreach(var item in dataShape)
            {
                item.Item2.RemoveNode();
            }
        }
        public void RemoveNode()
        {
            RemoveBlockImage();
            IsFilled = false;
            dataShape = null;
            if(Ghost != null)
            {
                Ghost.SetBelowLit(false);
            }
        }

    }
}
