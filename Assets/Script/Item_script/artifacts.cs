using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class Artifacts : MonoBehaviour
{
    public int[,] isEquipq = new int[3, 12]; //�ش� �������� ���� �Ǿ��°�?

    private Statuse statuseScript;   //�������ͽ� ��ũ��Ʈ ����

    private void Start()
    {
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();  //�������ͽ� ��ũ��Ʈ �ҷ�����
    }

    public void ResetEquip() //������ ���� �ʱ�ȭ 
    {
        for (int i = 0; i < 3; i++)
        {
            statuseScript.artifacts[i, 0] = -1;
        }
        isEquipq = new int[3,12];
    }

    public void ClearEquip(int kind, int num) //������ ���� ����
    {
        int[,] value = statuseScript.artifacts;
        for (int i = 0; i < 3; i++)
        {
            if (value[i, 0] == kind && value[i, 1] == num)
            {
                statuseScript.artifacts[i, 0] = -1;         //������ ���� ����
                statuseScript.artifacts[i, 1] = 0;          //�������� ��ȣ ����
            }
        }
    }

    private int CheckArtifacts(int _kind) // ������������ �ڸ� ã�� ( ������ �ش� �ڸ� ������ -1 ��ȯ
    {

        int[,] value = statuseScript.artifacts;
                                        //value[_kind, 1]�� _kind�� �´� ���� �����Ǿ� �ִ� ����� ��ȣ
        if (value[_kind, 0] != -1 && isEquipq[_kind, value[_kind, 1]] == 1)  //(�������� ��������)�����Ǿ� �ִ��� Ȯ���ϴ°�  (0,0) : ����, (1, 0) : ü��, (2, 0) : ũ�� && isEquipq[_kind, value[_kind, 1]] -> �̰� ���� �����Ǿ��ִ� ������� Ȯ��
        {
            //���� �������� ���۵Ǿ� ���� �� �κ��丮 ��ũ��Ʈ�ʿ� ItemEnforce_GameObjectSlot�� �����Ͽ�,
            //�ش� ��ȣ�� ItemEnforce��ũ��Ʈ�� �����Ͽ� !!���� �����Ǿ� �ִ� ��� ���� ��Ŵ!!
            GameObject.Find("Inventory").GetComponent<Inventory>().ItemEnforce_GameObjectSlot[_kind*12 + value[_kind, 1]].GetComponent<ItemEnforce>().EquipEffect(); 
        }
        return _kind;
    }
        // �����δ� �˴� ������ ���� ����


    public void Equipq(int _kind, int _num)
    {
        if (isEquipq[_kind, _num] == 0)
        {
            int i = CheckArtifacts(_kind);
            if (i != -1)
            {
                statuseScript.artifacts[i, 0] = _kind;          //��Ƽ��Ʈ�� ����
                statuseScript.artifacts[i, 1] = _num;           //��Ƽ��Ʈ�� ��ȣ, �⺻ �ɷ�ġ ��ġ
                statuseScript.StatuseUpdate();

                isEquipq[_kind, _num] = 1;
            }
        }
        else
        {
            ClearEquip(_kind, _num);            //������ ���� ����
            statuseScript.StatuseUpdate();
            isEquipq[_kind, _num] = 0;
        }
    }
    //------------------------

    //������ �����Ϸ��� �����Ǿ��ִ� �ٸ� �������� �����ؾ� ��(����), ���� �ڵ�� �ٸ� ��� ������ ���� �����Ǿ� �ִ� ��� ������Ű�� �ڵ���
    /* private int checkartifacts(int _kind) // ������������ �ڸ� ã�� ( ������ �ش� �ڸ� ������ -1 ��ȯ
     {

         int[,] value = statuseScript.artifacts;

         if (value[_kind, 0] == -1)  //������ �������� �����Ǿ� �ִ��� Ȯ���ϴ°�  (0,0) : ����, (1, 0) : ü��, (2, 0)
         {
             return _kind;
         }
         return -1;
     }
     // �����δ� �˴� ������ ���� ����

     public void Equipq(int _kind, int _num)
     {
         if (isEquipq[_kind, _num] == 0)
         {
             int i = checkartifacts(_kind);
             if (i != -1)
             {
                 statuseScript.artifacts[i, 0] = _kind;          //��Ƽ��Ʈ�� ����
                 statuseScript.artifacts[i, 1] = _num;           //��Ƽ��Ʈ�� ��ȣ, �⺻ �ɷ�ġ ��ġ
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