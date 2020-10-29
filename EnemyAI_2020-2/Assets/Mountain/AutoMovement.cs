using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class AutoMovement : MonoBehaviour
{
    public float[] forceMovement;
    public float maxVelocity;
    public int index = 0;
    
    private Vector2 _direction;

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _direction = Vector2.right;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_direction);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(_direction * forceMovement[index], ForceMode2D.Force);
        LimitVelocity();
    }

    private void LimitVelocity()
    {
        if (_rb.velocity.magnitude > maxVelocity)
            _rb.velocity = _rb.velocity.normalized * maxVelocity;
    }

    private void ChangeDirection(float angle)
    {
        _direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle) ).normalized;
        //Debug.Log(_direction);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject obj = other.gameObject;
        if (obj.CompareTag("Uphill"))
        {
            float rad = (obj.transform.eulerAngles.z - 2f)* Mathf.Deg2Rad;
            //Debug.Log(obj.transform.eulerAngles.z);
            ChangeDirection(rad);
            index = 1;
        }
        else if(obj.CompareTag("Uphill-T"))
        {
            //float rad = (obj.transform.eulerAngles.z - 2f)* Mathf.Deg2Rad;
            //Debug.Log(obj.transform.eulerAngles.z);
            float rad = obj.GetComponent<HillValues>().angle * Mathf.Deg2Rad;
            ChangeDirection(rad);
            index = 1;
        }
        else 
        {
            ChangeDirection(0f);
            index = 0;
        }
    }
    
}
