using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterFOV : MonoBehaviour
{
    private ChaseRoutine _chaseRoutine;
    private PatrolRoutine _patrolRoutine;
    private void Start()
    {
        _chaseRoutine = transform.root.gameObject.GetComponent<ChaseRoutine>();
        _patrolRoutine = transform.root.gameObject.GetComponent<PatrolRoutine>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.root.gameObject.CompareTag("Player"))
        {
            _chaseRoutine.SetChase(other.transform.root.gameObject);
            _patrolRoutine.UserDisable();
        }
    }
}
