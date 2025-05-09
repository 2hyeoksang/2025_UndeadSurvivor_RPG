using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float playerRange = 10f;

    public RuntimeAnimatorController[] animeCon;
    public Rigidbody2D target;

    bool isLive = true;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anime;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    WaitForSeconds waitsec;

    private void Awake()
    { 
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anime = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        waitsec = new WaitForSeconds(0.2f);
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive || !target || anime.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.linearVelocity = Vector2.zero;
    }

    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive) 
            return;

        if (target != null)
        {
            spriter.flipX = target.position.x < rigid.position.x;
        }

        Vector3 myPos = transform.position;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        float curDiff = Vector3.Distance(myPos, playerPos);

        if (curDiff <= playerRange)
        {
            target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        }
        else
        {
            target = null;
        }
    }

    private void OnEnable()
    {
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anime.SetBool("Dead", false);
        health = maxHealth;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0)
        {
            anime.SetTrigger("Hit");
        }

        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anime.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerator KnockBack()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;

        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 1.5f, ForceMode2D.Impulse);

        yield return waitsec;

        rigid.linearVelocity = Vector2.zero;
        rigid.bodyType = RigidbodyType2D.Kinematic;
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }

    public void Init(SpawnData data)
    {
        anime.runtimeAnimatorController = animeCon[data.sprtieType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
}
