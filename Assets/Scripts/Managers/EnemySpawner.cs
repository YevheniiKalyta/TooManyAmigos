using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private float timeToSpawn;
    [SerializeField] private float minTimeToSpawn = 0.3f;
    [SerializeField] private float spawRadius;
    [SerializeField] private float navMeshDistance = 2f;

    private float tts;
    private bool spawn = false;
    Coroutine spawning;
    public bool Spawn
    {
        get { return spawn; }
        set
        {
            spawn = value;
            if (spawn && spawning == null)
                spawning = StartCoroutine(SpawnEnemy());
            else
            {
                StopAllCoroutines();
                spawning = null;
            }
        }
    }

    private void Awake()
    {
        PlayerManager.OnPlayerDeath += () => Spawn = false;
    }
    public Vector3 RandomPointInBounds()
    {
        Vector3 randomDirection = Random.insideUnitCircle.normalized * spawRadius;
        Vector3 randomWorldPosition = new Vector3(randomDirection.x, 1, randomDirection.y) + GameManager.Instance.PlayerTransform.position;
        if (NavMesh.SamplePosition(randomWorldPosition, out NavMeshHit navMeshHit, navMeshDistance, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        else
        {
           return RandomPointInBounds();
        }
    }

    public IEnumerator SpawnEnemy()
    {
        spawn = true;
        tts = timeToSpawn;
        while (spawn)
        {
            ObjectPooler.Instance.GetFromPoolAtPosition(PooledObjectType.Enemy, RandomPointInBounds());
            yield return new WaitForSeconds(tts);
            tts = Mathf.Clamp(tts-0.005f, minTimeToSpawn, timeToSpawn);
        }
    }
    public void ToggleSpawn(bool on)
    {
        Spawn = on;
    }
}
