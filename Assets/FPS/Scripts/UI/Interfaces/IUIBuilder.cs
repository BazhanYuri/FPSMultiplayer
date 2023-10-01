namespace Unity.FPS.UI
{
    public interface IUIBuilder
    {
        public TeamChooserUI TeamChooserUI { get; }
        public TeamChooserUI CreateTeamChooserUI();
    }
}