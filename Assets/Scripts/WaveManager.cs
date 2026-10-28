using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private int[] enemiesPerWave = { 5, 10, 15 };
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float timeBetweenWaves = 10f;

    public static WaveManager Instance { get; private set; }

    public int CurrentWave { get; private set; }
    public int TotalWaves => enemiesPerWave.Length;
    public int EnemiesKilled { get; private set; }
    public int EnemiesInWave { get; private set; }
    public int EnemiesSpawned { get; private set; }
    public bool IsBetweenWaves { get; private set; }
    public float TimeUntilNextWave { get; private set; }
    public int TotalEnemiesKilled { get; private set; }
    public int WavesCompleted { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(PlayWaves());
    }

    private IEnumerator PlayWaves()
    {
        TotalEnemiesKilled = 0;
        WavesCompleted = 0;

        for(int i = 0; i < enemiesPerWave.Length; i++)
        {
            CurrentWave = i + 1;
            EnemiesInWave = enemiesPerWave[i];
            EnemiesKilled = 0;
            EnemiesSpawned = 0;

            while(EnemiesSpawned < EnemiesInWave)
            {
                if(SpawnEnemy()) EnemiesSpawned++;

                yield return new WaitForSeconds(spawnInterval);
            }

            yield return new WaitUntil(() => EnemiesKilled >= EnemiesInWave);
            WavesCompleted++;

            if(i < enemiesPerWave.Length - 1)
            {
                IsBetweenWaves = true;
                TimeUntilNextWave = timeBetweenWaves;

                while(TimeUntilNextWave > 0f)
                {
                    TimeUntilNextWave -= Time.deltaTime;
                    yield return null;
                }

                TimeUntilNextWave = 0f;
                IsBetweenWaves = false;
            }
        }
    }

    private bool SpawnEnemy()
    {
        int index = Random.Range(0, spawnPoints.Length);

        Transform pointA = spawnPoints[index];
        Transform pointB = spawnPoints[(index + 1) % spawnPoints.Length];

        Vector3 position = Vector3.Lerp(pointA.position, pointB.position, Random.value);

        if (NavMesh.SamplePosition(position, out NavMeshHit hit, 3f, NavMesh.AllAreas))
        {
            Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            return true;
        }

        return false;
    }

    public void RegisterEnemyDeath()
    {
        EnemiesKilled++;
        TotalEnemiesKilled++;
    }
}
