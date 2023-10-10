﻿using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Unity.FPS.UI
{
    public class JetpackCounter : MonoBehaviour
    {
        [Tooltip("Image component representing jetpack fuel")]
        public Image JetpackFillImage;

        [Tooltip("Canvas group that contains the whole UI for the jetack")]
        public CanvasGroup MainCanvasGroup;

        [Tooltip("Component to animate the color when empty or full")]
        public FillBarColorChange FillBarColorChange;

        Jetpack m_Jetpack;


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
            m_Jetpack = FindObjectOfType<Jetpack>();
            DebugUtility.HandleErrorIfNullFindObject<Jetpack, JetpackCounter>(m_Jetpack, this);

            FillBarColorChange.Initialize(1f, 0f);
        }
        void Update()
        {
            if (m_Jetpack == null)
            {
                return;
            }
            MainCanvasGroup.gameObject.SetActive(m_Jetpack.IsJetpackUnlocked);

            if (m_Jetpack.IsJetpackUnlocked)
            {
                JetpackFillImage.fillAmount = m_Jetpack.CurrentFillRatio;
                FillBarColorChange.UpdateVisual(m_Jetpack.CurrentFillRatio);
            }
        }
    }
}