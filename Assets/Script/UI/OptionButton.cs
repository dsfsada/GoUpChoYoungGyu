using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
    //옵션 버튼 클릭시 발생되는 이벤트
{
    public GameObject panel;
    private Button myButton;
    public GameObject ButtonControl;
    public GameObject[] RankList;


    private void Start()
    {
        myButton = GetComponent<Button>();
        panel.SetActive(false);
        myButton.onClick.AddListener(OnClickPanel);
        
    }

    public void OnClickPanel()
    {
        if (panel.activeSelf)
        {
            // 활성화된 상태라면 비활성화
            panel.SetActive(false);
        }
        else
        {
            GameObject.Find("ButtonControl").GetComponent<ButtonControl>().GetRank();
            // 비활성화된 상태라면 활성화
            panel.SetActive(true);
        }
    }

    public void OnClickPanelExit()
    {
        panel.SetActive(false);
    }
}
