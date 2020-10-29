using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletPoint;
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject go = Instantiate(bullet, bulletPoint.position, Quaternion.identity);
            float degree = transform.root.gameObject.GetComponent<Rigidbody2D>().rotation;
            go.GetComponent<Bullet>().SetVariables(degree, Bullet.DamageType.ToEnemy);
        }
    }
}
