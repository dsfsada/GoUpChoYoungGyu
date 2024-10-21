using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButton : MonoBehaviour
    //�ɼ� ��ư Ŭ���� �߻��Ǵ� �̺�Ʈ
{
    public GameObject panel;
    private Button myButton;

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
            // ��Ȱ��ȭ�� ���¶�� Ȱ��ȭ
            panel.SetActive(true);
        }
    }

    public void OnClickPanelExit()
    {
        panel.SetActive(false);
    }
}
