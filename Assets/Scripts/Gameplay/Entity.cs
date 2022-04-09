using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IKillable
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected Animator animator;
    protected int health;
    protected bool isDead;

    public bool IsDead => isDead;


    protected virtual void OnEnable()
    {
        health = maxHealth;
    }
    public virtual void Die() 
    {
        animator.SetTrigger("Death");
        isDead = true;
    }

    public virtual void TakeDamage(int damageAmount,ContactPoint contactPoint)
    {
        GameObject temp = ObjectPooler.Instance.GetFromPoolAtPosition(PooledObjectType.Blood, contactPoint.point);
        temp.transform.forward = contactPoint.normal;
        health = Mathf.Clamp(health - damageAmount, 0, maxHealth);
        if (health == 0) Die();
    }
}
