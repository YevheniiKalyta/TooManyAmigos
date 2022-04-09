using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : Entity, IKillable
{
    [SerializeField] private Collider enemyCollider;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float corpseTime = 3f;
    private bool canWalk;

    public static Action OnEnemyDeath;


    public bool CanWalk => canWalk;
    public NavMeshAgent NavigationMeshAgent => navMeshAgent;

    protected override void OnEnable()
    {
        base.OnEnable();
        enemyCollider.enabled = true;
        navMeshAgent.enabled = true;
        canWalk = true;
        isDead = false;
        PlayerManager.OnPlayerDeath += Dance;
    }

    private void Dance()
    {
        canWalk = false;
        animator.SetTrigger("Dance");
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.destination = transform.position;
        }

    }

    public override void Die()
    {
        base.Die();
        OnEnemyDeath?.Invoke();
        StartCoroutine(DisableEnemy());
    }

    private IEnumerator DisableEnemy()
    {
        enemyCollider.enabled = false;
        navMeshAgent.enabled = false;
        yield return new WaitForSeconds(corpseTime);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
