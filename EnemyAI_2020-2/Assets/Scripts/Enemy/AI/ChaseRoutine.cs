using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseRoutine : MonoBehaviour
{
    private ChaseState _chaseState;
    private GameObject go;
    private Rigidbody2D rb;
    public Vector3 vector;
    
    public float userVelocity;
    
    enum ChaseState
    {
        Chasing,
        Idle,
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _chaseState = ChaseState.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        switch (_chaseState)
        {
            case ChaseState.Chasing:
                if (go != null)
                {
                    vector = (go.transform.position - transform.position).normalized;
                    rb.velocity = vector * userVelocity;
                }
                break;    
            case ChaseState.Idle:
                //rb.velocity = Vector2.zero;
                break;
        }
    }

    public void SetChase(GameObject go)
    {
        _chaseState = ChaseState.Chasing;
        this.go = go;
    }

    public void SetIdle()
    {
        _chaseState = ChaseState.Idle;
        this.go = null;
    }
}
