using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private int followDelay;
    [SerializeField]
    private float followDistance;
    [SerializeField]
    private float speed;

    private List<Vector3> storedPositions;
    public Animator animator;


    void Awake()
    {
        storedPositions = new List<Vector3>();
    }

    void Update()
    {
        storedPositions.Add(player.position);
        if (storedPositions.Count > followDelay)
        {
            var nextPos = storedPositions[0] + player.right * followDistance;
            transform.position = Vector3.Lerp(transform.position, nextPos, Time.deltaTime * speed); 
            storedPositions.RemoveAt(0); 

            animator.SetFloat("Speed", (transform.position - nextPos).magnitude);
        }
        transform.rotation = player.rotation;
    }
}
