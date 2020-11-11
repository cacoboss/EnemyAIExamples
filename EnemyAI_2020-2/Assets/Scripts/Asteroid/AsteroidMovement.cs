using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private float _speed;
    private float _rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _direction = movementVector();
        _speed = Random.Range(1f, 5f);
        _rotationSpeed = Random.Range(-40f, 40f);
    }

    // Update is called once per frame
    void Update()
    {
        ObjectRotation();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _direction * _speed;
    }

    private Vector2 movementVector()
    {
        Vector2 vector = Random.insideUnitCircle;
        if (vector.x > 0)
        {
            vector = Vector2.Scale(new Vector2(-1f, 1f), vector);
        }
        return vector.normalized;
    }

    private void ObjectRotation()
    {
        transform.Rotate(0f,0f,_rotationSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        FindObjectOfType<AsteroidPooler>().ReturnAsteroidInstanceToPool(gameObject);
    }
}
