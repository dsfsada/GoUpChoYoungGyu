using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

//인벤토리 내 슬롯의 아이템 저장
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private int kind = 3, num = 12;            //kind 0무기, 1방어구, 2장신구

    [Header("gameobjec")]
    public GameObject Obejct_ItemUpgrade;
    public GameObject[] ItemEnforce_GameObjectSlot;     //업그레이드 슬롯의 setActive관련 쓸 문장

    [Header("Transform")]
    public Transform Tf_weaponContainer;                //slot의 부모객체(아이템)
    public Transform Tf_ArmorContainer;                //slot의 부모객체(아이템)
    public Transform Tf_AccContainer;                //slot의 부모객체(아이템)

    public Transform Tf_ItemUpgrade;                    //slot의 부모객체(아이템 강화 창)

    [Header("Script")]
    public InventorySlot[] Weapon_Slot;          //장비슬롯들
    public InventorySlot[] Armor_Slot;          //장비슬롯들
    public InventorySlot[] Acc_Slot;          //장비슬롯들

    public ItemEnforce[] ItemEnforce_Slot;              //업그레이드 슬롯들

    


    void Start()
    {
        Instance = this;                                         //외부에서 인벤토리 스크립트를 쉽게 참조하게 만들기 위한 코드
        Weapon_Slot = Tf_weaponContainer.GetComponentsInChildren<InventorySlot>();     //무기의 하위 슬롯들이 다 slots의 배열로 저장
        Armor_Slot = Tf_ArmorContainer.GetComponentsInChildren<InventorySlot>();     //방어구의 하위 슬롯들이 다 slots의 배열로 저장
        Acc_Slot = Tf_AccContainer.GetComponentsInChildren<InventorySlot>();     //장신구의 하위 슬롯들이 다 slots의 배열로 저장

        ItemEnforce_Slot = Tf_ItemUpgrade.GetComponentsInChildren<ItemEnforce>();
        ItemEnforce_GameObjectSlot = new GameObject[kind * num];
        OffSetActive();
        SetSlotKindNumber();
    }


    public void ItemLcation(int _kind, int _num)        //아이템 획득시 아이템 버튼 활성화
    {

        if (GameObject.Find("Underbar").GetComponent<UnderBar>().uis[1].activeSelf)
        {
            if (GameObject.Find("EqSelectUi").GetComponent<EqSelectUi>().uiPanels[0].activeSelf && _kind == 0 && Weapon_Slot[_num] != null)
            {
                Weapon_Slot[_num].Additem();                      //아이템 리스트의 있는 아이템 추가
            }

            if(GameObject.Find("EqSelectUi").GetComponent<EqSelectUi>().uiPanels[1].activeSelf && _kind == 1 && Armor_Slot[_num] != null)
            {
                Armor_Slot[_num].Additem();
            }

            if(GameObject.Find("EqSelectUi").GetComponent<EqSelectUi>().uiPanels[2].activeSelf && _kind == 2 && Acc_Slot[_num] != null)
            {
                Acc_Slot[_num].Additem();
            }
        }

    }

    void SetSlotKindNumber()   //인벤토리 슬롯의 kind와 num의 번호 부여
    {
        for (int j = 0; j < num; j++)
        {
            Weapon_Slot[j].SetSlotKindNum(0, j); //무기 타입
            Armor_Slot[j].SetSlotKindNum(1, j); //방어구 타입
            Acc_Slot[j].SetSlotKindNum(2, j); //장신구 타입
        }
    }

    void OffSetActive() //Item_enforce_area쪽 하위 오브젝트들(강화수치 조정) 비활성화 및 액티브 시킴
    {   //나중에 for문 돌려서 type넣을꺼
        for(int i=0; i< ItemEnforce_GameObjectSlot.Length; i++)
        {
            //i+i*type수 해서 slot안에 넣어줄듯
            ItemEnforce_Slot[i].kind = i / 12;
            ItemEnforce_Slot[i].num = i % 12;
            ItemEnforce_GameObjectSlot[i] = Obejct_ItemUpgrade.transform.GetChild(i).gameObject;
            ItemEnforce_GameObjectSlot[i].SetActive(false);
        }
    }

    public void OnSetActive(int _kind, int _num)        //인벤토리 상 아이템 클릭시 클릭시 업그레이드 화면 뜨도록
    { 
        int number = _kind * 12 + _num;
        ItemEnforce_GameObjectSlot[number].SetActive(true);     //번호에 맞는 업그레이드 화면 오픈
    }

    public void EquipItemEffect(int _kind, int _num)   //아이템 장착 버튼 클릭시 인벤토리 슬롯을 통해 장착ui 이미지를 변경
    {
        //인벤토리 슬롯 스크립트의 equipEffect임
        if (_kind == 0 && Weapon_Slot[_num] != null)
        {
            Weapon_Slot[_num].equipEffect();                      //아이템 리스트의 있는 아이템 장착 이미지
        }

        if (_kind == 1 && Armor_Slot[_num] != null)
        {
            Armor_Slot[_num].equipEffect();
        }

        if (_kind == 2 && Acc_Slot[_num] != null)
        {
            Acc_Slot[_num].equipEffect();
        }
    }

    /*
    public void GetAnItem(int _itemId)  //아이템을 습득할 경우(itemPickup 스크립트 참조)
    {
        for (int i = 0; i < Statuse.itemList.Count; i++)                    //스테이터스 스크립트에서 정의해둔 아이템(아티팩트)의 개수만큼 반복
        {
            if (_itemId == Statuse.itemList[i].itemId)                      //아이템 id와 정의해둔 id 같으면 true
            {
                if (inventoryItemList.Count != 0)                           //인벤토리의 아무 값도 없을 경우 오류가 발생하여 만든 if문
                {
                    for (int j = 0; j < inventoryItemList.Count; j++)       //인벤의 들어있는 아이템 개수만큼 반복
                    {
                        if (inventoryItemList[j].itemId == _itemId)         //소지하고 있는 아이템을 먹을경우 그냥 반환
                        {
                            return;
                        }
                    }
                }
                inventoryItemList.Add(Statuse.itemList[i]);                 //먹은 아이템의 id와 같은 아이템을 인벤토리에 추가
                return; // 아이템을 추가한 후 바로 함수를 종료합니다.
            }
        }
        Debug.LogError("해당 id의 아이템을 찾지못함");
    }*/
}