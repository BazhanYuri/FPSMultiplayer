using Photon.Pun;
using Unity.FPS.Enums;
using UnityEngine;

namespace Unity.FPS.Game
{
    public class Damageable : MonoBehaviour
    {
        public Health health;
        public PlayerPart playerPart;
        public PlayerDamageConfig playerDamageConfig;
        public bool isOnline;


        public Health Health { get => health; set => health = value; }

        public void InflictDamage(float damage, bool isExplosionDamage, GameObject damageSource)
        {
            if (Health)
            {
                var totalDamage = damage;

                totalDamage = damage * playerDamageConfig.GetMultiplayer(playerPart);


                if (isOnline == true)
                {
                    TookDamageOnline(totalDamage);
                }
                else
                {
                    TookDamageOffline(totalDamage);
                }
            }
        }
        private void TookDamageOnline(float damage)
        {
            Health.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
        }
        private void TookDamageOffline(float damage)
        {
            Health.TakeDamage(damage);
        }
    }
}