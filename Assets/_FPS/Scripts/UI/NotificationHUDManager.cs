﻿using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using Zenject;

namespace Unity.FPS.UI
{
    public class NotificationHUDManager : MonoBehaviour
    {
        [Tooltip("UI panel containing the layoutGroup for displaying notifications")]
        public RectTransform NotificationPanel;

        [Tooltip("Prefab for the notifications")]
        public GameObject NotificationPrefab;

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
            PlayerWeaponsManager playerWeaponsManager = FindObjectOfType<PlayerWeaponsManager>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerWeaponsManager, NotificationHUDManager>(playerWeaponsManager,
                this);
            playerWeaponsManager.OnAddedWeapon += OnPickupWeapon;

            Jetpack jetpack = FindObjectOfType<Jetpack>();
            DebugUtility.HandleErrorIfNullFindObject<Jetpack, NotificationHUDManager>(jetpack, this);
            jetpack.OnUnlockJetpack += OnUnlockJetpack;

            EventManager.AddListener<ObjectiveUpdateEvent>(OnObjectiveUpdateEvent);
        }

        void OnObjectiveUpdateEvent(ObjectiveUpdateEvent evt)
        {
            if (!string.IsNullOrEmpty(evt.NotificationText))
                CreateNotification(evt.NotificationText);
        }

        void OnPickupWeapon(WeaponController weaponController, int index)
        {
            if (index != 0)
                CreateNotification("Picked up weapon : " + weaponController.WeaponName);
        }

        void OnUnlockJetpack(bool unlock)
        {
            CreateNotification("Jetpack unlocked");
        }

        public void CreateNotification(string text)
        {
            GameObject notificationInstance = Instantiate(NotificationPrefab, NotificationPanel);
            notificationInstance.transform.SetSiblingIndex(0);

            NotificationToast toast = notificationInstance.GetComponent<NotificationToast>();
            if (toast)
            {
                toast.Initialize(text);
            }
        }

        void OnDestroy()
        {
            EventManager.RemoveListener<ObjectiveUpdateEvent>(OnObjectiveUpdateEvent);
        }
    }
}