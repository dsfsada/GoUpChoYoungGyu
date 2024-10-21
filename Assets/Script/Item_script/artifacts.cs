using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class Artifacts : MonoBehaviour
{
    public int[,] isEquipq = new int[3, 12]; //해당 아이템이 장착 되었는가?

    private Statuse statuseScript;   //스테이터스 스크립트 선언

    private void Start()
    {
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();  //스테이터스 스크립트 불러오기
    }

    public void ResetEquip() //아이템 장착 초기화 
    {
        for (int i = 0; i < 3; i++)
        {
            statuseScript.artifacts[i, 0] = -1;
        }
        isEquipq = new int[3,12];
    }

    public void ClearEquip(int kind, int num) //아이템 장착 해제
    {
        int[,] value = statuseScript.artifacts;
        for (int i = 0; i < 3; i++)
        {
            if (value[i, 0] == kind && value[i, 1] == num)
            {
                statuseScript.artifacts[i, 0] = -1;         //아이템 종류 해제
                statuseScript.artifacts[i, 1] = 0;          //아이템의 번호 해제
            }
        }
    }

    private int CheckArtifacts(int _kind) // 아이템장착할 자리 찾기 ( 있으면 해당 자리 없으면 -1 반환
    {

        int[,] value = statuseScript.artifacts;
                                        //value[_kind, 1]는 _kind의 맞는 현재 장착되어 있는 장비의 번호
        if (value[_kind, 0] != -1 && isEquipq[_kind, value[_kind, 1]] == 1)  //(아이템을 종류별로)장착되어 있는지 확인하는거  (0,0) : 공격, (1, 0) : 체력, (2, 0) : 크리 && isEquipq[_kind, value[_kind, 1]] -> 이게 지금 장착되어있는 장비인지 확인
        {
            //만약 아이템이 장작되어 있을 시 인벤토리 스크립트쪽에 ItemEnforce_GameObjectSlot을 참조하여,
            //해당 번호의 ItemEnforce스크립트를 실행하여 !!기존 장착되어 있는 장비를 해제 시킴!!
            GameObject.Find("Inventory").GetComponent<Inventory>().ItemEnforce_GameObjectSlot[_kind*12 + value[_kind, 1]].GetComponent<ItemEnforce>().EquipEffect(); 
        }
        return _kind;
    }
        // 밑으로는 죄다 아이템 장착 관련


    public void Equipq(int _kind, int _num)
    {
        if (isEquipq[_kind, _num] == 0)
        {
            int i = CheckArtifacts(_kind);
            if (i != -1)
            {
                statuseScript.artifacts[i, 0] = _kind;          //아티팩트의 종류
                statuseScript.artifacts[i, 1] = _num;           //아티팩트의 번호, 기본 능력치 수치
                statuseScript.StatuseUpdate();

                isEquipq[_kind, _num] = 1;
            }
        }
        else
        {
            ClearEquip(_kind, _num);            //아이템 장착 해제
            statuseScript.StatuseUpdate();
            isEquipq[_kind, _num] = 0;
        }
    }
    //------------------------

    //아이템 장착하려면 장착되어있는 다른 아이템을 해제해야 함(귀찮), 위에 코드는 다른 장비 장착시 현재 장착되어 있는 장비 해제시키는 코드임
    /* private int checkartifacts(int _kind) // 아이템장착할 자리 찾기 ( 있으면 해당 자리 없으면 -1 반환
     {

         int[,] value = statuseScript.artifacts;

         if (value[_kind, 0] == -1)  //아이템 종류별로 장착되어 있는지 확인하는거  (0,0) : 공격, (1, 0) : 체력, (2, 0)
         {
             return _kind;
         }
         return -1;
     }
     // 밑으로는 죄다 아이템 장착 관련

     public void Equipq(int _kind, int _num)
     {
         if (isEquipq[_kind, _num] == 0)
         {
             int i = checkartifacts(_kind);
             if (i != -1)
             {
                 statuseScript.artifacts[i, 0] = _kind;          //아티팩트의 종류
                 statuseScript.artifacts[i, 1] = _num;           //아티팩트의 번호, 기본 능력치 수치
                 statuseScript.StatuseUpdate();

                 isEquipq[_kind, _num] = 1;
             }
         }
         else
         {
             clearequip(_kind, _num);
             statuseScript.StatuseUpdate();
             isEquipq[_kind, _num] = 0;
         }
     }
     //------------------------*/


}