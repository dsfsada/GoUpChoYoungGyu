using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    private EnemySpawn enemySpawn;
    private DropItem dropItem;

    int randAtk;
    public float health, atk, attackCool;

    private Player player;
    private Rigidbody2D rb;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawn = GameObject.FindWithTag("Respawn").GetComponent<EnemySpawn>();            //나중에 enemySpawner.BossLv 를 이용해서 보스의 능력치나 드롭 아이템의 범위 설정
        dropItem = GetComponent<DropItem>();    //DropItem 스크립트 참조

        SetBossStatuse();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && player == null)
        {
            player = col.GetComponent<Player>();
            rb = player.GetComponent<Rigidbody2D>();
            StartCoroutine(AttackPlayer());
        }
        else
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(attackCool);

        while (player.health > 0 && health > 0) // 적의 체력이 0보다 클 때까지 반복
        {
            randAtk = Random.Range(0, 10);

            animator.SetBool("atk2", true);

            if(randAtk > 5) // 밀치기 이후 추가공격 방지
            {
                break;
            }

            yield return new WaitForSeconds(attackCool);
        }
        
    }

    public void Attack()  //애니메이션에 도중 공격을 성공시키기 위한 함수
    {
        if (rb != null && randAtk > 5)
        {
            rb.velocity = new Vector2(-2, 0);
        }
        player.health -= atk;
        FloatingText.Instance.ShowFloatingText(player.transform, -0.2f, 0.8f, atk.ToString(), new Color(1f, 0f, 0f));      //플로팅텍스트

        if (player.health <= 0)                          // 플레이어가 죽을때까지 반복
        {
            player.isFadeCoroutineActive = false;               // 죽었으니 while문 탈출
        }
    }

    public void AnimationOffAtk()       //애니메이션 끝날때 호출
    {       
        //animator.SetBool("atk", false);
        animator.SetBool("atk2", false);
    }

    public void DeadEvent()
    {
        rb.velocity = new Vector2(-2, 0);
        animator.SetBool("dead", true);
        //animator.SetBool("atk", false);
        animator.SetBool("atk2", false);
    }

    public void DestroyEnemy()      //dead애니메이션 끝날때 호출
    {
        dropItem.DropItems(enemySpawn.bossLv);       //아이템 드롭
        animator.SetBool("dead", false);
        Destroy(this.gameObject);
    }

    public void SetBossStatuse()
    {
        if(enemySpawn.bossLv == 1) 
        {
            health = 1200;
            atk = 1000;
        }
        else if(enemySpawn.bossLv == 2) 
        {
            health = 31750;
            atk = 31250;
        }
        else if(enemySpawn.bossLv == 3)
        {
            health = 845250;
            atk = 843750;
        }
    }
}
