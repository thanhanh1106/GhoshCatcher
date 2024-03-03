using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather.Utility;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Unicorn.GhostCather.UI
{
    public class Ui_Win : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Image")] private Image star1;
        [SerializeField, FoldoutGroup("Image")] private Image star2;
        [SerializeField, FoldoutGroup("Image")] private Image star3;

        [SerializeField, FoldoutGroup("Sprite")] private Sprite starOn_Small;
        [SerializeField, FoldoutGroup("Sprite")] private Sprite starOff_Small;
        [SerializeField, FoldoutGroup("Sprite")] private Sprite starOn_Big;
        [SerializeField, FoldoutGroup("Sprite")] private Sprite starOff_Big;

        [SerializeField, FoldoutGroup("Button")] private Button btnNextLevel;
        [SerializeField, FoldoutGroup("Button")] private Button btnReplay;

        [SerializeField] private List<DOTweenAnimation> _DOTweenAnimations;
        [SerializeField] private List<Animator> animators;

        [SerializeField] private RectTransform popupRect;


        private void Start()
        {
            btnNextLevel.onClick.AddListener(OnClickNextLevel);
            btnReplay.onClick.AddListener(OnClickReplay);
        }

        public void Initialized(int starNumber)
        {
            star1.sprite = starNumber >= 1 ? starOn_Small : starOff_Small;
            star2.sprite = starNumber >= 2 ? starOn_Small : starOff_Small;
            star3.sprite = starNumber >= 3 ? starOn_Big : starOff_Big;
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide()
        {
            popupRect.localScale = Vector3.one;
            gameObject.SetActive(false);
        }

        private void OnClickReplay()
        {
            SoundManager.Instance.PlaySoundButton();
            GameAction.OnClickButton(btnReplay);

            this.OnBackPressedWithAnim(_DOTweenAnimations, animators: animators, callback: () =>
            {
                Hide();
                GameManager.Instance.Replay();
            });
        }

        private void OnClickNextLevel()
        {
            SoundManager.Instance.PlaySoundButton();
            GameAction.OnClickButton(btnNextLevel);
            
            this.OnBackPressedWithAnim(_DOTweenAnimations, animators: animators, callback: () =>
            {
                Hide();
                GameManager.Instance.NextLevel();
            });
        }
    }
}
