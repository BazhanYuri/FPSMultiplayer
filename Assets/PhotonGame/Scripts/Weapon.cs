using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera camera;
    public int damage;
    public ParticleSystem hitFX;

    private void Update()
    {
        if (Input.GetButton("Fire"))
        {

            Fire();
        }
    }
    private void Fire()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f)) 
        {
            PhotonNetwork.Instantiate(hitFX.name, hit.point, Quaternion.identity);
            if (hit.transform.gameObject.GetComponent<Health>())
            {
                hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }
}
