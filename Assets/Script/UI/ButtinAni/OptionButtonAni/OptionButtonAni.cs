using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtonAni : MonoBehaviour
{
    public Button myButton;                            //�� ��ư
    public Animator animator;                          //��ư Ŭ���� �ִϸ��̼�

    // Update is called once per frame
    void Start()
    {
        myButton = GetComponent<Button>();

        if (animator.GetInteger("click") == 1)
        {
            animator.SetInteger("click", 2);        //OFF������ �ִϸ��̼�
        }
        myButton.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()    //��ư Ŭ���� �κ� �ɷ�ġ �����ִ� �ִϸ��̼� ���(1: ON, 2 : OFF)
    {
        if (animator.GetInteger("click") == 1)       //ON�ִϸ��̼� ��»��¿��� ��ư Ŭ���� OFF�ִϸ� ���
        {
            animator.SetInteger("click", 2);
        }
        else
        {
            animator.SetInteger("click", 1);
        }
    }
}
