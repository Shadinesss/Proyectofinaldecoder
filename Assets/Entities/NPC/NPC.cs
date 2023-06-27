using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    //Generic Methods
    protected virtual void LookAtTarget()
    {
    }
    protected virtual void ChaseTarget()
    {
    }
    protected virtual void AttackTarget()
    {
    }
    //protected virtual void EscapeFromTarget()
    //{
    //}
    //protected virtual void HealWhenNotInCombat()
    //{
    //}
    protected virtual void Die()
    {
    }
    protected virtual void TakeDamage()
    {
    }
}

