using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Entity, IKillable
{
    public static Action OnPlayerDeath;
    [SerializeField] CharacterController characterController;
    [SerializeField] CapsuleCollider capsuleCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            TakeDamage(999,collision.GetContact(0));
        }
    }

    public override void Die()
    {
        base.Die();
        OnPlayerDeath?.Invoke();
        characterController.detectCollisions = false;
        capsuleCollider.direction = 2;
    }

    public void ResetPlayer()
    {
        isDead = false;
        animator.Rebind();
        animator.Update(0f);
        characterController.detectCollisions = true;
        capsuleCollider.direction = 1;
    }

}
