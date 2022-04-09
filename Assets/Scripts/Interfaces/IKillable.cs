using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    public void Die();
    public void TakeDamage(int damageAmount,ContactPoint contactPoint = new ContactPoint());
}
