using System;
using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.Enums;

namespace Unity.FPS.UI
{
    public class TeamChooserUI : UIComponent
    {
        [SerializeField] private Button _blueTeamButton;
        [SerializeField] private Button _redTeamButton;

        public event Action<TeamType> TeamButtonClicked;


        private void OnEnable()
        {
            _blueTeamButton.onClick.AddListener(OnBlueTeamButtonClickedHandler);
            _redTeamButton.onClick.AddListener(OnRedTeamButtonClickedHandler);
        }
        private void OnDisable()
        {
            _blueTeamButton.onClick.RemoveListener(OnBlueTeamButtonClickedHandler);
            _redTeamButton.onClick.RemoveListener(OnRedTeamButtonClickedHandler);
        }

        private void OnBlueTeamButtonClickedHandler()
        {
            Hide();

            TeamButtonClicked?.Invoke(TeamType.Blue);
        }
        private void OnRedTeamButtonClickedHandler()
        {
            Hide();

            TeamButtonClicked?.Invoke(TeamType.Red);
        }
    }
}