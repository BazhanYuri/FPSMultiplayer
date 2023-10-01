using Unity.FPS.Game;
using UnityEngine;
using Zenject;

namespace Unity.FPS.AI
{
    public class FollowPlayer : MonoBehaviour
    {
        Transform m_PlayerTransform;
        Vector3 m_OriginalOffset;
        private EventBus _eventBus;



        [Inject]
        public void Construct(EventBus eventBus)
        {
            _eventBus = eventBus;
        }
        private void OnEnable()
        {
            _eventBus.PlayerSpawned += OnPlayerSpawned;
        }
        private void OnDisable()
        {
            _eventBus.PlayerSpawned -= OnPlayerSpawned;
        }
        private void OnPlayerSpawned()
        {
            GetPlayer();
        }
        private void GetPlayer()
        {
            ActorsManager actorsManager = FindObjectOfType<ActorsManager>();
            if (actorsManager != null)
                m_PlayerTransform = actorsManager.Player.transform;
            else
            {
                enabled = false;
                return;
            }

            m_OriginalOffset = transform.position - m_PlayerTransform.position;
        }

        void LateUpdate()
        {
            if (m_PlayerTransform == null)
                return;
            transform.position = m_PlayerTransform.position + m_OriginalOffset;
        }
    }
}