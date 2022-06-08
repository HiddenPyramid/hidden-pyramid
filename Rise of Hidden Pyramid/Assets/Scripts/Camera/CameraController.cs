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
    public float [] yBlockPositions = {77.08f, 20f};
    public int currentYBlockIndex = 0;

    private bool currentSwapped = false;
    public bool cameraShaking = false;
    public bool cameraShakingNoDamage = false;

    private Vector3 cameraShakeVector1 = new Vector3(-1.5f, 0.6f, 1.0f);
    private Vector3 cameraShakeVector2 = new Vector3(2.5f, -0.50f, -0.1f);
    private Vector3 cameraShakeVector3 = new Vector3(-1.5f, -0.3f, 1.0f);

    public float shakeInterval = 0.15f;
    public Animator curtainAnimator;

    private void Start() 
    {
        Player = FindObjectOfType<PlayerManager>().GetPlayer().gameObject.transform;
        FindObjectOfType<PlayerManager>().playerChangeEvent += GetCurrentPlayer;
    }

    void Update()
    {
        FollowPlayer();
        if (cameraShaking || cameraShakingNoDamage) CameraShake();
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
        
        Vector3 positionYBlocked = new Vector3 (Player.position.x, yBlockPositions[currentYBlockIndex], Player.position.z);
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

    public void UnblockY()
    {
        this.yBlocked = false;
        currentYBlockIndex ++;
    }

    public void BlockY()
    {
        this.yBlocked = true;
    }

    public void CameraShake()
    {
        if (cameraShaking) curtainAnimator.SetTrigger("takeDamage");
        cameraShaking = false;
        cameraShakingNoDamage = false;
        StartCoroutine(CameraShaking());
    }

    private IEnumerator CameraShaking()
    {
        Vector3 originalOffset = Offset;
        Offset = originalOffset - cameraShakeVector1;
        yield return new WaitForSeconds(shakeInterval);
        Offset = originalOffset - cameraShakeVector2;
        yield return new WaitForSeconds(shakeInterval);
        Offset = originalOffset - cameraShakeVector3;
        yield return new WaitForSeconds(shakeInterval);

        Offset = originalOffset - cameraShakeVector1;
        yield return new WaitForSeconds(shakeInterval);
        Offset = originalOffset - cameraShakeVector2;
        yield return new WaitForSeconds(shakeInterval);
        Offset = originalOffset - cameraShakeVector3;
        yield return new WaitForSeconds(shakeInterval);

        Offset = originalOffset;
    }

    
}
