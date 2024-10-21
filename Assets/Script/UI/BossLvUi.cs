using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

public class BossLvUi : MonoBehaviour
{
    private Transform bossLv1;
    private Transform bossLv2;
    private Transform bossLv3;
    
    private Button downButton;
    private Button UpButton;

    public int bossTypeValue=0; //0�� ��, 1�� �罿
    public int bossLvValue = 1;

    private void Start()
    {
        // ���� ������Ʈ ��������
        bossLv1 = transform.GetChild(0);
        bossLv2 = transform.GetChild(1);
        bossLv3 = transform.GetChild(2);

        // ��ư ������Ʈ ��������
        UpButton = transform.GetChild(3).GetComponent<Button>(); // ù ��° ��ư
        downButton = transform.GetChild(4).GetComponent<Button>(); // ù ��° ��ư

        BossUi(1);

        UpButton.onClick.AddListener(() => LvChangeButtonClick(true));
        downButton.onClick.AddListener(() => LvChangeButtonClick(false));
    }

    private void BossUi(int lv)
    {
        bossLvValue = lv; //�⺻�� 1
        bossLv1.gameObject.SetActive(false);
        bossLv2.gameObject.SetActive(false);
        bossLv3.gameObject.SetActive(false);

        if (bossLvValue == 1)
        {
            bossLv1.gameObject.SetActive(true);
            downButton.gameObject.SetActive(false);
            UpButton.gameObject.SetActive(true);
        }
        else if (bossLvValue == 2)
        {
            bossLv2.gameObject.SetActive(true);
            downButton.gameObject.SetActive(true);
            UpButton.gameObject.SetActive(true);
        }
        else if(bossLvValue == 3)
        {
            bossLv3.gameObject.SetActive(true);
            downButton.gameObject.SetActive(true);
            UpButton.gameObject.SetActive(false);
        }

    }

    private void LvChangeButtonClick(bool value)
    {
        if (value)  //up��ư�� ���
        {
            if(bossLvValue < 3)
            {
                bossLvValue += 1;
            }
        }
        else
        {
            if (bossLvValue > 0)
            {
                bossLvValue -= 1;
            }
        }
        BossUi(bossLvValue);
    }
}
