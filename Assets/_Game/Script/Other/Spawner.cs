using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Positions")]
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;

    [Header("Spawn Config")]
    [SerializeField] private float startDelay = 2f;      
    [SerializeField] private float minDelay = 0.5f;       
    [SerializeField] private float difficultyIncreaseTime = 10f;

    private float currentDelay;
    private Coroutine spawnCoroutine;
    private Coroutine difficultyCoroutine;

    private void Start()
    {
        currentDelay = startDelay;
        spawnCoroutine = StartCoroutine(SpawnRoutine());
        difficultyCoroutine = StartCoroutine(IncreaseDifficulty());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnRandomObstacle();
            yield return new WaitForSeconds(currentDelay);
        }
    }

    private void SpawnRandomObstacle()
    {
        int rand = Random.Range(0, 2); // 0: left, 1: right
        if (rand == 0)
        {
            //Instantiate(obstacleLeft, leftSpawnPoint.position, Quaternion.identity);
            SimplePool.Spawn<Obstacle>(PoolType.ObstacleL, leftSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            //Instantiate(obstacleRight, rightSpawnPoint.position, Quaternion.identity);
            SimplePool.Spawn<Obstacle>(PoolType.ObstacleR, rightSpawnPoint.position, Quaternion.identity);
        }
    }

    private IEnumerator IncreaseDifficulty()
    {
        while (currentDelay > minDelay)
        {
            yield return new WaitForSeconds(difficultyIncreaseTime);
            currentDelay -= 0.2f; // Giảm delay → spawn nhanh hơn
            currentDelay = Mathf.Max(currentDelay, minDelay);
        }
    }

    public void StopCor()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }

        if (difficultyCoroutine != null)
        {
            StopCoroutine(difficultyCoroutine);
            difficultyCoroutine = null;
        }
    }

}
