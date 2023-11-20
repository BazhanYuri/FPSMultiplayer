using RootMotion.Dynamics;
using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        public PuppetMaster puppetMaster;
        public Health health;


        private void OnEnable()
        {
            health.OnDie += HandleDie;
        }
        private void OnDisable()

        {
            health.OnDie -= HandleDie;
        }
        private void HandleDie()
        {
            puppetMaster.state = PuppetMaster.State.Dead;
        }
    }
}