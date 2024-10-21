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
        enemySpawn = GameObject.FindWithTag("Respawn").GetComponent<EnemySpawn>();            //���߿� enemySpawner.BossLv �� �̿��ؼ� ������ �ɷ�ġ�� ��� �������� ���� ����
        dropItem = GetComponent<DropItem>();    //DropItem ��ũ��Ʈ ����

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

        while (player.health > 0 && health > 0) // ���� ü���� 0���� Ŭ ������ �ݺ�
        {
            randAtk = Random.Range(0, 10);

            animator.SetBool("atk2", true);

            if(randAtk > 5) // ��ġ�� ���� �߰����� ����
            {
                break;
            }

            yield return new WaitForSeconds(attackCool);
        }
        
    }

    public void Attack()  //�ִϸ��̼ǿ� ���� ������ ������Ű�� ���� �Լ�
    {
        if (rb != null && randAtk > 5)
        {
            rb.velocity = new Vector2(-2, 0);
        }
        player.health -= atk;
        FloatingText.Instance.ShowFloatingText(player.transform, -0.2f, 0.8f, atk.ToString(), new Color(1f, 0f, 0f));      //�÷����ؽ�Ʈ

        if (player.health <= 0)                          // �÷��̾ ���������� �ݺ�
        {
            player.isFadeCoroutineActive = false;               // �׾����� while�� Ż��
        }
    }

    public void AnimationOffAtk()       //�ִϸ��̼� ������ ȣ��
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

    public void DestroyEnemy()      //dead�ִϸ��̼� ������ ȣ��
    {
        dropItem.DropItems(enemySpawn.bossLv);       //������ ���
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
