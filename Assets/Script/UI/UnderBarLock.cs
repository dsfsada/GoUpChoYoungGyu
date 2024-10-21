using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnderbarLock : MonoBehaviour
{
    public GameObject[] aniGameObject;  //애니메이션 나올 오브젝트
    private Animator[] animator;
    private Statuse statuseScript;

    void Start()
    {
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();
        if (statuseScript.removeBossRestrictions)     //게임 시작 후 한번이라도 200층이 넘었으면 이미 애니메이션을 봤다는 뜻으로 보고 이 오브젝트를 파괴함
        {
            for (int i = 0; i < aniGameObject.Length; i++)
            {
                Destroy(aniGameObject[i]);    //게임 오브젝트 파괴
                Destroy(this, 2f);              //해당 스크립트 파괴
            }
        }
    }


    public void UnlockBossAnimation()             //애니메이션 실행
    {
        animator = new Animator[aniGameObject.Length];

        //사실 player스크립트에서 한번 확인을 하기에 밑에 if문은 없어도 별 상관 없음
        if (!statuseScript.removeBossRestrictions && statuseScript.floor >= 200)          //애니가 한번도 실행 안되었고, 200층 이상일 경우 경우 실행,
        {
            for (int i = 0; i < aniGameObject.Length; i++)
            {
                animator[i] = aniGameObject[i].GetComponent<Animator>();
                animator[i].updateMode = AnimatorUpdateMode.UnscaledTime;     //애니메이션이 배속의 영향을 받지 않도록 만듬
                animator[i].SetBool("Unlock", true);
            }
            statuseScript.removeBossRestrictions = true;
        }         

    }
    //애니 종료시 파괴되는 이벤트는 AniOffAndDistroy 스크립트에서 적용


}
