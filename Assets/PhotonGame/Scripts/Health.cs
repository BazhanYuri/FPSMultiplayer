using UnityEngine;
using Photon.Pun;
using TMPro;

public class Health : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI text;

    [PunRPC]
    public void TakeDamage(int damage)
    {
        health -= damage;
        text.text = health.ToString();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
