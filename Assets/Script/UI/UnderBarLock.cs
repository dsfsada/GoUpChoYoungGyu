using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnderbarLock : MonoBehaviour
{
    public GameObject[] aniGameObject;  //�ִϸ��̼� ���� ������Ʈ
    private Animator[] animator;
    private Statuse statuseScript;

    void Start()
    {
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();
        if (statuseScript.removeBossRestrictions)     //���� ���� �� �ѹ��̶� 200���� �Ѿ����� �̹� �ִϸ��̼��� �ôٴ� ������ ���� �� ������Ʈ�� �ı���
        {
            for (int i = 0; i < aniGameObject.Length; i++)
            {
                Destroy(aniGameObject[i]);    //���� ������Ʈ �ı�
                Destroy(this, 2f);              //�ش� ��ũ��Ʈ �ı�
            }
        }
    }


    public void UnlockBossAnimation()             //�ִϸ��̼� ����
    {
        animator = new Animator[aniGameObject.Length];

        //��� player��ũ��Ʈ���� �ѹ� Ȯ���� �ϱ⿡ �ؿ� if���� ��� �� ��� ����
        if (!statuseScript.removeBossRestrictions && statuseScript.floor >= 200)          //�ִϰ� �ѹ��� ���� �ȵǾ���, 200�� �̻��� ��� ��� ����,
        {
            for (int i = 0; i < aniGameObject.Length; i++)
            {
                animator[i] = aniGameObject[i].GetComponent<Animator>();
                animator[i].updateMode = AnimatorUpdateMode.UnscaledTime;     //�ִϸ��̼��� ����� ������ ���� �ʵ��� ����
                animator[i].SetBool("Unlock", true);
            }
            statuseScript.removeBossRestrictions = true;
        }         

    }
    //�ִ� ����� �ı��Ǵ� �̺�Ʈ�� AniOffAndDistroy ��ũ��Ʈ���� ����


}
