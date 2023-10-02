using Unity.FPS.Multiplayer;
using Zenject;

namespace Unity.FPS.UI
{
    public class UIBuilder : IInitializable, IUIBuilder
    {
        private TeamChooserUIFactory _teamChooserUIFactory;
        private IPhotonManager _photonGameplayManager;
        private GameConfig _gameConfig;


        public TeamChooserUI TeamChooserUI { get; private set; }


        [Inject]
        public void Construct(TeamChooserUIFactory teamChooserUIFactory, IPhotonManager photonGameplayManager, GameConfig gameConfig)
        {
            _teamChooserUIFactory = teamChooserUIFactory;
            _photonGameplayManager = photonGameplayManager;
            _gameConfig = gameConfig;
        }

        public TeamChooserUI CreateTeamChooserUI()
        {
            TeamChooserUI = _teamChooserUIFactory.Create();
            TeamChooserUI.Initialize(_photonGameplayManager, _gameConfig);
            return TeamChooserUI;
        }

        public void Initialize()
        {
            CreateTeamChooserUI();
        }
    }
    public class TeamChooserUIFactory : PlaceholderFactory<TeamChooserUI>
    {
    }
}