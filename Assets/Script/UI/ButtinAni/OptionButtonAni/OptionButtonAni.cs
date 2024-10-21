using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtonAni : MonoBehaviour
{
    public Button myButton;                            //내 버튼
    public Animator animator;                          //버튼 클릭시 애니메이션

    // Update is called once per frame
    void Start()
    {
        myButton = GetComponent<Button>();

        if (animator.GetInteger("click") == 1)
        {
            animator.SetInteger("click", 2);        //OFF형태의 애니메이션
        }
        myButton.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()    //버튼 클릭시 부분 능력치 보여주는 애니메이션 출력(1: ON, 2 : OFF)
    {
        if (animator.GetInteger("click") == 1)       //ON애니메이션 출력상태에서 버튼 클릭시 OFF애니메 출력
        {
            animator.SetInteger("click", 2);
        }
        else
        {
            animator.SetInteger("click", 1);
        }
    }
}
