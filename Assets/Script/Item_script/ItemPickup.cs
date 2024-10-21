using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//�� ��ũ��Ʈ�� ������Ʈ(������)�� �÷��̾ �浹�� ������ ����
public class ItemPickup : MonoBehaviour
{
    public int kind, number;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �±װ� "Player"���� Ȯ��
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum[kind, number] += 1;

            //Inventory.Instance.GetAnItem(itemId);            //������ id���� �κ��丮 ��ũ��Ʈ�� ����, kind�� number�� �ο�
            Inventory.Instance.ItemLcation(kind, number);   //�������� ����,number ���ؼ� ��ġ�� ��ư Ȱ��ȭ
            Destroy(gameObject);                            // �ڽ��� ���� ������Ʈ�� �ı�
        }
    }


}