using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Unicorn
{
    public class Ghost : MonoBehaviour
    {
        [SerializeField] private Image img;

        [SerializeField] private Sprite ghostNomarl;
        [SerializeField] private Sprite ghostFrown;

        [SerializeField] private DOTweenAnimation dotAnim;
        public bool IsBelowLit { get; set;}

        private void Start()
        {
            // dùng random này bởi vì nếu dùng của unity trong cùn 1 frame sẽ bị trùng kết quả random trên tất cả các con ma
           
            transform.localScale = new Vector3(GameUtility.RandomInt(0,1) == 0 ? 1 : -1, 1, 1);
        }

        public void SetBelowLit(bool isBelowLit)
        {
            IsBelowLit = isBelowLit;
            img.sprite = isBelowLit ? ghostFrown : ghostNomarl;
            if (isBelowLit) dotAnim.DOPause();
            else dotAnim.DOPlay();
        }
    }
}
