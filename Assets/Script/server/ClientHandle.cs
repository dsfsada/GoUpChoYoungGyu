using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClientHandle
{
    public void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");

        Client.instance.myId = _myId;
        // use ClientSend to send Data back to Server
        ClientSend.WelcomeReceived();
        Client.isConnected = true;

        if(SceneManager.GetActiveScene().name == "MainScene")
            GameObject.Find("ButtonControl").GetComponent<ButtonControl>().SerectClick();
    }
    // You can add like below. You receive data through packet
    public void ReceivedUserInfo(Packet _packet)
    {
        string _name = _packet.ReadString();
        string[] name = _name.Split('/');
        int userIndex = int.Parse(name[0]) - 1;
        Debug.Log("index : " + userIndex);
        
        Debug.Log($"Sent user info: {_name}");
    }
    public void ButtonClick(Packet _packet)
    {
        
        string _msg = _packet.ReadString();
        Debug.Log(_msg);
        string[] split_data = _msg.Split(".");
        Debug.Log($"msg from server : {_msg[1]}");
        if(split_data[0] == "0") // 로그인 
        {
            if (split_data[2] == "true")
            {
                GameObject.Find("UserData").GetComponent<UserData>().id = GameObject.Find("ButtonControl").GetComponent<ButtonControl>().Id;
                GameObject.Find("UserData").GetComponent<UserData>().passward = GameObject.Find("ButtonControl").GetComponent<ButtonControl>().Passward;
                GameObject.Find("ButtonControl").GetComponent<ButtonControl>().NextScene();
            }
            else
            {
                Debug.Log("아이디 또는 비밀번호를 확인해주세요");
            }
        }
        if (split_data[0] == "2")
        {
            if (split_data[3] == "state")
            {

                string[] status = split_data[2].Split('^');
                for (int i = 0; i < status.Length; i++)
                {
                    GameObject.Find("Statuse").GetComponent<Statuse>().upgradeState[i] = int.Parse(status[i]);
                }
            }
            else if (split_data[3] == "av")
            {
                string[] artifactsValues = split_data[2].Split('^');
                
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        GameObject.Find("Statuse").GetComponent<Statuse>().artifactsValues[i, j] = int.Parse(artifactsValues[i * 4 + j]);
                    }
                }
            }
            else if (split_data[3] == "an")
            {
                string[] artifactsNum = split_data[2].Split('^');
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum[i, j] = int.Parse(artifactsNum[i * 12 + j]);
                    }
                }
            }
            else if (split_data[3] == "an")
            {
                string[] artifactsNum = split_data[2].Split('^');
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum[i, j] = int.Parse(artifactsNum[i * 12 + j]);
                    }
                }
            }
            else if (split_data[3] == "artifacts")
            {
                string[] artifacts = split_data[2].Split('^');
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        GameObject.Find("Statuse").GetComponent<Statuse>().artifacts[i, j] = int.Parse(artifacts[i * 2 + j]);
                    }
                }

            }
            else if (split_data[3] == "coin")
            {
                GameObject.Find("Statuse").GetComponent<Statuse>().coin = float.Parse(split_data[2]);
            }
            else if (split_data[3] == "floor")
            {
                GameObject.Find("Statuse").GetComponent<Statuse>().floor = float.Parse(split_data[2]);
            }
        } //select 받는것
        if (split_data[0] == "5")
        {
            string[] notice = split_data[2].Split('^');
            foreach(string noti in notice)
            {
                Debug.Log(noti);
            }
        } //공지 쓰기
        if (split_data[0] == "10") 
        {
            Debug.Log($"{split_data[1]} : {split_data[2]}");
        } //채팅 받기
        if (split_data[0] == "00000") //new user 안 씀
        {
            GameObject.Find("ButtonControl").GetComponent<ButtonControl>().Id = split_data[2];
        }
        if (split_data[0] == "6") //reward
        { 
            
         // 리워드 받는걸 분석해서 statuse 에 넣기 
        }
        if (split_data[0] =="20")
        {
            if (split_data[2] == "true")
            {
                GameObject.Find("UserData").GetComponent<UserData>().id = GameObject.Find("ButtonControl").GetComponent<ButtonControl>().Id;
                GameObject.Find("UserData").GetComponent<UserData>().passward = GameObject.Find("ButtonControl").GetComponent<ButtonControl>().Passward;
                GameObject.Find("ButtonControl").GetComponent<ButtonControl>().NextScene();
                Debug.Log("새로운 유저를 환영합니다.");
            }
            else
            {
                Debug.Log("아이디 또는 비밀번호를 확인해주세요");
            }
        }
    }
}


