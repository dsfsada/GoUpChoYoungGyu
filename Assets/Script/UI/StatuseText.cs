
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatuseText : MonoBehaviour
{
    private Statuse statuseScript;

    private readonly float[] baseState = new float[4];           //기본 능력치를 배열로 정렬

    [Header("StatsLV")]
    public Text atkLV;
    public Text healthLV;
    public Text criAtkLV;
    public Text criRateLV;

    [Header("StatsUpgrade")]
    public Text atk_text;
    public Text health_text;
    public Text criAtk_text;
    public Text criRate_text;

    [Header("CoinUsage")]
    public Text atkCoin_text;
    public Text healthCoin_text;
    public Text criAtkCoin_text;
    public Text criRateCoin_text;

    [Header("RebrithStatsLV")]
    public Text reAtkLV;
    public Text reHealthLV;
    public Text reCriRateLV;
    public Text reCoinLV;
    public Text reSpeedLV;

    [Header("RebrithStatsUpgrade")]
    public Text reAtk_text;
    public Text reHealth_text;
    public Text reCriRate_text;
    public Text reCoin_text;
    public Text reSpeed_text;

    [Header("RebrithCoinUsage")]
    public Text reAtkCoin_text;
    public Text reHealthCoin_text;
    public Text reCriRateCoin_text;
    public Text reCoinCoin_text;
    public Text reSpeedCoin_text;



    void Start()
    {
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();
        UpdateStatuseText();
    }

    //void Update()
    public void UpdateStatuseText()             //StatuseValueButtonEvent스크립트에서 버튼 클릭시 활성화 
    {
        if (statuseScript == null)
        {
            statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();
        }

        if (GameObject.Find("Underbar").GetComponent<UnderBar>().uis[0].activeSelf == true) //underbar ui1이 활성화 되어있는 경우에만 on
        {
            atkLV.text = $"LV. {statuseScript.upgradeState[0]}";
            healthLV.text = $"LV. {statuseScript.upgradeState[1]}";
            criAtkLV.text = $"LV. {statuseScript.upgradeState[2]}";
            criRateLV.text = $"LV. {statuseScript.upgradeState[3]}";

            for (int i = 0; i < baseState.Length; i++)
            {
                baseState[i] = statuseScript.GetAbilityValue(i) * (statuseScript.upgradeState[i] + 1);        //text로 집어넣을 능력치
            }

            // float 값을 문자열로 변환하여 Text 필드에 할당
            atk_text.text = $"{baseState[0]} -> {baseState[0] + statuseScript.GetAbilityValue(0)}";         //statuseScript.GetAbilityValue(i) => 능력치 기본 값들을 get으로 가져옴
            health_text.text = $"{baseState[1]} -> {baseState[1] + statuseScript.GetAbilityValue(1)}";
            criAtk_text.text = $"{baseState[2]}% -> {baseState[2] + statuseScript.GetAbilityValue(2)}%";
            criRate_text.text = $"{baseState[3].ToString("F1")}% -> {(baseState[3] + statuseScript.GetAbilityValue(3)).ToString("F1")}%";

            atkCoin_text.text = $"{statuseScript.upgradeState[0] + 1f}";
            healthCoin_text.text = $"{statuseScript.upgradeState[1] + 1f}";
            criAtkCoin_text.text = $"{statuseScript.upgradeState[2] + 1f}";
            criRateCoin_text.text = $"{statuseScript.upgradeState[3] + 1f}";

            // Rebirth Statuse버튼 아래쪽

            reAtkLV.text = $"LV. {statuseScript.reUpgradeState[0]}";
            reHealthLV.text = $"LV. {statuseScript.reUpgradeState[1]}";
            reCriRateLV.text = $"LV. {statuseScript.reUpgradeState[2]}";
            reCoinLV.text = $"LV. {statuseScript.reUpgradeState[3]}";
            //reSpeedLV.text = $"LV. {statuseScript.reUpgradeState[4]}";

            reAtk_text.text = $"x{Mathf.Pow(2, statuseScript.reUpgradeState[0])} -> x{Mathf.Pow(2, statuseScript.reUpgradeState[0] + 1)}";
            reHealth_text.text = $"x{Mathf.Pow(2, statuseScript.reUpgradeState[1])} -> x{Mathf.Pow(2, statuseScript.reUpgradeState[1] + 1)}";
            reCriRate_text.text = $"{statuseScript.reUpgradeState[2] * 5f}% -> {(statuseScript.reUpgradeState[2] + 1) * 5f}%";
            reCoin_text.text = $"{statuseScript.reUpgradeState[3] * 10}% -> {(statuseScript.reUpgradeState[3] + 1) * 10}%";
            //reSpeed_text.text = $"{baseState[0]} -> {baseState[0] + statuseScript.GetAbilityValue(0)}";

            reAtkCoin_text.text = $"{(statuseScript.reUpgradeState[0] > 0 ? statuseScript.reUpgradeState[0] * 5 : 1)}";
            reHealthCoin_text.text = $"{(statuseScript.reUpgradeState[1] > 0 ? statuseScript.reUpgradeState[1] * 5 : 1)}";
            reCriRateCoin_text.text = $"{(statuseScript.reUpgradeState[2] > 0 ? statuseScript.reUpgradeState[2] * 5 : 1)}";
            reCoinCoin_text.text = $"{(statuseScript.reUpgradeState[3] > 0 ? statuseScript.reUpgradeState[3] * 5 : 1)}";
            //reSpeedCoin_text.text = $"LV. {(statuseScript.reUpgradeState[4] > 0 ? statuseScript.reUpgradeState[4] * 5 : 1)}";
        }
    }
}
