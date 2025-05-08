using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public float spawnRadius = 5f;
    float timer;
    int level;
    float spawnTime;


    private void SetSpawnTime()
    {
        spawnTime = Random.Range(0.7f, 1.5f);
    }
    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        level = Random.Range(0, Spawner.instance.spawnData.Length);

        if (timer > spawnTime)
        {
            timer = 0;
            SetSpawnTime();
            Spawn();
        }
    }

    void Spawn()
    {
        Vector2 offset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPos = gameObject.transform.position + (Vector3)offset;

        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPos;
        enemy.GetComponent<Monster>().Init(Spawner.instance.spawnData[level]);
    }
}
