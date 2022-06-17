using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private int followDelay;
    [SerializeField]
    private float followDistance;
    [SerializeField]
    private float speed;

    private bool inversed = false;

    private List<Vector3> storedPositions;
    public Animator animator;


    void Awake()
    {
        storedPositions = new List<Vector3>();
    }

    private void Start() 
    {
        player = FindObjectOfType<PlayerManager>().GetPlayer().gameObject.transform;
        FindObjectOfType<PlayerManager>().playerChangeEvent += GetCurrentPlayer;
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

    private void GetCurrentPlayer()
    {
        player = FindObjectOfType<PlayerManager>().GetPlayer().gameObject.transform;
    }

    public void Inverse()
    {
        if (!inversed)
        {
            inversed = true;
            this.followDistance *= -1;
        }
    }

    public void Deinverse()
    {
        if (inversed)
        {
            inversed = false;
            this.followDistance *= -1;
        }
    }
}
