using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unicorn.GhostCather.Data;
using Unicorn.GhostCather.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unicorn.GhostCather.UI
{
    public class Ui_PackLevel : MonoBehaviour,IPointerClickHandler
    {
        [SerializeField] private TMP_Text levelTxt;
        [SerializeField] private Image levelImage;
        [SerializeField] private RectTransform lockImg;
        [SerializeField] private List<Image> stars;
        [SerializeField] private Sprite starOn;
        [SerializeField] private Sprite starOff;

        public Action<DataLevel> OnChooseLevel;

        private DataLevel data;
        public DataLevel DataLevel => data;

        public void Initialized(DataLevel data)
        {
            this.data = data;
            levelTxt.text = data.Level.ToString();

            Sprite bg = GameUtility.LoadResources<Sprite>($"UI/ChooseLevel/LevelImg/lv{data.Level}");
            if (bg == null) bg = GameUtility.LoadResources<Sprite>($"UI/ChooseLevel/LevelImg/lv{1}");
            levelImage.sprite = bg;

            lockImg.gameObject.SetActive(data.IsLocked);
            SetStarNumber(data.StarNumber);
            data.OnValueChanged = OnDataChanged;
        }

        // tự động thay đổi khi dữ liệu được thay đổi
        public void OnDataChanged()
        {
            lockImg.gameObject.SetActive(data.IsLocked);
            SetStarNumber(data.StarNumber);
        }
        private void SetStarNumber(int starNumber)
        {
            stars[0].sprite = starNumber >= 1 ? starOn : starOff;
            stars[1].sprite = starNumber >= 2 ? starOn : starOff;
            stars[2].sprite = starNumber >= 3 ? starOn : starOff;
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            if (!data.IsLocked) OnChooseLevel?.Invoke(data);
        }
    }
}
