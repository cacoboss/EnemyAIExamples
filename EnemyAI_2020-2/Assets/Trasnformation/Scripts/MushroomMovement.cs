using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool isFalling;

    public Vector2 direction;

    public Transitions.MarioState ms;

    public AudioClip sfx;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        isFalling = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isFalling)
        {
            _rb.velocity = direction;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject obj_c = other.gameObject;
        GameObject obj_f = other.transform.root.gameObject;

        if (obj_c.CompareTag("Ground"))
        {
            isFalling = false;
        }
        else if (obj_f.CompareTag("Player"))
        {
            obj_f.GetComponentInChildren<Transitions>().ChangeState(ms);
            AusioManager.p_Instance.PlaySFX(sfx);
            Destroy(this.gameObject);
        }
    }
}
