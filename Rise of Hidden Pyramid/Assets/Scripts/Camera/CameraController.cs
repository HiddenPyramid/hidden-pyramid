using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CameraController : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    private float SmoothTime = 0f;
    [SerializeField]
    private Vector3 Offset;
    public bool yBlocked = true;
    public float yBlockPosition = 78.43f;

    private bool currentSwapped = false;

    private void Start() 
    {
        Player = FindObjectOfType<PlayerManager>().GetPlayer().gameObject.transform;
        FindObjectOfType<PlayerManager>().playerChangeEvent += GetCurrentPlayer;
    }

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
        Vector3 offset;
        offset = Offset.x * Player.right + Offset.y * Player.up + Offset.z * Player.forward;

        if (!yBlocked) 
            return Player.position + offset;
        
        Vector3 positionYBlocked = new Vector3 (Player.position.x, yBlockPosition, Player.position.z);
        return positionYBlocked + offset;
    }

    public Vector3 GetOffset()
    {
        return this.Offset;
    }

    public void SetOffset(Vector3 newOffset)
    {
        this.Offset = currentSwapped ? new Vector3 (   newOffset.x, newOffset.y, newOffset.z) : 
                                       new Vector3 ( - newOffset.x, newOffset.y, newOffset.z);
    }

    public void SwapXOffset()
    {
        this.Offset = new Vector3(- this.Offset.x, 
                                    this.Offset.y, 
                                    this.Offset.z);
        this.currentSwapped = ! this.currentSwapped;
    }

    private void GetCurrentPlayer()
    {
        Player = FindObjectOfType<PlayerManager>().GetPlayer().gameObject.transform;
    }
}
