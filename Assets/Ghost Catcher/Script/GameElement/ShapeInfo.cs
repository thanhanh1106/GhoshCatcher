using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unicorn.GhostCather.Utility;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unicorn.GhostCather;
using Sirenix.OdinInspector;

namespace Unicorn.GhostCather.GamePlay
{
    public class ShapeInfo : SerializedMonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
    {
        public int ShapeID = 0;

        private LevelGameManager levelManager => LevelGameManager.Instance;

        [SerializeField] private List<Block> blocksInShape;
        private Transform parentTrans;
        // list lưu 4 hướng của shape đã được cắt ra thành các sprite nhỏ hơn có ma trận là 2x2 
        // 4 hướng được quy định bởi index trong list tương đương với 0 là hướng gốc, các hướng tăng dần sẽ được xoay theo chiều kim đồng hồ
        // biến bool chỉ ra block nào là vùng được chiếu sáng
        [SerializeField] private List<Dictionary<(int, int),(Sprite,bool)>> shapesSprites = new List<Dictionary<(int, int), (Sprite,bool)>>();

        private int currentDirection;

        public float Size { get; private set; }

        private void Start()
        {
            parentTrans = transform.parent;
            currentDirection = 0;
            foreach(Block block in blocksInShape)
            {
                block.Init();
                block.SetBlock(shapesSprites[currentDirection]);
            }
        }

        public void Initialization()
        {
            Size = transform.GetComponent<RectTransform>().rect.width;
        }

        #region Interface Method
        public void OnPointerClick(PointerEventData eventData)
        {
            currentDirection++;
            if (currentDirection >= blocksInShape.Count) currentDirection = 0;
            foreach (Block block in blocksInShape)
            {
                block.SetBlock(shapesSprites[currentDirection]);
            }
            levelManager.OnStep();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            levelManager.ShapeDrag.Show();
            levelManager.ShapeDrag.SetPosition(eventData.position);
            levelManager.ShapeDrag.SetData(shapesSprites[currentDirection], this);
            levelManager.ShapeDrag.DoScale();
            Hide();
        }

        public void OnDrag(PointerEventData eventData)
        {
            levelManager.ShapeDrag.SetPosition(Input.mousePosition,true);
            levelManager.ShapeDrag.CheckCanPlaceShape();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            bool applySuccess = levelManager.Grid.ApplyShapeToNode(levelManager.ShapeDrag.CurrentShapeInfo);
            if(applySuccess) ApplySuccess();
            else levelManager.ShapeDrag.DoScaleAndMoveBackwards(transform.position, () =>
            {
                Show();
                levelManager.ShapeDrag.Hide();
            });
        }
        #endregion

        // tạm ẩn shape trên panel đi
        private void Hide()
        {
            foreach(Block block in blocksInShape)
            {
                block.Hide();
            }
        }
        // bật shape trên panel
        private void Show()
        {
            foreach(Block block in blocksInShape)
            {
                block.Show();
            }
        }
        // thực hiện xóa shape trên panel
        private void ApplySuccess()
        {
            levelManager.ShapeDrag.Hide();
            transform.parent.gameObject.SetActive(false);
            GameAction.OnDeleteShapeInPanel();
        }
        // cho shape quay trở lại panel khi không lắp được vào bảng
        public void Return()
        {
            levelManager.ShapeDrag.DoScaleAndMoveBackwards(transform.position, () =>
            {
                transform.parent.gameObject.SetActive(true);
                Show();
                levelManager.ShapeDrag.Hide();
            });
        }
    }
}
