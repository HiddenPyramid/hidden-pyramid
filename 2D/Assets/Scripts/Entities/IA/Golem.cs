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

    protected List<Transform> playersDetected;
    protected PlayerDetection detection;

    // Start is called before the first frame update
    void Awake()
    {
        detection = GetComponent<PlayerDetection>();
    }

    protected abstract void Move();

    public void TakeDamage(float dmg)
    {
        Health -= dmg;
    }
    protected void CheckDie()
    {
        if (Health <= 0)
            Destroy(gameObject);
    }
}
