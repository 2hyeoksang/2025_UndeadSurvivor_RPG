using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

    float timer;
    Player player;

    private void Awake()
    {
        //player = GameManager.instance.player;
    }

    private void Start()
    {
        player = GameManager.instance.player;
        Init();
    }

    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            default:
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }
    }

    public void Init()  // �ӽ�
    {
        switch (id)
        {
            case 0:
                speed = 150f;
                Batch();
                break;

            default:
                speed = 0.3f;
                break;
        }
    }

    void Batch()
    {
        for (int index = 0; index < count; ++index)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }

            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().Init(damage, -100, Vector3.zero);
        }
    }



    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, count, dir);

    }
}
