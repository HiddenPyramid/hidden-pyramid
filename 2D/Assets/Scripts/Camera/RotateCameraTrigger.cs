using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraTrigger : MonoBehaviour
{

    public Camera _camera;
    // private float distanceToTarget = 10;

    // public float speedMod = 2.0f;

    public GameObject newPos;
   
    public float timeCount = 0.0f;
    public float timeToLerp= 3.0f;
    private bool rotation = false;
    Quaternion startRotation;
    private GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == Parameter.LAYER_PLAYER)
        {
            rotation = true;
            timeCount=0.0f;
            startRotation=_camera.transform.rotation;
            player = other.gameObject;
            StartCoroutine(DisablePlayer());
        }
    }

    void FixedUpdate(){

        if(rotation){
            timeCount = timeCount + Time.fixedDeltaTime;
            float l_Pct=Mathf.Min(1.0f, timeCount/timeToLerp);
            _camera.transform.rotation = Quaternion.Lerp(startRotation, newPos.transform.rotation, l_Pct);
            
            if(l_Pct==1.0f){
                rotation=false;
            }
                
        }
    }

    IEnumerator DisablePlayer()
    {
        player.SetActive(false);
        yield return new WaitForSeconds(timeToLerp);
        player.SetActive(true);
        player.transform.rotation = _camera.transform.rotation;
        player.transform.position = transform.position;
        gameObject.SetActive(false);
    }
}
