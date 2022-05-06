using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CameraController : MonoBehaviour
{
    public Transform CameraTransform;
    public List<Transform> Targets2;

    [Range(0, 1)]
    public float Smoothing;
    // public float ZPosition;
    public float MinZoom = 3;
    public float zoomMarges = 0.5f;

    private Vector3 _targetPosition;
    private Vector3 _lookAtPosition;
    private Vector3 _velocity;
    // private bool _follow = true;
    private Camera _camera;

    public float forward=5f;


    public static Camera mainCamera;


    void Start()
    {
        // StartCoroutine(FollowPlayer());
        _camera = CameraTransform.GetComponent<Camera>();
        // ZPosition = transform.position.z;
        mainCamera = _camera;
        
    }


    void FixedUpdate(){
        SetCameraPositionExtension();
        SmoothFollow();
        // SetZoom();
    }
   

    void SmoothFollow()
    {
        //Es com perque es mogui la camara en plan "suau"
        //Que es mogui de la posicio a la objectiu amb la velocitat (pas per referencia) i un valor d'smoothing
        CameraTransform.position = Vector3.SmoothDamp(CameraTransform.position, _targetPosition, ref _velocity, Smoothing);
        //_lookAtPosition=CameraTransform.position+_camera.transform.forward * forward;
        //CameraTransform.LookAt(_lookAtPosition);
    }

    void SetCameraPositionExtension()
    {
        // var pos = Vector3.zero;

        float posX=Targets2[0].position.x;
        float posY=Targets2[0].position.y;
        float posZ=Targets2[0].position.z;


        //suma de tot + dividir pel total
        // foreach(Transform t in Targets2){
        //     posX += t.position.x;
        //     posY += t.position.y;
        //     posZ += t.position.z;

        // }


        //Basicament fiquem la camera al mig
        _lookAtPosition.x = posX; 
        _lookAtPosition.y = posY;
        _lookAtPosition.z = posZ;
        // Debug.Log(Targets2.Count());

        _targetPosition = _lookAtPosition - _camera.transform.forward * forward;

        // _targetPosition.z = ZPosition;
        //ZPos es una variable que definim nosaltres

    }

    // void SetZoom()
    // {
    //     return;
    //     //Si sol movem la camara a la meitat, pot ser que no veiem ningun

    //     //Tornem a copiar i pegar els minims i maxims
    //     float minY = Targets2.Min(pepe => pepe.position.y);
    //     float minX = Targets2.Min(pepe => pepe.position.x);
    //     float maxY = Targets2.Max(pepe => pepe.position.y);
    //     float maxX = Targets2.Max(pepe => pepe.position.x);

    //     float ar = _camera.aspect;

    //     float y1 = CameraTransform.position.y - minY;
    //     float y2 = maxY - CameraTransform.position.y;
    //     float yNeeded = Mathf.Max(y1, y2);

    //     float x1 = CameraTransform.position.x - minX;
    //     float x2 = maxX - CameraTransform.position.x;
    //     float xNeeded = Mathf.Max(x1, x2) / ar;

    //     _camera.orthographicSize = Mathf.Max(MinZoom, Math.Max(yNeeded, xNeeded))+0.2f;



    // }

    public void addTarget(){
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Player");

        // foreach(GameObject obj in characters){
        //     Targets
        // }
        Targets2.Clear();
        Debug.Log(characters.Length);
        for (int i=0; i<characters.Length; i++){
            // Targets[i] = characters[i].GetComponent<Transform>();
            
            Targets2.Add(characters[i].transform);
            Debug.Log("adding target");
        }       
    }


}
