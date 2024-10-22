using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
    //�ɼ� ��ư Ŭ���� �߻��Ǵ� �̺�Ʈ
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
            // Ȱ��ȭ�� ���¶�� ��Ȱ��ȭ
            panel.SetActive(false);
        }
        else
        {
            GameObject.Find("ButtonControl").GetComponent<ButtonControl>().GetRank();
            // ��Ȱ��ȭ�� ���¶�� Ȱ��ȭ
            panel.SetActive(true);
        }
    }

    public void OnClickPanelExit()
    {
        panel.SetActive(false);
    }
}
