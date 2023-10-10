using UnityEngine;
using DG.Tweening;
using Unity.FPS.Enums;
using Unity.FPS.Multiplayer;

namespace Unity.FPS.UI
{
    public class TeamWinScreen : UIComponent
    {
        [SerializeField] private RectTransform _blueTeamWinText;
        [SerializeField] private RectTransform _redTeamWinSection;



        public void Initialize(IPhotonManager photonGameplayManager)
        {
            photonGameplayManager.TeamWon += OnTeamWinned;
        }
        private void OnTeamWinned(TeamType teamType)
        {
            Show();
            if (teamType == TeamType.Blue)
            {
                _blueTeamWinText.gameObject.SetActive(true);
                _redTeamWinSection.gameObject.SetActive(false);
                TextAnimation(_blueTeamWinText);
            }
            else
            {
                _blueTeamWinText.gameObject.SetActive(false);
                _redTeamWinSection.gameObject.SetActive(true);
                TextAnimation(_redTeamWinSection);
            }
        }
        private void TextAnimation(RectTransform rectTransform)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(rectTransform.DOScale(Vector3.one, 1).SetEase(Ease.OutBounce));
            sequence.AppendInterval(2);
            sequence.Append(rectTransform.DOScale(Vector3.zero, 1).SetEase(Ease.InBounce));
        }
    }
}