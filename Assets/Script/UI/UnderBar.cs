using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnderBar : MonoBehaviour
{
    private UnderbarLock underbarLockScript;
    public int maxKind = 3 , maxNum = 12;

    [Header("Underbar쪽 버튼 4개 적용")]
    public Button[] buttons;        // 버튼 배열
    public GameObject[] uis;        // UI 배열
    public Animator[] animators;    // 애니메이터 배열

    public GameObject EnUi_1;       //Item_enforce_area
    public GameObject EnUi_2;       //boss_scroll_area

    private void Start()
    {
        underbarLockScript = GetComponent<UnderbarLock>();

        // 각 버튼에 클릭 이벤트 연결
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // 로컬 변수로 캡처
            buttons[i].onClick.AddListener(() => OnClickButton(index));     //버튼 클릭시 이벤트 발생[publci으로 버튼 집어넣을때 순서에 맞게 집어 넣으셈
            animators[i].updateMode = AnimatorUpdateMode.UnscaledTime;      //애니메이션이 배속의 영향을 받지 않도록 만듬
        }

        ButtonInteractable();       //환생 전후로 한번이라도 200층 도달 못했다면 보스 및 아이템 비활성화
        OnClickButton(0);
    }

    public void OnClickButton(int index)
    {
        ButtonReset();
        buttons[index].GetComponent<Image>().color = GetButtonColor(index); // 버튼 색상 변경

        if (index < animators.Length)
        {
            animators[index].SetBool("click", true);    // 애니메이션 실행
        }

        uis[index].SetActive(true);         // 선택된 UI 활성화

        if (index == 0)
        {
            GameObject.Find("Statuse_scroll").GetComponent<StatuseText>().UpdateStatuseText();
        }
        else if (index == 1)
        {
            EnUi_1.SetActive(true);
        }
        else if (index == 2)
        {
            EnUi_2.SetActive(true);
        }
        else if (index == 3)
        {
            GameObject.Find("StatuseWindow_scroll")?.GetComponent<StatuseWindowTextManager>()
                .GetUpdateStatuseWindowText();     //능력치 보여주는 화면 갱신
        }
    }

    public void ButtonInteractable()       //환생 전후로 한번이라도 200층 도달 못했다면 보스 및 아이템 비활성화
    {
        if (GameObject.Find("Statuse")?.GetComponent<Statuse>().removeBossRestrictions == false)
        {
            buttons[1].interactable = false;
            buttons[2].interactable = false;
        }
        else
        {
            buttons[1].interactable = true;
            buttons[2].interactable = true;
        }
    }

    private void ButtonReset()          //버튼 색 초기화
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Image>().color = Color.white;       //버튼 색 초기화
        }

        foreach (var animator in animators)
        {
            animator.SetBool("click", false);                       //버튼 애니메이션 초기화
        }

        foreach (var ui in uis)
        {
            ui.SetActive(false);                //버튼 활성 초기화
        }

        EnUi_1.SetActive(false);                //gameobject 활성 초기화
        EnUi_2.SetActive(false);
    }

    private Color GetButtonColor(int index)
    {
        return index switch
        {
            0 => (Color)new Color32(80, 255, 80, 255),// 초록색
            1 => (Color)new Color32(130, 200, 255, 255),// 하늘색
            2 => (Color)new Color32(220, 130, 255, 255),// 보라색
            3 => (Color)new Color32(255, 0, 0, 255),// 빨간색
            _ => Color.white,
        };
    }
}