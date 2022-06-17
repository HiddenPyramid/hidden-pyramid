using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class CameraController : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private float SmoothTime = 0.3f;
    [SerializeField] private float dampMinDistance = 0.2f;
    [SerializeField] private float regularSmoothTime = 0.1f;

    [SerializeField] private Vector3 Offset;
    private Vector3 lastValidOffset;
    public bool yBlocked = true;
    public float [] yBlockPositions = {77.08f, 20f};
    public int currentYBlockIndex = 0;

    public bool cameraShaking = false;
    public bool cameraShakingNoDamage = false;

    private Vector3 cameraShakeVector1 = new Vector3(-1.5f, 0.6f, 1.0f);
    private Vector3 cameraShakeVector2 = new Vector3(2.5f, -0.50f, -0.1f);
    private Vector3 cameraShakeVector3 = new Vector3(-1.5f, -0.3f, 1.0f);

    public float shakeInterval = 0.15f;
    public Animator curtainAnimator;

    private IEnumerator lastSmoothCoroutine = null;

    private void Start() 
    {
        lastValidOffset = Offset;
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
        transform.position = Vector3.SmoothDamp(transform.position, GetPosition(), ref vel, regularSmoothTime);
        //transform.position = GetPosition();
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
        if (lastSmoothCoroutine != null) StopCoroutine(lastSmoothCoroutine);
        lastSmoothCoroutine = SmoothOffsetChange(newOffset);
        StartCoroutine(lastSmoothCoroutine);
    }

    private IEnumerator SmoothOffsetChange(Vector3 newOffset)
    {
        Vector3 oldOffset = new Vector3(this.Offset.x, this.Offset.y, this.Offset.z);
        Vector3 vel = Vector3.zero;
        while (NotNearNewOffset(newOffset))
        {
            this.Offset = Vector3.SmoothDamp(this.Offset, newOffset, ref vel, SmoothTime);
            yield return new WaitForEndOfFrame();
        }
        this.Offset = newOffset;
        lastValidOffset = Offset;
    }

    private bool NotNearNewOffset(Vector3 newOffset)
    {
        return (Offset - newOffset).magnitude > dampMinDistance; 
    }

    public void SwapPositiveXOffset()
    {
        SetOffset(new Vector3(GetPostitiveXOffset(), lastValidOffset.y, lastValidOffset.z));
    }

    public void SwapNegativeXOffset()
    {
        SetOffset(new Vector3(GetNegativeXOffset(), lastValidOffset.y, lastValidOffset.z));
    }

    private float GetPostitiveXOffset()
    {
        return Mathf.Abs(this.lastValidOffset.x);
    }

    private float GetNegativeXOffset()
    {
        return - Mathf.Abs(this.lastValidOffset.x);
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
