using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Unity.FPS.UI
{
    public class TeamChooseSection : UIComponent
    {
        [SerializeField] private Button _teamButton;
        [SerializeField] private TextMeshProUGUI _playersCountText;

        public event Action TeamButtonClicked;

        private int _maxCount;


        public void OnEnable()
        {
            _teamButton.onClick.AddListener(OnButtonClickedHandler);
        }
        public void OnDisable()
        {
            _teamButton.onClick.RemoveListener(OnButtonClickedHandler);
        }
        public void UpdateTeamCount(int currentCount, int maxCount)
        {
            Debug.Log("UpdateTeamCount");
            _maxCount = maxCount;
            _playersCountText.text = currentCount + "/" + maxCount;

            if (_maxCount == currentCount)
            {
                _teamButton.interactable = false;
            }
            else
            {
                _teamButton.interactable = true;
            }
        }
        private void OnButtonClickedHandler()
        {
            TeamButtonClicked?.Invoke();
        }
     
    }
}