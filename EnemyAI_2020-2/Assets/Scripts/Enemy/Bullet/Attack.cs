using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform bulletPoint;
    public GameObject bullet;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.gameObject.CompareTag("Player"))
        {
            GameObject go = Instantiate(bullet, bulletPoint.position, Quaternion.identity);
            float degree = transform.root.gameObject.GetComponent<Rigidbody2D>().rotation;
            go.GetComponent<Bullet>().SetVariables(degree, Bullet.DamageType.ToPlayer);
        }
    }
}
