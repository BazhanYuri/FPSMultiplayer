using Zenject;

namespace Unity.FPS.UI
{
    public class UIBuilder : IInitializable, IUIBuilder
    {
        private TeamChooserUIFactory _teamChooserUIFactory;


        public TeamChooserUI TeamChooserUI { get; private set; }


        [Inject]
        public void Construct(TeamChooserUIFactory teamChooserUIFactory)
        {
            _teamChooserUIFactory = teamChooserUIFactory;
        }

        public TeamChooserUI CreateTeamChooserUI()
        {
            TeamChooserUI = _teamChooserUIFactory.Create();
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