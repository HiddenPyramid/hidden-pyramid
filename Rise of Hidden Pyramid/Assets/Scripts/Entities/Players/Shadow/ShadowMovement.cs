using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    public float[]  yBlockPositions = {77.2f, -1.135f};
    private int indexY = 0;
    public bool fixedY = true;

    void FixedUpdate()
    {
        if (fixedY)
            GoToFixedY();
    }

    private void GoToFixedY()
    {
        transform.position = new Vector3(transform.position.x, yBlockPositions[indexY], transform.position.z);
        Debug.Log(yBlockPositions[indexY]);
    } 

    public void NextYPosition()
    {
        indexY ++;
        if (indexY >= yBlockPositions.Length)
            indexY = 0;
    }

    public void FixY()
    {
        this.fixedY = true;
    }

    public void UnfixY()
    {
        this.fixedY = false;
    } 
}
