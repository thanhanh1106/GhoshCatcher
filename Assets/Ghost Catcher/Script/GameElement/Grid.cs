using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather;
using Unicorn.GhostCather.Data;
using Unicorn.GhostCather.GamePlay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unicorn.GhostCather.GamePlay
{
    public class Grid : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform rect;
        [SerializeField] private GridLayoutGroup layoutGroup;
        [SerializeField] private int rowCount;
        [SerializeField] private int columnCount;
        [SerializeField] private List<Node> nodes;

        private float cellSize;
        public List<Node> Nodes => nodes;
        public float CellSize => cellSize;

        public List<(Block,Node)> currentNodeHighLingt;
        public List<Ghost> Ghosts { get; private set; }

        #region Other Reference
        private ShapeDrag shapeDrag => LevelGameManager.Instance.ShapeDrag;
        private DataLevelSO dataLevel => LevelGameManager.Instance.Data;
        #endregion

        private bool isDragging;
        private void Start()
        {
            currentNodeHighLingt = new List<(Block,Node)>();
        }

        public void Init()
        {
            // reponsive
            float cellSizeX = rect.rect.width / rowCount;
            float cellSizeY = rect.rect.height / columnCount;
            if (cellSizeX < cellSizeY)
            {
                cellSize = cellSizeX;
                Vector2 size = new Vector2(cellSizeX, cellSizeX);
                layoutGroup.cellSize = size;
                foreach (Node block in nodes) block.Init(size);
            }
            else
            {
                cellSize = cellSizeY;
                Vector2 size = new Vector2(cellSizeY, cellSizeY);
                layoutGroup.cellSize = size;
                foreach (Node block in nodes) block.Init(size);
            }
            InitGhost();
        }

        private void InitGhost()
        {
            bool[,] ghostMatrix = dataLevel.GhostsMatrix;
            List<Ghost> ghostsPrefab = PrefabStorage.Instance.Ghosts;
            Ghosts = new List<Ghost>();
            foreach(Node node in nodes)
            {
                //node.IsGhost = ghostMatrix[node.Row, node.Column];
                if(ghostMatrix[node.Column, node.Row])
                {
                    Ghost ghost = Instantiate(ghostsPrefab[Random.Range(0, ghostsPrefab.Count)], node.transform);
                    node.Ghost = ghost;
                    //ghost.transform.localScale = new Vector3(Random.Range(0, 2) == 0 ? 1 : -1, 1, 1);
                    ghost.transform.SetSiblingIndex(0);
                    Ghosts.Add(ghost);
                }

            }
        }

        public void HightLingtNodeCanPlace(List<(Block,Node)> nodesHightLingt)
        {
            foreach(var node in currentNodeHighLingt)
            {
                node.Item2.StopHighLighting();
            }
            currentNodeHighLingt.Clear();
            foreach((Block,Node) nodeHightLingt in nodesHightLingt)
            {
                Block block = nodeHightLingt.Item1;
                Node node = nodeHightLingt.Item2;

                node.SetHightLightImage(block.Sprite);
                currentNodeHighLingt.Add((block,node));
            }
        }
        public void StopHightLightNodeCantPlace()
        {
            foreach (var node in currentNodeHighLingt)
            {
                node.Item2.StopHighLighting();
            }
            currentNodeHighLingt.Clear();
        }
        public bool ApplyShapeToNode(ShapeInfo shapeReference)
        {
            bool success = false;
            if(currentNodeHighLingt.Count > 0) success = true;
            foreach(var node in currentNodeHighLingt)
            {
                node.Item2.StopHighLighting();
                node.Item2.ApplyNode(node,currentNodeHighLingt,shapeReference);
            }
            currentNodeHighLingt.Clear();
            if(success) LevelGameManager.Instance.OnStep();
            return success;
        }

        #region Interface Method
        public void OnBeginDrag(PointerEventData eventData)
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
            if (hit && hit.collider.CompareTag("Node"))
            {
                Node node = hit.transform.GetComponent<Node>();
                if (!node.IsFilled) return;
                isDragging = true;
                node.RemoveShape();
                shapeDrag.Show();
                shapeDrag.SetData(node.dataSpriteShape,node.Shape);
            }

        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;
            shapeDrag.SetPosition(Input.mousePosition,true);
            shapeDrag.CheckCanPlaceShape();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isDragging) return;
            bool ApplySuccess = ApplyShapeToNode(shapeDrag.CurrentShapeInfo);
            if (!ApplySuccess)
            {
                shapeDrag.CurrentShapeInfo.Return();
            }
            else shapeDrag.Hide();
            isDragging = false;
        }
        #endregion
    }
}
