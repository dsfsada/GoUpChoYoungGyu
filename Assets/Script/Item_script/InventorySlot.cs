using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;


public class InventorySlot : MonoBehaviour
{
    //인벤토리 내 있는 슬롯의 아이콘 변경을 위한 스크립트

    //public Image icon;
    
    //private Inventory inventory;

    public int kind, num;   //장비 번호
    private Button button;



    //이미지 가져오기
    public Image spriteRenderer;
    public Sprite mountedImage;
    public Sprite clearImage;

    public Image[] childSpriteRenderer;
    public Sprite[] itemImage;      //스프라이트의 배열 저장

    private void Awake()
    {
        spriteRenderer = GetComponent<Image>(); //자기 자신의 스프라이트 가져옴
        childSpriteRenderer = GetComponentsInChildren<Image>(true);  //자기 포함 자식 스프라이트 가져옴

        mountedImage = Resources.Load<Sprite>("Sprites/Ui/ItemUi/ex");
        clearImage = Resources.Load<Sprite>("Sprites/Ui/ItemUi/64 Light");
        itemImage = Resources.LoadAll<Sprite>("ItemIcon/Item");

        button = GetComponent<Button>();

       

        if (GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum[kind, num] == 0 && GameObject.Find("Statuse").GetComponent<Statuse>().artifactsValues[kind, num] == 0)
        {
            button.interactable = false;            //아이템이 없을 경우 버튼의 비활성화
            ImgInactive();
        }

        /*foreach (var image in childSpriteRenderer)      //자기 자신의 이미지는 삭제
        {
            // 현재 오브젝트에 있는 Image를 제외하고 작업 수행
            if (image.gameObject != this.gameObject)
            {
                image.sprite = itemImage[num + kind * 12]; //특정 배열의 스프라이트를 가져옴
            }
        }*/
    }

    private void Start()
    {
        equipEffect();                          //게임 시작 때 아이템 장착 활성화시 이미지 변경을 위한 start문에 넣어둠
        button.onClick.AddListener(OnClickButton);
        foreach (var image in childSpriteRenderer)      //자기 자신의 이미지는 삭제
        {
            // 현재 오브젝트에 있는 Image를 제외하고 작업 수행
            if (image.gameObject != this.gameObject)
            {
                image.sprite = itemImage[num + kind * 12]; //특정 배열의 스프라이트를 가져옴
            }
        }

    }

    public void Additem()
    {
        if (button.interactable == false)
        {
            //StartCoroutine(AdditemCoroutine());
            button.interactable = true;                 
            ImgActivation();                            //하위 아이콘 색상 변경
        }
    }

    public void ImgInactive()          //아이콘의 처음 비활성화의 색상을 검정으로 지정
    {
        foreach (Transform child in transform)   // 자식 오브젝트에서 Image 컴포넌트를 가져옴
        {
            Image img = child.GetComponent<Image>();

            if (img != null)
            {
                img.color = Color.gray;                 //색상을 회색으로 설정
            }
        }
    }
    
    public void ImgActivation()
    {
        foreach (Transform child in transform)   // 자식 오브젝트에서 Image 컴포넌트를 가져옴
        {
            Image img = child.GetComponent<Image>();

            if (img != null)
            {
                img.color = Color.white;                 //색상을 흰색으로 설정
            }
        }
    }
    //위 3개의 코드는 버튼 활성화의 관여함

    public void SetSlotKindNum(int _kind, int _num) //kind와 num 번호 지정
    {
        kind = _kind;
        num  = _num;
    }

    public void OnClickButton()
    {
        Inventory.Instance.OnSetActive(kind,num);//버튼 클릭시 업그레이드 화면 on   
    }

    public void equipEffect()   //자신의 오브젝트의 장착, 해제시 이미지 변경 
    {

        if (GameObject.Find("ArtifactE").GetComponent<Artifacts>().isEquipq[kind, num] == 1)  //특정 번호(현재 아이템)의 장착여부(1,0) 1이면 장착on
        {
            spriteRenderer.sprite = mountedImage;   //장착상태의 icon
        }
        else
        {
            spriteRenderer.sprite = clearImage; //해제상태의 icon
        }
    }
}
