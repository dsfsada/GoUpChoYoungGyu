using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public float maxHealth, minHealth, health, dropItem, atk, attackCool, floor, calcHealth, calcAtk;
    public Animator animator;
    private Statuse statuseObj;
    private Player player;

    //public MonoBehaviour ShadowScrpit;  //������ �׸��� ���ֱ� ���� �ڵ�

    // Start is called before the first frame update
    void Start()
    {
        statuseObj = GameObject.Find("Statuse").GetComponent<Statuse>();
        floor = statuseObj.floor;
        calcHealth = floor + (floor * floor / 40) * ((int)floor / 500 > 0 ? (int)floor / 500 * 5 : 1);   // 500������ ���� ���� ��·� ����
        calcAtk = (floor * floor / 40) * ((int)floor / 500 > 0 ? (int)floor / 500 * 5 : 1);              // 500������ ���� ���� ��·� ����
        health = (int)Random.Range(minHealth + calcHealth, maxHealth + calcHealth);                 // ���� ü���� minHealth + floor ~ maxHealth + floor �� ����
        atk = (int)Random.Range(atk + calcAtk, atk + 1 + calcAtk);                                  // ���� ���ݷ��� atk + floor ~ +1 �� ����
    }

    public void DeadEvent()
    {
        animator.SetBool("die", true);
        animator.SetBool("attack", false);
        statuseObj.coin += floor;     // �� ��ŭ ���� ���
    }

    private void OnTriggerEnter2D(Collider2D col)   // ĳ���Ͷ� �ε����� �۵�
    {
        if(col.CompareTag("Player"))
        {
            player = col.GetComponent<Player>();    // �÷��̾������� ����
            StartCoroutine(AttackPlayer());      // AttackPlayer�Լ� �۵�
        }
    }

    private IEnumerator AttackPlayer()        // ���� player�� ������ ��ƾ
    {
        yield return new WaitForSeconds(attackCool/2);      // ������ ���ÿ� ���� �������� ���ÿ� ���� ���� ����
        while (player.health > 0 && health > 0)             // ���� ü���� 0���� Ŭ ������ �ݺ�
        {
            animator.SetBool("attack", true);

            yield return new WaitForSeconds(0.5f);

            player.health -= atk;                           // �� ���ݷ¸�ŭ �÷��̾� ������
            FloatingText.Instance.ShowFloatingText(player.transform, -0.2f, 0.8f, atk.ToString(), new Color(1f, 0f, 0f));      //�÷����ؽ�Ʈ

            if(player.health <= 0)                          // �÷��̾ ���������� �ݺ�
            {
                statuseObj.GetComponent<Statuse>().floor--; // �÷��̾ ������ �� �϶�
                player.isFadeCoroutineActive = false;               // �׾����� while�� Ż��
                player.velocity = new(0,0);
            }

            yield return new WaitForSeconds(attackCool);    // ���� ��
        }
    }

    public void OffDieAnimation()
    {
        animator.SetBool("die", false);
        Destroy(this.gameObject);
    }

    public void OffAttackAnimation()
    {
        animator.SetBool("attack", false);          //�ִϸ����� �ʿ� �����Ǿ�����
    }
}