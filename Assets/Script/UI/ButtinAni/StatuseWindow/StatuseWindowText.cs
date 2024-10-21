using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class StatuseWindowText : MonoBehaviour
{
    private Text[] allTexts;                                    // ���� ������Ʈ(Button)�� ��� ���� ������Ʈ���� Text ������Ʈ�� ������
    private Statuse statuseScript;
    private Dictionary<int, string> statusTextMapping;          //text���� �� �տ� �� text�� ����

    private float baseState = 0f;                        //������ ���� �ɷ�ġ ��
    private float artifactState = 0f;                    //��Ƽ��Ʈ�� �ִ� ��

    public Button myButton;                            //�� ��ư
    public Animator animator;                          //��ư Ŭ���� �ִϸ��̼�
    public int kind;

    void Start()
    {
        /*myButton = GetComponent<Button>();
        animator = GetComponent<Animator>();*/
        if (animator.GetInteger("click") == 1)
        {
            animator.SetInteger("click", 2);        //OFF������ �ִϸ��̼�
        }

        animator.updateMode = AnimatorUpdateMode.UnscaledTime;      //�ִϸ��̼��� ����� ������ ���� �ʵ��� ����

        myButton.onClick.AddListener(OnClickButton);
        UpdateStatuseWindowText();
    }

    private void OnClickButton()    //��ư Ŭ���� �κ� �ɷ�ġ �����ִ� �ִϸ��̼� ���(1: ON, 2 : OFF)
    {
        if(animator.GetInteger("click") == 1)       //ON�ִϸ��̼� ��»��¿��� ��ư Ŭ���� OFF�ִϸ� ���
        {
            animator.SetInteger("click", 2);
        }
        else
        {
            animator.SetInteger("click", 1);
        }
    }

    public void UpdateStatuseWindowText()
    {
        allTexts = GetComponentsInChildren<Text>();
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();

        statusTextMapping = new Dictionary<int, string>()
        {
            { 0, "ATK"},
            { 1 ,"HP" },
            { 2, "CRI-ATK" },
            { 3, "CRI-RATE" },
        };

        baseState = statuseScript.GetAbilityValue(kind) * (statuseScript.upgradeState[kind] + 1f);           //kind�� �´� �ɷ�ġ �� -> ���̳�_�������ͽ�[kind] - ��Ƽ��Ʈvalue[kind]
        if (kind < 3 && kind >= 0)  //��Ƽ��Ʈ ������ 0,1,2����
        {
            if (statuseScript.artifacts[kind, 0] != -1)
            {
                int artifactType = statuseScript.artifacts[kind, 0];
                int artifactLevel = statuseScript.artifacts[kind, 1];
                float basicValue = MathF.Pow(statuseScript.GetTypeAbility(kind), artifactLevel + 1);
                artifactState = basicValue + (basicValue * statuseScript.artifactsValues[artifactType, artifactLevel] * 0.1f);
            }
        }

        allTexts[0].text = $"{statusTextMapping[kind]} : {statuseScript.finalStatus[kind]}";      //ex> ATK : 5 
        allTexts[1].text = $"{statusTextMapping[kind]}(base) : {baseState} \n{statusTextMapping[kind]}(artifact) : {artifactState}";
        // ������ Text ������Ʈ���� ��ȸ�ϸ� �۾� ����
    }
}