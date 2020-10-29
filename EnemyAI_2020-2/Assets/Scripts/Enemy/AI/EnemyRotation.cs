using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyRotation : MonoBehaviour
{
    private PatrolRoutine _patrolRoutine;

    private ChaseRoutine _chaseRoutine;

    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _patrolRoutine = GetComponent<PatrolRoutine>();
        _chaseRoutine = GetComponent<ChaseRoutine>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_patrolRoutine.GetScriptStatus())
        {
            float degrees = Mathf.Atan2(
                _patrolRoutine.Get__directionVectors()[_patrolRoutine.Get__indexPoint()].y,
                _patrolRoutine.Get__directionVectors()[_patrolRoutine.Get__indexPoint()].x
            ) * Mathf.Rad2Deg;
            _rb.rotation = degrees;
        }
        else
        {
            float degrees = Mathf.Atan2(
                _chaseRoutine.vector.y,
                _chaseRoutine.vector.x
            ) * Mathf.Rad2Deg;
            _rb.rotation = degrees;
        }
        
    }
}
