using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather;
using Unicorn.GhostCather.Data;
using Unicorn.GhostCather.GamePlay;
using UnityEngine;

namespace Unicorn.GhostCather.GamePlay
{
    public class ShapePanel : MonoBehaviour
    {
        [HideInInspector] public List<Shape> Shapes;

        [SerializeField] private RectTransform content;

        private DataLevelSO data => LevelGameManager.Instance.Data;
        private PrefabStorage prefabStorage => PrefabStorage.Instance;

        public void Init()
        {
            Shapes = new List<Shape>();
            List<Shape> shapePrefabs = prefabStorage.Shapes;
            foreach(int id in data.ShapeIds)
            {
                Shape shape = Instantiate(shapePrefabs[id - 1],content);
                shape.Initialization();
                Shapes.Add(shape);
            }
        }
    }
}
