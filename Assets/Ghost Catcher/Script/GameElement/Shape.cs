using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unicorn.GhostCather.GamePlay
{
    // dùng để reponsive và giữ shapeInfor
    public class Shape : MonoBehaviour
    {
        [SerializeField] private ShapeInfo shapeInfo;
        public ShapeInfo ShapeInfo => shapeInfo;
        public void Initialization()
        {
            shapeInfo.Initialization();
        }

    }
}
