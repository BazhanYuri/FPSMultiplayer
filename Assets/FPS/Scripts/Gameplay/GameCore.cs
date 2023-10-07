using System;
using Unity.FPS.Enums;
using Unity.FPS.Game;
using Unity.FPS.Multiplayer;
using Unity.FPS.UI;
using Zenject;

namespace Unity.FPS.Gameplay
{
    public class GameCore : IGameCore, IInitializable
    {
        private IUIBuilder _uiBuilder;
        private IPhotonManager _photonManager;
        private EventBus _eventBus;

        public event Action GameRestarted;


        [Inject]
        public void Initialize(IUIBuilder uiBuilder, IPhotonManager photonManager, EventBus eventBus)
        {
            _uiBuilder = uiBuilder;
            _photonManager = photonManager;
            _eventBus = eventBus;
        }

        public void Initialize()
        {
            _photonManager.TeamWon += OnTeamWinned;
        }

        private void OnTeamWinned(TeamType teamType)
        {
            GameRestarted?.Invoke();
            _eventBus.InvokeRoundCompleted();
        }
    }
}