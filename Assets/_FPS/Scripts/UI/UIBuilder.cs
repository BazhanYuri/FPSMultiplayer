using Unity.FPS.Multiplayer;
using Zenject;

namespace Unity.FPS.UI
{
    public class UIBuilder : IInitializable, IUIBuilder
    {
        private TeamChooserUIFactory _teamChooserUIFactory;
        private TeamWinUIFactory _teamWinUIFactory;
        private IPhotonManager _photonGameplayManager;
        private GameConfig _gameConfig;


        public TeamChooserUI TeamChooserUI { get; private set; }
        public TeamWinScreen TeamWinScreen { get; private set; }


        [Inject]
        public void Construct(TeamChooserUIFactory teamChooserUIFactory, TeamWinUIFactory teamWinUIFactory, IPhotonManager photonGameplayManager, GameConfig gameConfig)
        {
            _teamChooserUIFactory = teamChooserUIFactory;
            _teamWinUIFactory = teamWinUIFactory;
            _photonGameplayManager = photonGameplayManager;
            _gameConfig = gameConfig;
        }

        public TeamChooserUI CreateTeamChooserUI()
        {
            TeamChooserUI = _teamChooserUIFactory.Create();
            TeamChooserUI.Initialize(_photonGameplayManager, _gameConfig);
            return TeamChooserUI;
        }

        public TeamWinScreen CreateTeamWinScreen()
        {
            TeamWinScreen = _teamWinUIFactory.Create();
            TeamWinScreen.Initialize(_photonGameplayManager);
            return TeamWinScreen;
        }

        public void Initialize()
        {
            CreateTeamChooserUI();
            CreateTeamWinScreen();
            TeamWinScreen.Hide();
        }
    }
    public class TeamChooserUIFactory : PlaceholderFactory<TeamChooserUI>
    {
    }
    public class TeamWinUIFactory : PlaceholderFactory<TeamWinScreen>
    {
    }
}