using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private bool fov;
    [SerializeField]
    private float FovAngle;
    [SerializeField]
    LayerMask playerLayer;

    private List<Transform> playersDetected;

    public List<Transform> PlayersDetected { get => playersDetected;}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;

        var dir = Quaternion.AngleAxis(FovAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, dir * radius);
        var dir2 = Quaternion.AngleAxis(-FovAngle / 2, transform.forward) * transform.right;
        Gizmos.DrawRay(transform.position, dir2 * radius);
        Gizmos.color = Color.white;
    }
    private void Start()
    {
        playersDetected = new List<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        DetectPlayers();
    }

    private void DetectPlayers()
    {
        var players = Physics.OverlapSphere(transform.position, radius, playerLayer);
        foreach (var item in players)
        {
            if (fov)
            {
                IsInFov(item.transform);
            }
            else
                PlayersDetected.Add(item.transform);
        }
    }

    private void IsInFov(Transform player)
    {
        Vector3 dir = player.position - transform.position;
        float angle = Vector2.Angle(transform.right, dir);

        if (angle < FovAngle / 2 || angle < -FovAngle / 2)
        {
            PlayersDetected.Add(transform);

        }
    }
}

