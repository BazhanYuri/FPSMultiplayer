using System;
using Zenject;

namespace Unity.FPS.Game
{
    public class EventBus
    {
        public event Action PlayerSpawned;

        
        public void InvokePlayerSpawned()
        {
            PlayerSpawned?.Invoke();
        }
    }
}
