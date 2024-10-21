using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StSelectUi : MonoBehaviour
{
    [Header("StSelectUi �Ʒ� ��ư 2�� ����")]
    public Button[] buttons; // ��ư �迭 (Button_1, Button_2, Button_3)

    [Header("�⺻ / ȯ��")]
    public GameObject[] uiPanels; // UI �г� �迭 (Ui_1, Ui_2, Ui_3)

    private StatuseText statuseTextScript;

    private readonly Color activeColor = new Color32(250, 120, 120, 255);
    private readonly Color inactiveColor = Color.white;


    void Start()
    {
        statuseTextScript = GameObject.Find("Statuse_scroll").GetComponent<StatuseText>();

        // �ʱ� ���·� ù ��° ��ư Ŭ��
        OnClickButton(0);

        // ��ư Ŭ�� �̺�Ʈ ���
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // ���� ������ ĸó
            buttons[i].onClick.AddListener(() => OnClickButton(index));
        }
    }

    public void OnClickButton(int index)
    {
       
        statuseTextScript = GameObject.Find("Statuse_scroll").GetComponent<StatuseText>();
        
        // ��� UI �г��� ��Ȱ��ȭ�ϰ� ���õ� �гθ� Ȱ��ȭ
        for (int i = 0; i < uiPanels.Length; i++)
        {
            uiPanels[i].SetActive(i == index);
        }

        // ��� ��ư�� ������ �ʱ�ȭ�ϰ� ���õ� ��ư�� Ȱ��ȭ �������� ����
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = i == index ? activeColor : inactiveColor;
        }

        // ������ Ȱ��ȭ ó��
        statuseTextScript.UpdateStatuseText();
    }

}
