using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather.Utility;
using UnityEditor;
using UnityEngine;

namespace Unicorn.GhostCather.Data
{
    [CreateAssetMenu(fileName = "Data level")]
    public class DataLevelSO : SerializedScriptableObject
    {
        [TableMatrix(DrawElementMethod = nameof(DrawGhost),ResizableColumns = false,SquareCells = true)]
        public bool[,] GhostsMatrix;

        [SerializeField, PreviewField(200)] public Sprite BackGroundTable;  

        [SerializeField,PreviewField(100)]
        private List<Sprite> ShapesSprite;

        public Step Step;

        [ReadOnly]
        public List<int> ShapeIds;


        [Button("Init matrix")]
        public void InitMatrix(int x = 4,int y = 4)
        {
            GhostsMatrix = new bool[x,y];
        }

        [Button("Init Shape")]
        public void InitDataShape()
        {
            ShapeIds = new List<int>();
            foreach(Sprite s in ShapesSprite)
            {
                 int id = s.name.TryParseInt();
                ShapeIds.Add(id);
            }
        }

        private bool DrawGhost(Rect rect, bool value)
        {
            if(Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value =!value;
                GUI.changed = true;
            }

#if UNITY_EDITOR
            if (value) EditorGUI.DrawRect(rect, Color.white);
            else EditorGUI.DrawRect(rect, new Color(0, 0, 0, 0));
#endif

            return value;
        }

    }

    [System.Serializable]
    public class Step
    {
        public int MaxStep;
        public int Milestone3Star;
        public int Milestone2Star;
        public int Milestone1Star;

        public int CurrentStep { get; set; }

        /// <summary>
        /// gán curent step bằng max step;
        /// </summary>
        public void Init()
        {
            CurrentStep = MaxStep;
        }

        public int GetStar()
        {
            int star = 0;
            if (CurrentStep >= Milestone1Star) star = 1;
            if(CurrentStep >= Milestone2Star) star = 2;
            if(CurrentStep >= Milestone3Star) star = 3;
            return star;
        }
    }
}
