using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class StatuseWindowText : MonoBehaviour
{
    private Text[] allTexts;                                    // 현재 오브젝트(Button)와 모든 하위 오브젝트에서 Text 컴포넌트를 가져옴
    private Statuse statuseScript;
    private Dictionary<int, string> statusTextMapping;          //text에서 맨 앞에 올 text를 결정

    private float baseState = 0f;                        //아이템 없는 능력치 값
    private float artifactState = 0f;                    //아티팩트만 있는 값

    public Button myButton;                            //내 버튼
    public Animator animator;                          //버튼 클릭시 애니메이션
    public int kind;

    void Start()
    {
        /*myButton = GetComponent<Button>();
        animator = GetComponent<Animator>();*/
        if (animator.GetInteger("click") == 1)
        {
            animator.SetInteger("click", 2);        //OFF형태의 애니메이션
        }

        animator.updateMode = AnimatorUpdateMode.UnscaledTime;      //애니메이션이 배속의 영향을 받지 않도록 만듬

        myButton.onClick.AddListener(OnClickButton);
        UpdateStatuseWindowText();
    }

    private void OnClickButton()    //버튼 클릭시 부분 능력치 보여주는 애니메이션 출력(1: ON, 2 : OFF)
    {
        if(animator.GetInteger("click") == 1)       //ON애니메이션 출력상태에서 버튼 클릭시 OFF애니메 출력
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

        baseState = statuseScript.GetAbilityValue(kind) * (statuseScript.upgradeState[kind] + 1f);           //kind에 맞는 능력치 값 -> 파이널_스테이터스[kind] - 아티팩트value[kind]
        if (kind < 3 && kind >= 0)  //아티팩트 종류가 0,1,2뿐임
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
        // 가져온 Text 컴포넌트들을 순회하며 작업 수행
    }
}