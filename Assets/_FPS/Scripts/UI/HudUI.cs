using Unity.FPS.Enums;
using Zenject;

namespace Unity.FPS.UI
{
    public class HudUI : UIComponent
    {
        private IUIBuilder _uIBuilder;

        [Inject]
        public void Construct(IUIBuilder uIBuilder)
        {
            _uIBuilder = uIBuilder;
        }

        private void Start()
        {
            _uIBuilder.TeamChooserUI.TeamButtonClicked += OnPlayerChoosed;
        }
        private void OnPlayerChoosed(TeamType teamType)
        {
            Show();
        }

    }
}