using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float _timeToSpawn = 5f;

    private float _timeSinceSpawn;

    private AsteroidPooler _asteroidPooler;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _asteroidPooler = transform.root.gameObject.GetComponentInChildren<AsteroidPooler>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceSpawn += Time.deltaTime;
        if (_timeSinceSpawn > _timeToSpawn)
        {
            GameObject obj = _asteroidPooler.GetAsteroidInstance();
            obj.transform.position = this.transform.position;
            _timeSinceSpawn = 0f;
        }
    }
}
