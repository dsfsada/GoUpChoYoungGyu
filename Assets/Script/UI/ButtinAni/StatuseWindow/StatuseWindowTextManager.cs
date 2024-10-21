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
        // st 오브젝트의 모든 하위 오브젝트에서 StatuseWindowText 스크립트들을 가져
        statusScripts = GetComponentsInChildren<StatuseWindowText>();

        foreach (StatuseWindowText statusScript in statusScripts)
        {
            if (statusScript != null)
            {
                statusScript.UpdateStatuseWindowText();     //하위 StatuseWindowText스크립트 모두 실행
            }
        }
    }
}
