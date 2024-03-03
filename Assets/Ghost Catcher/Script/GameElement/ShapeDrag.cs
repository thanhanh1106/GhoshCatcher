
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unicorn.GhostCather.GamePlay
{
    public class ShapeDrag : MonoBehaviour
    {
        [SerializeField] private Vector2 offset;
        private RectTransform rectTransform;
        [SerializeField] private List<Block> blocks;
        [SerializeField] private RectTransform displayRect;
        [SerializeField] private GridLayoutGroup gridLayoutBlocks;
        [SerializeField] private GridLayoutGroup gridLayoutDisplays;

        [SerializeField] private float durationDoScale = 0.25f;
        [SerializeField] private Ease ease = Ease.Linear;

        public ShapeInfo CurrentShapeInfo { get; private set; }

        private Vector3 shapeInfoScale;
        public void Init(float cellSize,float shapeInfoSize)
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(cellSize, cellSize) * 2;
            foreach (var block in blocks)
            {
                block.Init();
            }
            gridLayoutBlocks.cellSize = new Vector2(cellSize,cellSize);
            gridLayoutDisplays.cellSize = new Vector2(cellSize,cellSize);

            float scale = shapeInfoSize/ rectTransform.rect.width; // scale so với shape info
            shapeInfoScale = new Vector3(scale, scale, scale);
        }

        public void SetPosition(Vector3 position,bool plusOffset = false)
        {
            if (plusOffset)
            {
                rectTransform.position = new Vector3(position.x + offset.x,position.y + offset.y,position.z);
            }
            else
            {
                rectTransform.position = position;
            }
        }
        public void SetData(Dictionary<(int, int), (Sprite, bool)> dataShape, ShapeInfo shapeInfo)
        {
            SetDataBlocks(dataShape);
            CurrentShapeInfo = shapeInfo;
        }

        private void SetDataBlocks(Dictionary<(int, int), (Sprite, bool)> dataShape)
        {
            foreach (var block in blocks)
            {
                block.SetBlock(dataShape);
            }
        }
        
        public void CheckCanPlaceShape()
        {
            List<(Block,Node)> nodes = new List<(Block, Node)>(); // list lưu theo cặp block và node được block đó đè lên
            bool canPlace = false;
            foreach(Block block in blocks)
            {
                if (!block.IsFilled) continue; // nếu block không có gì thì bỏ qua
                RaycastHit2D hit = Physics2D.Raycast(block.rectTransform.position,Vector2.zero);
                if (hit && hit.collider.CompareTag("Node"))
                {
                    canPlace = true;

                    Node node = hit.transform.GetComponent<Node>();
                    if (node.IsFilled) // nếu 1 node đã đầy thì đánh dấu là không có chỗ trống và thoát luôn vòng lặp
                    {
                        canPlace = false;
                        break;
                    }
                    nodes.Add((block,node)); 
                }
                else
                {
                    canPlace = false;
                    break;
                }
            }
            if (canPlace) LevelGameManager.Instance.Grid.HightLingtNodeCanPlace(nodes);
            else LevelGameManager.Instance.Grid.StopHightLightNodeCantPlace();
        }
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void DoScale()
        {
            displayRect.localScale = shapeInfoScale;
            displayRect.DOScale(1, durationDoScale).SetEase(ease);
        }
        public void DoScaleAndMoveBackwards(Vector3 position, Action callBack)
        {
            displayRect.localScale = Vector3.one;
            displayRect.DOScale(shapeInfoScale.x, 0.2f).SetEase(ease);
            rectTransform.DOMove(position,0.2f).SetEase(ease).OnComplete(() => callBack?.Invoke());
        }
    }
}
