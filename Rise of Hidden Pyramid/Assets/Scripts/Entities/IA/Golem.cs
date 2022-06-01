using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour, ITakeDamage
{
    public AIPatrol aiPatrol;

    public void TakeDamage(float dmg)
    {
        aiPatrol.TakeDamage(dmg);
    }

    protected virtual void Move(){}
    protected bool CheckDie(float Health)
    {
        if (Health <= 0){
            return true;
        }
        return false;
    }
}
