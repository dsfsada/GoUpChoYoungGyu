using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StSelectUi : MonoBehaviour
{
    [Header("StSelectUi 아래 버튼 2개 적용")]
    public Button[] buttons; // 버튼 배열 (Button_1, Button_2, Button_3)

    [Header("기본 / 환생")]
    public GameObject[] uiPanels; // UI 패널 배열 (Ui_1, Ui_2, Ui_3)

    private StatuseText statuseTextScript;

    private readonly Color activeColor = new Color32(250, 120, 120, 255);
    private readonly Color inactiveColor = Color.white;


    void Start()
    {
        statuseTextScript = GameObject.Find("Statuse_scroll").GetComponent<StatuseText>();

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
       
        statuseTextScript = GameObject.Find("Statuse_scroll").GetComponent<StatuseText>();
        
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
        statuseTextScript.UpdateStatuseText();
    }

}
