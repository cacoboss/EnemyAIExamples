using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AusioManagerTest : MonoBehaviour
{
    [SerializeField] private AudioClip sfx;
    [SerializeField] private AudioClip music1;
    [SerializeField] private AudioClip music2;

    private void Update()
    {
        if(Input.GetButtonDown("Crouch"))
            AusioManager.p_Instance.PlaySFX(sfx);
        
        if(Input.GetAxisRaw("Horizontal") == -1 && !Input.GetButton("Jump"))
            AusioManager.p_Instance.PlayMusic(music1);
        
        if(Input.GetAxisRaw("Horizontal") == 1 && !Input.GetButton("Jump"))
            AusioManager.p_Instance.PlayMusic(music2);
        
        if(Input.GetAxisRaw("Vertical") == -1 && !Input.GetButton("Jump"))
            AusioManager.p_Instance.PlayMusicWithFade(music1);
        
        if(Input.GetAxisRaw("Vertical") == 1 && !Input.GetButton("Jump"))
            AusioManager.p_Instance.PlayMusicWithFade(music2);
        
        if(Input.GetAxisRaw("Horizontal") == -1 && Input.GetButton("Jump"))
            AusioManager.p_Instance.PlayMusicWithCrossfade(music1, 3f);
        
        if(Input.GetAxisRaw("Horizontal") == 1 && Input.GetButton("Jump"))
            AusioManager.p_Instance.PlayMusicWithCrossfade(music2, 3f);
        
        if(Input.GetAxisRaw("Vertical") == -1 && Input.GetButton("Jump"))
            AusioManager.p_Instance.StopMusic();
        //Debug.Log("GetAxisRaw: " + Input.GetAxisRaw("Horizontal"));
        //Debug.Log("GetAxis: " + Input.GetAxis("Horizontal"));
    }
}
