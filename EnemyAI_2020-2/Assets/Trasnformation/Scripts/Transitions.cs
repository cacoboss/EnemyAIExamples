using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    public enum MarioState
    {
        Small_Mario,
        Big_Mario,
        Cape_Mario,
        Flower_Mario,
        Death
    }

    private int _childCount;
    
    // Start is called before the first frame update
    void Start()
    {
        _childCount = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeState(MarioState marioState)
    {
        switch (marioState)
        {
            case MarioState.Small_Mario:
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                //transform.GetChild(3).gameObject.SetActive(true);
                break;
            case MarioState.Big_Mario:
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                break;
            case MarioState.Cape_Mario:
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                //transform.GetChild(3).gameObject.SetActive(true);
                break;
        }
    }
}
