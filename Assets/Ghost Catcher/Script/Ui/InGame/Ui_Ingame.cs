using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unicorn.GhostCather.Data;
using Unicorn.GhostCather.Utility;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

namespace Unicorn.GhostCather.UI
{
    public class Ui_Ingame : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Button")] private Button btnSetting;
        [SerializeField, FoldoutGroup("Button")] private Button btnBack;

        [SerializeField, FoldoutGroup("Text")] private TMP_Text levelTxt;
        [SerializeField, FoldoutGroup("Text")] private TMP_Text stepTxt;

        [SerializeField, FoldoutGroup("Image")] private Image stepBar;
        [SerializeField, FoldoutGroup("Image")] private List<Image> starImages;

        [SerializeField, FoldoutGroup("RectTransform")] private List<RectTransform> barStarLines;
        [SerializeField, FoldoutGroup("RectTransform")] private RectTransform parentbarStarLines; 

        [SerializeField, FoldoutGroup("Sprite")] private Sprite starOn;
        [SerializeField, FoldoutGroup("Sprite")] private Sprite starOff;

        [SerializeField] private List<DOTweenAnimation> _DOTweenAnimations;
        [SerializeField] private List<Animator> animators;

        private Step stepData;

        private void Start()
        {
            btnSetting.onClick.AddListener(ClickButtonSetting);
            btnBack.onClick.AddListener(ClickBtnBack);
        }

        public void Show() => transform.gameObject.SetActive(true);
        public void Hide() => transform.gameObject.SetActive(false);

        public void SetLevel(int level) => levelTxt.text = "LEVEL " + level.ToString();


        public void SetData(int level,Step step)
        {
            stepData = step;
            SetLevel(level);
            SetStep(step.MaxStep);
            foreach(Image starImg in starImages)
            {
                starImg.sprite = starOn;
            }

            float widthFill = parentbarStarLines.rect.width;
            float XRatio = widthFill / step.MaxStep;

            float XStarLine1 = XRatio * step.Milestone1Star;
            float XStarLine2 = XRatio * step.Milestone2Star;
            float XStarLine3 = XRatio * step.Milestone3Star;

            barStarLines[0].anchoredPosition = new Vector2(XStarLine1, barStarLines[0].anchoredPosition.y);
            barStarLines[1].anchoredPosition = new Vector2(XStarLine2, barStarLines[1].anchoredPosition.y);
            barStarLines[2].anchoredPosition = new Vector2(XStarLine3, barStarLines[2].anchoredPosition.y);

        }

        public void SetStep(int value)
        {
            float valueF = (float)value;
            float maxStepF = (float)stepData.MaxStep;
            stepBar.fillAmount = value/maxStepF;
            stepTxt.text = value.ToString();

            if (value < stepData.Milestone3Star) starImages[2].sprite = starOff;
            if(value < stepData.Milestone2Star) starImages[1].sprite = starOff;
            if(value < stepData.Milestone1Star) starImages[0].sprite = starOff;

        }

        public void ClickButtonSetting()
        {
            GameAction.OnClickButton(btnSetting);
            SoundManager.Instance.PlaySoundButton();
        }

        private void ClickBtnBack()
        {
            GameAction.OnClickButton(btnBack);
            SoundManager.Instance.PlaySoundButton();

            GameManager.Instance.GameStateController.ChangeState(GameState.CHOOSE_LEVEL);
        }

    }
}
