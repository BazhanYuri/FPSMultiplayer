using System;
using UnityEngine;
using Unity.FPS.Enums;
using Unity.FPS.Multiplayer;

namespace Unity.FPS.UI
{

    public class TeamChooserUI : UIComponent
    {
        [SerializeField] private TeamChooseSection _blueTeamButton;
        [SerializeField] private TeamChooseSection _redTeamButton;

        private IPhotonManager _photonGameplayManager;
        private GameConfig _gameConfig;

        public event Action<TeamType> TeamButtonClicked;


        public void Initialize(IPhotonManager photonGameplayManager, GameConfig gameConfig)
        {
            _photonGameplayManager = photonGameplayManager;
            _gameConfig = gameConfig;

            _blueTeamButton.UpdateTeamCount(_photonGameplayManager.BlueTeamPlayerCount, _gameConfig.maxBlueTeamPlayers);
            _redTeamButton.UpdateTeamCount(_photonGameplayManager.RedTeamPlayerCount, _gameConfig.maxBlueTeamPlayers);

            _blueTeamButton.TeamButtonClicked += OnBlueTeamButtonClickedHandler;
            _redTeamButton.TeamButtonClicked += OnRedTeamButtonClickedHandler;
            _photonGameplayManager.TeamCountUpdated += UpdateTeamsButtons;
        }
        private void OnDisable()
        {
            _blueTeamButton.TeamButtonClicked -= OnBlueTeamButtonClickedHandler;
            _redTeamButton.TeamButtonClicked -= OnRedTeamButtonClickedHandler;
            _photonGameplayManager.TeamCountUpdated -= UpdateTeamsButtons;
        }


        private void UpdateTeamsButtons()
        {
            _blueTeamButton.UpdateTeamCount(_photonGameplayManager.BlueTeamPlayerCount,  _gameConfig.maxBlueTeamPlayers);
            _redTeamButton.UpdateTeamCount(_photonGameplayManager.RedTeamPlayerCount,  _gameConfig.maxBlueTeamPlayers);

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