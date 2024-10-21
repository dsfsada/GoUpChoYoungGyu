using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RebirthButton : MonoBehaviour
{
    public GameObject rebirthGameObject;
    public Image image;
    public Button reButton;

    private Statuse status;
    private Player player;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();        //자기자신의 애니메이션 가져옴
        //rebirthGameObject = this.gameObject;

        status = GameObject.Find("Statuse").GetComponent<Statuse>();
        player = GameObject.Find("Player").GetComponent<Player>();

        player.ActivateRebirthButton();     //플레이어에서 리버스 버튼 참조를 rebirthButton스크립트에서 가능하게 만들어줌
        ReButtonActive();


        if (status.floor < 100 && status.rebirthCount < 1)
        {
            rebirthGameObject.SetActive(false);
            
        }
        else
        {
            if(gameObject.activeSelf == false)
            {
                rebirthGameObject.SetActive(true);
                
            }
            Destroy(animator);      //애니메이터 컴포넌트 파괴
        }
        //rebirthBtn.onClick.AddListener(RebirthOnClick);

    }


    void OnEnable()
    {
        //setActive되었을때의 효과
        if(reButton == null)
        {
            reButton = GetComponent<Button>();
        }
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

/*    private void RebirthOnClick()
    {
        rebirthWindow.reCointTextUpdate();
    }*/

    public void ReButtonActive()        //한번 환생을 경험 했어도 100층 이전이면 버튼클릭이 안되게 설정
    {
        if(gameObject.activeSelf == true)
        {
            if (status.floor >= 100)
            {
                reButton.enabled = true;  //버튼 클릭 가능함
                image.color = new Color32(255,255,255,255);
            }
            else
            {
                reButton.enabled = false;
                image.color = new Color32(144, 144, 144,255);
            }
        }
    } 

    public bool ReturnActiveValue()
    {
        if (rebirthGameObject.activeSelf == false)
        {
            return true;        //액티브 활성화 되어있으면 player쪽에서 100층 넘었는지 확인
        }
        else
        {
            return false;
        }
    }

    public void OnRebirthButton()       //100층을 넘어가면 리버스 버튼 활성화
    {
        gameObject.SetActive(true);
    }

}
