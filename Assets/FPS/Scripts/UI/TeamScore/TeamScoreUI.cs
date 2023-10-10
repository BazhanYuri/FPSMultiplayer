using UnityEngine;
using Unity.FPS.Multiplayer;
using TMPro;
using Zenject;

namespace Unity.FPS.UI
{
    public class TeamScoreUI : UIComponent
    {
        [SerializeField] private TextMeshProUGUI _blueTeamScoreText;
        [SerializeField] private TextMeshProUGUI _redTeamScoreText;

        private IPhotonManager _photonManager;


        [Inject]
        public void Construct(IPhotonManager photonManager)
        {
            _photonManager = photonManager;
        }
        private void Awake()
        {
            _photonManager.TeamScoreHanlder.ScoreUpdated += OnScoreChanged;
        }
        private void OnScoreChanged()
        {
            _blueTeamScoreText.text = _photonManager.TeamScoreHanlder.BlueTeamScore.ToString();
            _redTeamScoreText.text = _photonManager.TeamScoreHanlder.RedTeamScore.ToString();
        }
    }
}