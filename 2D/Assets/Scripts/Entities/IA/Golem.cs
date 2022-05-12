using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Golem : MonoBehaviour, ITakeDamage
{
    [SerializeField]
    protected float Health;
    [SerializeField]
    protected float Speed;
    [SerializeField]
    protected bool Chase;
    [SerializeField]
    protected Transform Visuals;

    protected List<Transform> playersDetected;
    protected PlayerDetection detection;
    protected GolemCollision collision;

    // Start is called before the first frame update
    void Awake()
    {
        detection = GetComponent<PlayerDetection>();
        collision = GetComponent<GolemCollision>();
    }

    protected abstract void Move();

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }
    protected bool CheckDie()
    {
        if (Health <= 0)
            return true;
        return false;
    }
}
