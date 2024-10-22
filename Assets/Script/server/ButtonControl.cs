using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ButtonControl : MonoBehaviour
{
    public string dbType = "1"; // 1 insert ,,, 2 select data 3 update 
    public string Id = "0";
    public string Passward = "1234";
    public string dbData = "";
    public string code = "";

    public string chat = "";

    ClientSend clientsend = new ClientSend();

    public TextMeshProUGUI inputId;
    public TextMeshProUGUI inputPassword;

    private float updateTime; 

    public void Start()
    {
        updateTime = 0f;
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            Id = GameObject.Find("UserData").GetComponent<UserData>().id;
            Passward = GameObject.Find("UserData").GetComponent<UserData>().passward;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainScene")
        {
            updateTime += 1f * Time.deltaTime;
            try
            {
                if (updateTime > 10f)
                {
                    UpdateClick();
                    updateTime = 0f;
                    Debug.Log("자동 업데이트");
                }
            }
            catch
            {
                Debug.Log("자동 업데이트 불가");
            }
        }

    }
    public void InsertClick() //insert
    {
        // 보낼 데이터 수집
        float coin = GameObject.Find("Statuse").GetComponent<Statuse>().coin;
        float floor = GameObject.Find("Statuse").GetComponent<Statuse>().floor;

        int[] status = GameObject.Find("Statuse").GetComponent<Statuse>().upgradeState;
        int[,] artifactsValues = GameObject.Find("Statuse").GetComponent<Statuse>().artifactsValues;
        int[,] artifactsNum = GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum;
        int[,] artifacts = GameObject.Find("Statuse").GetComponent<Statuse>().artifacts;

        // db 데이터 초기화

        //보낼 값 작성
        string state = "";
        for(int i = 0; i < status.Length; i++) {
            state += $"{status[i]}^";
        }
        state = state.TrimEnd('^');

        string av = "";
        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 12; j++) {
                av += $"{artifactsValues[i, j]}^";
            }
        }
        av = av.TrimEnd('^');

        string an = "";
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                an += $"{artifactsNum[i, j]}^";
            }
        }
        an = an.TrimEnd('^');

        string artifact = "";
        for (int i = 0; i < 3; i++){
            for (int j = 0; j < 2; j++)
            {
                artifact += $"{artifacts[i, j]}^";
            }
        }
        artifact = artifact.TrimEnd('^');
        string co = $"{coin}";
        string fl = $"{floor}";

        string msg = $"1.{Id}.{Passward}";
        clientsend.ButtonClick(msg);
    }
    public void SerectClick() //serect
    {
        string msg = "";

        Debug.Log("Button is clicked");
        msg = $"2.{Id}.{Passward}.state";
        clientsend.ButtonClick(msg);

        msg = $"2.{Id}.{Passward}.av";
        clientsend.ButtonClick(msg);

        msg = $"2.{Id}.{Passward}.an";
        clientsend.ButtonClick(msg);

        msg = $"2.{Id}.{Passward}.artifacts";
        clientsend.ButtonClick(msg);

        msg = $"2.{Id}.{Passward}.coin";
        clientsend.ButtonClick(msg);

        msg = $"2.{Id}.{Passward}.floor";
        clientsend.ButtonClick(msg);

        msg = $"2.{Id}.{Passward}.reCoin";
        clientsend.ButtonClick(msg);
        msg = $"2.{Id}.{Passward}.rebirthCount";
        clientsend.ButtonClick(msg);
        msg = $"2.{Id}.{Passward}.reUpgradeState";
        clientsend.ButtonClick(msg);
        msg = $"2.{Id}.{Passward}.questOrder";
        clientsend.ButtonClick(msg);
        
    }
    public void UpdateClick() //update
    {
        float coin = GameObject.Find("Statuse").GetComponent<Statuse>().coin;
        float floor = GameObject.Find("Statuse").GetComponent<Statuse>().floor;

        int[] status = GameObject.Find("Statuse").GetComponent<Statuse>().upgradeState;
        int[,] artifactsValues = GameObject.Find("Statuse").GetComponent<Statuse>().artifactsValues;
        int[,] artifactsNum = GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum;
        int[,] artifacts = GameObject.Find("Statuse").GetComponent<Statuse>().artifacts;

        float reCoin = GameObject.Find("Statuse").GetComponent<Statuse>().reCoin;
        float rebirthCount = GameObject.Find("Statuse").GetComponent<Statuse>().rebirthCount;
        int[] reUpgradeState = GameObject.Find("Statuse").GetComponent<Statuse>().reUpgradeState;
        int questOrder = GameObject.Find("QusetBar").GetComponent<TutorialText>().questOrder;

        dbData = ""; // db 넘길 데이터 정리 하는 곳
        string msg = "";

        //보낼 값 작성
        for (int i = 0; i < status.Length; i++) //state 서버 저장
        {
            dbData += $"{status[i]}^";
        }
        dbData = dbData.TrimEnd('^');
        msg = $"3.{Id}.{Passward}.state.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = "";
        for (int i = 0; i < 3; i++) // artifactsValues 서버 저장
        {
            for (int j = 0; j < 12; j++)
            {
                dbData += $"{artifactsValues[i, j]}^";
            }
        }
        dbData = dbData.TrimEnd('^');
        msg = $"3.{Id}.{Passward}.av.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = "";
        for (int i = 0; i < 3; i++) // artifactsNum 서버 저장
        {
            for (int j = 0; j < 12; j++)
            {
                dbData += $"{artifactsNum[i, j]}^";
            }
        }
        dbData = dbData.TrimEnd('^');
        msg = $"3.{Id}.{Passward}.an.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = "";
        for (int i = 0; i < 3; i++) // artifacts 서버 저장
        {
            for (int j = 0; j < 2; j++)
            {
                dbData += $"{artifacts[i, j]}^";
            }
        }
        dbData = dbData.TrimEnd('^');
        msg = $"3.{Id}.{Passward}.artifacts.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = $"{coin}"; //코인 서버 저장
        msg = $"3.{Id}.{Passward}.coin.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = $"{floor}"; //도달 층 서버 저장
        msg = $"3.{Id}.{Passward}.floor.{dbData}";
        clientsend.ButtonClick(msg);


        dbData = $"{reCoin}"; //환생 재화 
        msg = $"3.{Id}.{Passward}.reCoin.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = $"{rebirthCount}"; //환생횟수 서버 저장
        msg = $"3.{Id}.{Passward}.rebirthCount.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = "";
        for (int i = 0; i < reUpgradeState.Length; i++) //환생 스탯 서버 저장
        {
            dbData += $"{reUpgradeState[i]}^";
        }
        dbData = dbData.TrimEnd('^');
        msg = $"3.{Id}.{Passward}.reUpgradeState.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = $"{questOrder}"; //퀘스트 순서
        msg = $"3.{Id}.{Passward}.questOrder.{dbData}";
        clientsend.ButtonClick(msg);
        
    }

    public void RequestReward() // 리워드 요청하기
    {
        string msg = $"6.{Id}.{code}";
        clientsend.ButtonClick(msg);
    }
    public void GetRank()
    {
        string msg = $"11.{Id}";
        clientsend.ButtonClick(msg);
    }
    public void SendCht() //메세지 보내기
    {
        string msg = $"10.{Id}.{chat}";
        clientsend.ButtonClick(msg);
    }
   
    public void NextScene()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("다음 씬");
    }
    public void Login()
    {
        Id = inputId.text.Replace("\u200B", "");
        Passward = inputPassword.text.Replace("\u200B", "");


        GameObject.Find("UserData").GetComponent<UserData>().id = Id;
        GameObject.Find("UserData").GetComponent<UserData>().passward = Passward;

        string msg = $"0.{Id}.{Passward}";
        Debug.Log(msg);
        clientsend.ButtonClick(msg);
    }
    public void NewUser() {
        Id = inputId.text.Replace("\u200B", "");
        Passward = inputPassword.text.Replace("\u200B", "");

        GameObject.Find("UserData").GetComponent<UserData>().id = Id;
        GameObject.Find("UserData").GetComponent<UserData>().passward = Passward;

        string msg = $"20.{Id}.{Passward}";
        clientsend.ButtonClick(msg);
    }
}
