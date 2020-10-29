using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;

    private Rigidbody2D _rb;

    private float degrees;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        degrees = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void FixedUpdate()
    {
        _rb.rotation = degrees;
    }
}
