using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

public class Statuse : MonoBehaviour
{
    //타입
    private const float AttackValue = 1.0f;
    private const float HealthValue = 20.0f;
    private const float CriticalDamageValue = 1.0f;
    private const float CriticalRateValue = 0.1f;
    private const float AttackSpeedValue = 0.01f;


    private Player playerScript;                //플레이어 스크립트 가져올때 사용

    private readonly float[] basestate = { 1, 20, 0, 0, 1 };
    // 최종 능력치 값
    public float[] finalStatus = new float[5];
    // 돈 , 층수
    public float coin;
    public float floor;
    public float reCoin;                // 환생 재화
    public float rebirthCount;          // 환생한 횟수

    public bool removeBossRestrictions; //보스 제한 해제 , 200층 이상 갔을시 BOOL값을 TRUE로 할당하여 보스와 아이템을 해제함

    //업그레이드한 양
    public int[] upgradeState = new int[5];

    // 환생코인으로 업그레이드한 양
    public int[] reUpgradeState = new int[5];   // 공, 체, 크확, 추가 돈, 이동속도

    // 아티팩트 관련  --

    //아티팩트 아이템 갯수
    public int[,] artifactsValues = new int[3, 12]; 
    //아티팩트 아이템 갯수
    public int[,] artifactsNum = new int[3, 12];    

    // 렙업에 필요한 파편 갯수
    public int[] Levelitem = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; 

    //장착시킨 아티팩트
    public int[,] artifacts = {  // -1 안에 든 값 없음
        {-1,0}, {-1,0}, {-1,0}      //[장착 아티팩트의 종류, 장착된 장비 초기 능력치?(번호)]
    };

    private void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();
        StatuseUpdate();
    }

    public float GetAbilityValue(int _kind)         //능력치 기본 값들을 가져옴
    {
        return _kind switch
        {
            0 => AttackValue,               // 공격력이면
            1 => HealthValue,               // 체력이면
            2 => CriticalDamageValue,       // 크리공격이면
            3 => CriticalRateValue,         // 크리확률이면
            4 => AttackSpeedValue,
            _ => throw new ArgumentOutOfRangeException(nameof(_kind), _kind, "어떤 능력치에 대한 정보인지 모름")
        };
    }

    public void StatuseUpdate()
    {
        // 능력치 업그레이드로 인한 능력치 변화
        for (int i=0; i<finalStatus.Length; i++)    
        {
            finalStatus[i] = basestate[i] + GetAbilityValue(i) * upgradeState[i];       //0부터 4까지 해서 공, 체, 크공, 크확, 공속(없음) 이다
        }

        // 아티팩트 로 인한 능력치 변화
        for (int i = 0; i < 3; i++)
        {
            if (artifacts[i, 0] != -1)
            {
                int artifactType = artifacts[i, 0];
                int artifactLevel = artifacts[i, 1];
                float basicValue = MathF.Pow(GetTypeAbility(i), artifactLevel + 1);
                finalStatus[artifactType] *= basicValue + (basicValue * artifactsValues[artifactType, artifactLevel] * 0.1f);
                //아티팩트 초기 능력치 * 아티팩트 강화 횟수 * 아티팩트 종류별 다른 수치
            }
        }

        // 환생 능력치 업그레이드로 인한 능력치 변화
        // 능력치 순서가 일정하지 않아 열거해서 대입함
        finalStatus[0] *= MathF.Pow(2, reUpgradeState[0]);      // 공격력 증가량
        finalStatus[1] *= MathF.Pow(2, reUpgradeState[1]);      // 체력 증가량
        finalStatus[3] += reUpgradeState[2] * 5f;

        //player에게 정보 보내기
        playerScript.attackPower = finalStatus[0];
        playerScript.maxHealth = finalStatus[1];
        playerScript.critAtk = finalStatus[2];
        playerScript.critRate = finalStatus[3];
        playerScript.attackCooldown = finalStatus[4];
    }
    
    public int GetTypeAbility(int _kind)            //타입별 부여되는 수치를 다르게 하기 위함
    {
        return _kind switch
        {
            0 => 2,              //공격력이면
            1 => 2,             //체력이면
            2 => 2,              //크리공격이면
            _ => throw new ArgumentOutOfRangeException(nameof(_kind), _kind, "오류 발생 0, 1, 2.")
        };
    }

    public void ResetEquip()
    {
        for (int i = 0; i < 3; i++)
        {
            artifacts[i, 0] = -1;
        }
        GameObject.Find("ButtonControl").GetComponent<Artifacts>().isEquipq = new int[3,12];
    }

    public void CoinUsage(float _useCoin)   //업그레이드 버튼 클릭시 코인 사용
    {
        coin -= _useCoin;
    }

    public void ReCoinUsage(float _useCoin)   //업그레이드 버튼 클릭시 코인 사용
    {
        reCoin -= _useCoin;
    }

    //아이템 갯수확인 후 렙업 최대 

    public void UpgradeAtk0()
    {
        int kind = 0;
        int num = 0;
        if (artifactsNum[kind, num] >= Levelitem[artifactsValues[kind, num]] && artifactsValues[kind, num] < 10)
        {
            artifactsValues[kind, num] += 1;
        }
    }
    public void UpgradeItem(int kind = 0, int num = 0)
    {

        if (artifactsNum[kind, num] >= Levelitem[artifactsValues[kind, num]] && artifactsValues[kind, num] < 9)
        {
            artifactsNum[kind, num] -= Levelitem[artifactsValues[kind, num]];
            artifactsValues[kind, num] += 1;

        }
    }

    //void InitializeArray(int[,] array)      //2차원 배열 값 선언
    //{
    //    for (int j = 0; j < array.GetLength(0); j++)
    //    {
    //        for (int k = 0; k < array.GetLength(1); k++)
    //        {
    //            array[j, k] = 0;
    //        }
    //    }
    //}

}