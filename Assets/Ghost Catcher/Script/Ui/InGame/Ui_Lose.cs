using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unicorn.GhostCather.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Unicorn.GhostCather.UI
{
    public class Ui_Lose : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Button")] private Button btnTryAgain;
        [SerializeField, FoldoutGroup("Button")] private Button btnHome;

        [SerializeField] private List<DOTweenAnimation> _DOTweenAnimations;
        [SerializeField] private List<Animator> animators;

        [SerializeField] private RectTransform popupRect;

        private void Start()
        {
             btnTryAgain.onClick.AddListener(OnClickTryAgain);
             btnHome.onClick.AddListener(OnClickBackHome);
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide()
        {
            popupRect.localScale = Vector3.one;
            gameObject.SetActive(false);
        }

        private void OnClickBackHome()
        {
            SoundManager.Instance.PlaySoundButton();
            GameAction.OnClickButton(btnHome);

            this.OnBackPressedWithAnim(_DOTweenAnimations, animators: animators, callback: () =>
            {
                Hide();
                GameManager.Instance.BackHome();
            });
        }

        private void OnClickTryAgain()
        {
            SoundManager.Instance.PlaySoundButton();
            GameAction.OnClickButton(btnTryAgain);

            this.OnBackPressedWithAnim(_DOTweenAnimations, animators: animators, callback: () =>
            {
                Hide();
                GameManager.Instance.Replay();
            });
        }
    }
}
