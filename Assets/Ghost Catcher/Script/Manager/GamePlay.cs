using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unicorn.GhostCather.GamePlay
{
    public class GamePlay : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        private List<Node> blockGrid => LevelGameManager.Instance.Grid.Nodes;

        private ShapeInfo currentShape;
        private Transform hittingBlock;
        private List<Node> highLightingBlocks;


        #region UnityMethod

        #endregion

        #region Interface Method
        //public void OnPointerDown(PointerEventData eventData)
        //{
        //    GameObject gameObject = eventData.pointerCurrentRaycast.gameObject;
        //    if(gameObject != null)
        //    {
        //        Transform clickedObject = gameObject.transform;
        //        ShapeInfo shapeInfo = clickedObject.GetComponent<ShapeInfo>();
        //        if (shapeInfo != null && clickedObject.childCount > 0)
        //        {
        //            currentShape = shapeInfo;
        //            if (!currentShape.isLock)
        //            {
        //                Vector3 pos = Camera.main.ScreenToWorldPoint(eventData.position); Debug.Log("Tham chiếu lại cam!");
        //                //currentShape.transform.localScale

        //            }
        //        }
        //    }
        //}
        //public void OnBeginDrag(PointerEventData eventData)
        //{
            
        //}

        //public void OnDrag(PointerEventData eventData)
        //{
            
        //}

        //public void OnPointerUp(PointerEventData eventData)
        //{
            
        //}
        #endregion
    }
}
