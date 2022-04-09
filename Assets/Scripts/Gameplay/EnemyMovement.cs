using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyManager))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed;
    private EnemyManager enemyManager;


    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }
    void Update()
    {
        if (!enemyManager.IsDead && enemyManager.CanWalk)
        {
            Vector3 playerPosition = GameManager.Instance.PlayerTransform.position;
            if (enemyManager.NavigationMeshAgent.isOnNavMesh)
            {
                enemyManager.NavigationMeshAgent.destination = playerPosition;
            }

            transform.LookAt(playerPosition);
        }
    }
}
