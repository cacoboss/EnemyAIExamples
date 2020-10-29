using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float degree;
    public float velocity;

    public Rigidbody2D _rb;

    public Vector2 direction;

    public float lifeTime;

    private float currentTime;

    private DamageType _damageType;

    public enum DamageType
    {
        ToPlayer,
        ToEnemy
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //_rb = GetComponent<Rigidbody2D>();
        currentTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = direction * velocity;
        
        if(Time.timeSinceLevelLoad > currentTime + lifeTime)
            Destroy(transform.root.gameObject);
    }

    public void SetVariables(float degree, DamageType damageType)
    {

        this.degree = degree;

        _rb.rotation = degree;
        _damageType = damageType;
        direction.Set(  Mathf.Cos(degree * Mathf.Deg2Rad),
                        Mathf.Sin(degree * Mathf.Deg2Rad)
                    );

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        GameObject obj = other.transform.root.gameObject;
        switch (_damageType)
        {
            case DamageType.ToEnemy:
                if (other.gameObject.CompareTag("Enemy"))
                {
                    Destroy(obj);
                    Destroy(gameObject);
                }
                break;
            case DamageType.ToPlayer:
                if (obj.CompareTag("Player"))
                {
                    Destroy(obj);
                    Destroy(gameObject);
                }

                break;
        }
    }
}
