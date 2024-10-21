using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//이 스크립트의 오브젝트(아이템)와 플레이어가 충돌시 아이템 슥듭
public class ItemPickup : MonoBehaviour
{
    public int kind, number;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 객체의 태그가 "Player"인지 확인
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum[kind, number] += 1;

            //Inventory.Instance.GetAnItem(itemId);            //아이템 id값을 인벤토리 스크립트의 보냄, kind와 number을 부여
            Inventory.Instance.ItemLcation(kind, number);   //아이템의 종류,number 비교해서 위치에 버튼 활성화
            Destroy(gameObject);                            // 자신의 게임 오브젝트를 파괴
        }
    }


}