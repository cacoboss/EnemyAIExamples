using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPooler : MonoBehaviour
{
    [SerializeField] private GameObject _asteroidPrefab;

    [SerializeField] private Queue<GameObject> _asteroidPool;

    [SerializeField] private int poolSize;

    #region UpdateMethods
    
    // Start is called before the first frame update

    void Start()
    {
        poolSize = 5;
        _asteroidPool = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(_asteroidPrefab);
            _asteroidPool.Enqueue( obj );
            obj.SetActive(false);
        }
    }
    
    #endregion

    public GameObject GetAsteroidInstance()
    {
        if (_asteroidPool.Count > 0)
        {
            GameObject obj = _asteroidPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(_asteroidPrefab);
            return obj;
        }
    }

    public void ReturnAsteroidInstanceToPool(GameObject obj)
    {
        _asteroidPool.Enqueue(obj);
        obj.SetActive(false);
    }
    
}
