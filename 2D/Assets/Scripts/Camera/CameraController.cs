using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private float SmoothTime = 0f;
    [SerializeField]
    private Vector3 Offset;



    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 vel = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, GetPosition(), ref vel, SmoothTime);
    }

    private Vector3 GetPosition()
    {
        Vector3 offset = Offset.x * Player.right + Offset.y * Player.up + Offset.z * Player.forward;
        return Player.position + offset;
    }
}
