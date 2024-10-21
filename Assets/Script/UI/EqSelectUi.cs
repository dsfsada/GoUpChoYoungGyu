using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EqSelectUi : MonoBehaviour
{
    [Header("EqSelectUi �Ʒ� ��ư 3�� ����")]
    public Button[] buttons; // ��ư �迭 (Button_1, Button_2, Button_3)

    [Header("����/��/��ű� UI")]
    public GameObject[] uiPanels; // UI �г� �迭 (Ui_1, Ui_2, Ui_3)

    private Statuse statuse;
    private Inventory inventory;

    private readonly Color activeColor = new Color32(250, 120, 120, 255);
    private readonly Color inactiveColor = Color.white;


    void Start()
    {
        statuse = GameObject.Find("Statuse").GetComponent<Statuse>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

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
        ItemOn();
    }

    void ItemOn()
    {
        // 3x12 ũ���� ���� �迭�� ��ȸ�ϸ鼭 ������ ���¸� Ȯ���ϰ�, �ش� �������� ��ġ�� Inventory�� ������Ʈ
        for (int kind = 0; kind < 3; kind++)
        {
            for (int num = 0; num < 12; num++)
            {
                if (statuse.artifactsNum[kind, num] > 0 || statuse.artifactsValues[kind, num] > 0)
                {
                    inventory.ItemLcation(kind, num); // ��Ƽ��Ʈ�� �����ϴ� ��� ��ư Ȱ��ȭ
                }
            }
        }
    }
}
