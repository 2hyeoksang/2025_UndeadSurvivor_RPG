using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float range = 10f;

    private void Awake()
    {
        instance = this;
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    private void Start()
    {
        StartCoroutine(ActivateSpawnPoint());
    }

    IEnumerator ActivateSpawnPoint()
    {
        while (true)
        {
            Vector3 playerPos = GameManager.instance.player.transform.position;

            for (int i = 1; i < spawnPoint.Length; i++)
            {
                Transform point = spawnPoint[i];
                Vector3 pointPos = point.position;
                float curDiff = Vector3.Distance(pointPos, playerPos);

                point.gameObject.SetActive(curDiff < range);
            }
            yield return new WaitForSeconds(0.5f);
        }
            
    }

    //private void Update()
    //{
    //    if (!GameManager.instance.isLive)
    //        return;

    //    timer += Time.deltaTime;


    //    if (timer > spawnTime)
    //    {
    //        for (int i = 1; i < spawnPoint.Length; i++)
    //        {
    //            Transform point = spawnPoint[i];

    //            if (!point.gameObject.activeSelf)
    //                continue;

    //            timer = 0;
    //            SetSpawnTime();
    //            Spawn();
    //        }
    //    }
    //}

    //void Spawn()
    //{
    //    GameObject enemy = GameManager.instance.pool.Get(0);
    //    enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    //    enemy.GetComponent<Monster>().Init(spawnData[level]);
    //}
}


[System.Serializable]
public class SpawnData
{
    public int sprtieType;
    public float health;
    public float speed;
}