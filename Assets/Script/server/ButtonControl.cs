using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public string dbType = "1"; // 1 insert ,,, 2 select data 3 update 
    public string Id = "0";
    public string Passward = "1234";
    public string dbData = "";
    public string code = "";

    public string chat = "";

    ClientSend clientsend = new ClientSend();

    public void Start()
    {
        Id = GameObject.Find("UserData")?.GetComponent<UserData>().id;
        Passward = GameObject.Find("UserData")?.GetComponent<UserData>().passward;
    }

    public void InsertClick() //insert
    {
        // ���� ������ ����
        float coin = GameObject.Find("Statuse").GetComponent<Statuse>().coin;
        float floor = GameObject.Find("Statuse").GetComponent<Statuse>().floor;

        int[] status = GameObject.Find("Statuse").GetComponent<Statuse>().upgradeState;
        int[,] artifactsValues = GameObject.Find("Statuse").GetComponent<Statuse>().artifactsValues;
        int[,] artifactsNum = GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum;
        int[,] artifacts = GameObject.Find("Statuse").GetComponent<Statuse>().artifacts;

        // db ������ �ʱ�ȭ

        //���� �� �ۼ�
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

    }
    public void UpdateClick() //update
    {
        float coin = GameObject.Find("Statuse").GetComponent<Statuse>().coin;
        float floor = GameObject.Find("Statuse").GetComponent<Statuse>().floor;

        int[] status = GameObject.Find("Statuse").GetComponent<Statuse>().upgradeState;
        int[,] artifactsValues = GameObject.Find("Statuse").GetComponent<Statuse>().artifactsValues;
        int[,] artifactsNum = GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum;
        int[,] artifacts = GameObject.Find("Statuse").GetComponent<Statuse>().artifacts;
        dbData = ""; // db �ѱ� ������ ���� �ϴ� ��
        string msg = "";

        //���� �� �ۼ�
        for (int i = 0; i < status.Length; i++) //state ���� ����
        {
            dbData += $"{status[i]}^";
        }
        dbData = dbData.TrimEnd('^');
        msg = $"3.{Id}.{Passward}.state.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = "";
        for (int i = 0; i < 3; i++) // artifactsValues ���� ����
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
        for (int i = 0; i < 3; i++) // artifactsNum ���� ����
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
        for (int i = 0; i < 3; i++) // artifacts ���� ����
        {
            for (int j = 0; j < 2; j++)
            {
                dbData += $"{artifacts[i, j]}^";
            }
        }
        dbData = dbData.TrimEnd('^');
        msg = $"3.{Id}.{Passward}.artifacts.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = $"{coin}"; //���� ���� ����
        msg = $"3.{Id}.{Passward}.coin.{dbData}";
        clientsend.ButtonClick(msg);

        dbData = $"{floor}"; //���� �� ���� ����
        msg = $"3.{Id}.{Passward}.floor.{dbData}";
        clientsend.ButtonClick(msg);
    }

    public void RequestReward() // ������ ��û�ϱ�
    {
        string msg = $"6.{Id}.{code}";
        clientsend.ButtonClick(msg);
    }

    public void SendCht() //�޼��� ������
    {
        string msg = $"10.{Id}.{chat}";
        clientsend.ButtonClick(msg);
    }
   
    public void Login()
    {
        string msg = $"0.{Id}.{Passward}";
        clientsend.ButtonClick(msg);
    }

    public void NewUser() {

        string msg = $"20.{Id}.{Passward}";
        clientsend.ButtonClick(msg);
    }

    public void NextScene()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("���� ��");
    }
}
