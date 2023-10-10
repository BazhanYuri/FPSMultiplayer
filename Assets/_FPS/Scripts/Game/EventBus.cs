using System;
using Zenject;

namespace Unity.FPS.Game
{
    public class EventBus
    {
        public event Action PlayerSpawned;
        public event Action RoundCompleted;
        public event Action HalfRoundsPassed;


        public void InvokePlayerSpawned()
        {
            PlayerSpawned?.Invoke();
        }
        public void InvokeRoundCompleted()
        {
            RoundCompleted?.Invoke();
        }
        public void InvokeHalfRoundsPassed()
        {
            HalfRoundsPassed?.Invoke();
        }
    }
}
