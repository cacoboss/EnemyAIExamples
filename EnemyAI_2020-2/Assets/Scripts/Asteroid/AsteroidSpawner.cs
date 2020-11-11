using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private float _timeToSpawn = 5f;

    private float _timeSinceSpawn;

    private AsteroidPooler _asteroidPooler;

    private Vector2 _cameraSize;
    
    // Start is called before the first frame update
    void Start()
    {
        _asteroidPooler = transform.root.gameObject.GetComponentInChildren<AsteroidPooler>();
        _cameraSize = Camera.main.ScreenToWorldPoint(
            new Vector3(Screen.width, Screen.height, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        _timeSinceSpawn += Time.deltaTime;
        if (_timeSinceSpawn > _timeToSpawn)
        {
            GameObject obj = _asteroidPooler.GetAsteroidInstance();
            if ((obj) != null)
            {
                //obj.transform.position = this.transform.position;
                obj.transform.position = new Vector3(this.transform.position.x,
                                            Random.Range(-_cameraSize.y/2,_cameraSize.y/2),
                                            this.transform.position.z);
            }
            _timeSinceSpawn = 0f;
        }
    }
}
