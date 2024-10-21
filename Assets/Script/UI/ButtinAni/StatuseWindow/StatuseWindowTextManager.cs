using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatuseWindowTextManager : MonoBehaviour
{
    private StatuseWindowText[] statusScripts; 

    void Start()
    {
        GetUpdateStatuseWindowText();
    }

    public void GetUpdateStatuseWindowText()
    {
        // st ������Ʈ�� ��� ���� ������Ʈ���� StatuseWindowText ��ũ��Ʈ���� ����
        statusScripts = GetComponentsInChildren<StatuseWindowText>();

        foreach (StatuseWindowText statusScript in statusScripts)
        {
            if (statusScript != null)
            {
                statusScript.UpdateStatuseWindowText();     //���� StatuseWindowText��ũ��Ʈ ��� ����
            }
        }
    }
}
