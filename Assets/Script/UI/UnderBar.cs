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

    [Header("Underbar�� ��ư 4�� ����")]
    public Button[] buttons;        // ��ư �迭
    public GameObject[] uis;        // UI �迭
    public Animator[] animators;    // �ִϸ����� �迭

    public GameObject EnUi_1;       //Item_enforce_area
    public GameObject EnUi_2;       //boss_scroll_area

    private void Start()
    {
        underbarLockScript = GetComponent<UnderbarLock>();

        // �� ��ư�� Ŭ�� �̺�Ʈ ����
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // ���� ������ ĸó
            buttons[i].onClick.AddListener(() => OnClickButton(index));     //��ư Ŭ���� �̺�Ʈ �߻�[publci���� ��ư ��������� ������ �°� ���� ������
            animators[i].updateMode = AnimatorUpdateMode.UnscaledTime;      //�ִϸ��̼��� ����� ������ ���� �ʵ��� ����
        }

        ButtonInteractable();       //ȯ�� ���ķ� �ѹ��̶� 200�� ���� ���ߴٸ� ���� �� ������ ��Ȱ��ȭ
        OnClickButton(0);
    }

    public void OnClickButton(int index)
    {
        ButtonReset();
        buttons[index].GetComponent<Image>().color = GetButtonColor(index); // ��ư ���� ����

        if (index < animators.Length)
        {
            animators[index].SetBool("click", true);    // �ִϸ��̼� ����
        }

        uis[index].SetActive(true);         // ���õ� UI Ȱ��ȭ

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
                .GetUpdateStatuseWindowText();     //�ɷ�ġ �����ִ� ȭ�� ����
        }
    }

    public void ButtonInteractable()       //ȯ�� ���ķ� �ѹ��̶� 200�� ���� ���ߴٸ� ���� �� ������ ��Ȱ��ȭ
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

    private void ButtonReset()          //��ư �� �ʱ�ȭ
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Image>().color = Color.white;       //��ư �� �ʱ�ȭ
        }

        foreach (var animator in animators)
        {
            animator.SetBool("click", false);                       //��ư �ִϸ��̼� �ʱ�ȭ
        }

        foreach (var ui in uis)
        {
            ui.SetActive(false);                //��ư Ȱ�� �ʱ�ȭ
        }

        EnUi_1.SetActive(false);                //gameobject Ȱ�� �ʱ�ȭ
        EnUi_2.SetActive(false);
    }

    private Color GetButtonColor(int index)
    {
        return index switch
        {
            0 => (Color)new Color32(80, 255, 80, 255),// �ʷϻ�
            1 => (Color)new Color32(130, 200, 255, 255),// �ϴû�
            2 => (Color)new Color32(220, 130, 255, 255),// �����
            3 => (Color)new Color32(255, 0, 0, 255),// ������
            _ => Color.white,
        };
    }
}