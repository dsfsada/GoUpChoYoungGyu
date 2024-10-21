using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EqSelectUi : MonoBehaviour
{
    [Header("EqSelectUi 아래 버튼 3개 적용")]
    public Button[] buttons; // 버튼 배열 (Button_1, Button_2, Button_3)

    [Header("무기/방어구/장신구 UI")]
    public GameObject[] uiPanels; // UI 패널 배열 (Ui_1, Ui_2, Ui_3)

    private Statuse statuse;
    private Inventory inventory;

    private readonly Color activeColor = new Color32(250, 120, 120, 255);
    private readonly Color inactiveColor = Color.white;


    void Start()
    {
        statuse = GameObject.Find("Statuse").GetComponent<Statuse>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        // 초기 상태로 첫 번째 버튼 클릭
        OnClickButton(0);

        // 버튼 클릭 이벤트 등록
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 로컬 변수로 캡처
            buttons[i].onClick.AddListener(() => OnClickButton(index));
        }
    }

    public void OnClickButton(int index)
    {
        // 모든 UI 패널을 비활성화하고 선택된 패널만 활성화
        for (int i = 0; i < uiPanels.Length; i++)
        {
            uiPanels[i].SetActive(i == index);
        }

        // 모든 버튼의 색상을 초기화하고 선택된 버튼만 활성화 색상으로 변경
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().color = i == index ? activeColor : inactiveColor;
        }

        // 아이템 활성화 처리
        ItemOn();
    }

    void ItemOn()
    {
        // 3x12 크기의 이중 배열을 순회하면서 아이템 상태를 확인하고, 해당 아이템의 위치를 Inventory에 업데이트
        for (int kind = 0; kind < 3; kind++)
        {
            for (int num = 0; num < 12; num++)
            {
                if (statuse.artifactsNum[kind, num] > 0 || statuse.artifactsValues[kind, num] > 0)
                {
                    inventory.ItemLcation(kind, num); // 아티팩트가 존재하는 경우 버튼 활성화
                }
            }
        }
    }
}
