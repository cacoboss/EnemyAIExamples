using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PatrolRoutine : MonoBehaviour
{
    [SerializeField]
    private Vector3[] patrolPoints;
    [SerializeField]
    private Vector3[] directionVectors;
    private GameObject go = null;

    private PatrolState _patrolState;
    private bool distance;

    public float forceMultiplier;
    public float maxVelocity;
    public float errorMargin = 0.01f;
    
    private int indexPoint;
    private Rigidbody2D rb;

    private bool enable;

    #region States

    enum PatrolState
    {
        FirstPoint,
        Patrol
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        GetNumberOfPatrolPoints();
        GetWorldPatrolPoints();
        CalculateVectors();
        
        _patrolState = PatrolState.FirstPoint;
        indexPoint = 1;
        rb = GetComponent<Rigidbody2D>();

        enable = true;
    }

    #region UpdateMethods
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(patrolPoints[indexPoint], transform.position) < errorMargin;
    }
    
    private void FixedUpdate()
    {
        if (enable)
        {
            if (_patrolState == PatrolState.FirstPoint &&
                Vector3.Distance(patrolPoints[0], transform.position) < errorMargin)
            {
                indexPoint = 1;
                _patrolState = PatrolState.Patrol;
            }
                
        
            //Move();
            //LimitVelocity();
        
            MoveVel();
        }
    }
    

    #endregion
    

    private void GetNumberOfPatrolPoints()
    {
        int childrens = transform.childCount;
        for (int i = 0; i < childrens; i++)
        {
            go = transform.GetChild(i).gameObject;
            if (go.CompareTag("Behaviour"))
            {
                break;
            }
        }
        childrens = go.transform.childCount;
        patrolPoints = new Vector3[childrens];
        directionVectors = new Vector3[childrens + 1];
        //indexPoint = childrens + 1;
        indexPoint = 0;
    }

    private void GetWorldPatrolPoints()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = transform.position + 
                              go.transform.localPosition + go.transform.GetChild(i).localPosition;
        }
    }

    private void CalculateVectors()
    {
        directionVectors[directionVectors.Length - 1] = go.transform.GetChild(0).localPosition.normalized;

        for (int i = 0; i < directionVectors.Length - 1; i++)
        {
            Vector3 vector;
            if (i == 0)
                vector = patrolPoints[0] - patrolPoints[patrolPoints.Length - 1];
            else
                vector = patrolPoints[i] - patrolPoints[i - 1];
            directionVectors[i] = vector.normalized;
        }
    }

    private void CalculateVectors(int index)
    {
        directionVectors[index] = (patrolPoints[index] - transform.position).normalized;
    }
    
    private void Move()
    {
        switch (_patrolState)
        {
            case PatrolState.FirstPoint:
                rb.AddForce(directionVectors[directionVectors.Length - 1] * forceMultiplier
                    , ForceMode2D.Force);
                break;
            case PatrolState.Patrol:
                if (distance)
                {
                    indexPoint++;
                    if (indexPoint == patrolPoints.Length)
                        indexPoint = 0;
                }
                rb.AddForce(directionVectors[indexPoint] * forceMultiplier
                    , ForceMode2D.Force);
                CalculateVectors(indexPoint);
                break;
        }
    }

    private void MoveVel()
    {
        switch (_patrolState)
        {
            case PatrolState.FirstPoint:
                rb.velocity = directionVectors[directionVectors.Length - 1] * forceMultiplier;
                break;
            case PatrolState.Patrol:
                if (Vector3.Distance(patrolPoints[indexPoint], transform.position) < errorMargin)
                {
                    indexPoint++;
                    if (indexPoint == patrolPoints.Length)
                        indexPoint = 0;
                }
                rb.velocity = directionVectors[indexPoint] * forceMultiplier;
                //CalculateVectors(indexPoint);
                break;
        }
    }

    private void LimitVelocity()
    {
        if (rb.velocity.magnitude > maxVelocity)
            rb.velocity = rb.velocity.normalized * maxVelocity;
    }

    public void UserEnable()
    {
        enable = true;
        _patrolState = PatrolState.FirstPoint;
        indexPoint = 1;
        directionVectors[directionVectors.Length - 1] = (patrolPoints[0] - transform.position).normalized ;
    }
    public void UserDisable()
    {
        enable = false;
    }

    public bool GetScriptStatus()
    {
        return enable;
    }

    public Vector3[] Get__directionVectors()
    {
        return this.directionVectors;
    }

    public int Get__indexPoint()
    {
        return this.indexPoint;
    }
}
