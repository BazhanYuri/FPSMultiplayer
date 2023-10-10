namespace Unity.FPS.UI
{
    public interface IUIBuilder
    {
        public TeamChooserUI TeamChooserUI { get; }
        public TeamWinScreen TeamWinScreen { get; }
        public TeamChooserUI CreateTeamChooserUI();
        public TeamWinScreen CreateTeamWinScreen();
    }
}