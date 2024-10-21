using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

//�κ��丮 �� ������ ������ ����
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    private int kind = 3, num = 12;            //kind 0����, 1��, 2��ű�

    [Header("gameobjec")]
    public GameObject Obejct_ItemUpgrade;
    public GameObject[] ItemEnforce_GameObjectSlot;     //���׷��̵� ������ setActive���� �� ����

    [Header("Transform")]
    public Transform Tf_weaponContainer;                //slot�� �θ�ü(������)
    public Transform Tf_ArmorContainer;                //slot�� �θ�ü(������)
    public Transform Tf_AccContainer;                //slot�� �θ�ü(������)

    public Transform Tf_ItemUpgrade;                    //slot�� �θ�ü(������ ��ȭ â)

    [Header("Script")]
    public InventorySlot[] Weapon_Slot;          //��񽽷Ե�
    public InventorySlot[] Armor_Slot;          //��񽽷Ե�
    public InventorySlot[] Acc_Slot;          //��񽽷Ե�

    public ItemEnforce[] ItemEnforce_Slot;              //���׷��̵� ���Ե�

    


    void Start()
    {
        Instance = this;                                         //�ܺο��� �κ��丮 ��ũ��Ʈ�� ���� �����ϰ� ����� ���� �ڵ�
        Weapon_Slot = Tf_weaponContainer.GetComponentsInChildren<InventorySlot>();     //������ ���� ���Ե��� �� slots�� �迭�� ����
        Armor_Slot = Tf_ArmorContainer.GetComponentsInChildren<InventorySlot>();     //���� ���� ���Ե��� �� slots�� �迭�� ����
        Acc_Slot = Tf_AccContainer.GetComponentsInChildren<InventorySlot>();     //��ű��� ���� ���Ե��� �� slots�� �迭�� ����

        ItemEnforce_Slot = Tf_ItemUpgrade.GetComponentsInChildren<ItemEnforce>();
        ItemEnforce_GameObjectSlot = new GameObject[kind * num];
        OffSetActive();
        SetSlotKindNumber();
    }


    public void ItemLcation(int _kind, int _num)        //������ ȹ��� ������ ��ư Ȱ��ȭ
    {

        if (GameObject.Find("Underbar").GetComponent<UnderBar>().uis[1].activeSelf)
        {
            if (GameObject.Find("EqSelectUi").GetComponent<EqSelectUi>().uiPanels[0].activeSelf && _kind == 0 && Weapon_Slot[_num] != null)
            {
                Weapon_Slot[_num].Additem();                      //������ ����Ʈ�� �ִ� ������ �߰�
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

    void SetSlotKindNumber()   //�κ��丮 ������ kind�� num�� ��ȣ �ο�
    {
        for (int j = 0; j < num; j++)
        {
            Weapon_Slot[j].SetSlotKindNum(0, j); //���� Ÿ��
            Armor_Slot[j].SetSlotKindNum(1, j); //�� Ÿ��
            Acc_Slot[j].SetSlotKindNum(2, j); //��ű� Ÿ��
        }
    }

    void OffSetActive() //Item_enforce_area�� ���� ������Ʈ��(��ȭ��ġ ����) ��Ȱ��ȭ �� ��Ƽ�� ��Ŵ
    {   //���߿� for�� ������ type������
        for(int i=0; i< ItemEnforce_GameObjectSlot.Length; i++)
        {
            //i+i*type�� �ؼ� slot�ȿ� �־��ٵ�
            ItemEnforce_Slot[i].kind = i / 12;
            ItemEnforce_Slot[i].num = i % 12;
            ItemEnforce_GameObjectSlot[i] = Obejct_ItemUpgrade.transform.GetChild(i).gameObject;
            ItemEnforce_GameObjectSlot[i].SetActive(false);
        }
    }

    public void OnSetActive(int _kind, int _num)        //�κ��丮 �� ������ Ŭ���� Ŭ���� ���׷��̵� ȭ�� �ߵ���
    { 
        int number = _kind * 12 + _num;
        ItemEnforce_GameObjectSlot[number].SetActive(true);     //��ȣ�� �´� ���׷��̵� ȭ�� ����
    }

    public void EquipItemEffect(int _kind, int _num)   //������ ���� ��ư Ŭ���� �κ��丮 ������ ���� ����ui �̹����� ����
    {
        //�κ��丮 ���� ��ũ��Ʈ�� equipEffect��
        if (_kind == 0 && Weapon_Slot[_num] != null)
        {
            Weapon_Slot[_num].equipEffect();                      //������ ����Ʈ�� �ִ� ������ ���� �̹���
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
    public void GetAnItem(int _itemId)  //�������� ������ ���(itemPickup ��ũ��Ʈ ����)
    {
        for (int i = 0; i < Statuse.itemList.Count; i++)                    //�������ͽ� ��ũ��Ʈ���� �����ص� ������(��Ƽ��Ʈ)�� ������ŭ �ݺ�
        {
            if (_itemId == Statuse.itemList[i].itemId)                      //������ id�� �����ص� id ������ true
            {
                if (inventoryItemList.Count != 0)                           //�κ��丮�� �ƹ� ���� ���� ��� ������ �߻��Ͽ� ���� if��
                {
                    for (int j = 0; j < inventoryItemList.Count; j++)       //�κ��� ����ִ� ������ ������ŭ �ݺ�
                    {
                        if (inventoryItemList[j].itemId == _itemId)         //�����ϰ� �ִ� �������� ������� �׳� ��ȯ
                        {
                            return;
                        }
                    }
                }
                inventoryItemList.Add(Statuse.itemList[i]);                 //���� �������� id�� ���� �������� �κ��丮�� �߰�
                return; // �������� �߰��� �� �ٷ� �Լ��� �����մϴ�.
            }
        }
        Debug.LogError("�ش� id�� �������� ã������");
    }*/
}