using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

//ui -> Item_enforce_area -> 게임 오브젝트 개개인마다 부여할 스크립트

public class ItemEnforce : MonoBehaviour
{
    public int kind, num;

    public Text enforceShameText;
    public Text enforceNumbeText;
    public Text Mounting;


    private string colorText;           //강화 버튼의 텍스트의 색상 부여
    //private GameObject artifact;
    //private GameObject isEquip;

    public Button upgradeButton;
    public Button mountingButton;

    public Sprite mountedImage; //장비 장착시 이미지
    public Sprite clearImage; //장비 해제시 이미지

    private Artifacts artifactScript;   //아티팩트 스크립트 선언
    private Statuse statuseScript;   //스테이터스 스크립트 선언

    void Start()
    {
        //artifact = GameObject.Find("Statuse");
        //isEquip = GameObject.Find("ArtifactE");
        artifactScript = GameObject.Find("ArtifactE")?.GetComponent<Artifacts>();   //아티팩트 스크립트 불러오기
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();  //스테이터스 스크립트 불러오기

        SetColorText();
        UpdateText();

        if (artifactScript.isEquipq[kind, num] == 0)  //버튼의 해제 및 장착 화면을 클리어하기 위함
        {
            Mounting.text = "장착";
        }else
        {
            Mounting.text = "해제";
        }

        upgradeButton.onClick.AddListener(OnButtonClick);
        mountingButton.onClick.AddListener(EquipEffect);   
    }

    private void Update()
    {
        UpdateText();
        //equipEffect();
    }

    private void SetColorText()         //강화 text의 색상 변경 부분
    {
        int requiredItems = statuseScript.Levelitem[statuseScript.artifactsValues[kind, num]];
        int availableItems = statuseScript.artifactsNum[kind, num];

        string color = requiredItems > availableItems ? "<color=#FF0000>" : "<color=#546E00>";      //강화 재료가 부족하면 빨강, 올바르면 초록으로 나타남
        colorText = $"{color}{requiredItems}{"</color>"}";
    }

    public void OnButtonClick()         //버튼 클릭시
    {
        //kind는 장비의 0공격, 1체력, 2크뎀을 선택 # num 세부번호들
        statuseScript.UpgradeItem(kind, num);
        statuseScript.StatuseUpdate();             //수치 반영
        UpdateText();
 
    }
    public void UpdateText()   //업그레이드 창의 text를 업데이트 해줌
    {
        SetColorText();

        int currentArtifactValue = statuseScript.artifactsValues[kind, num];    //현재 강화 수치(몇강인지를 나타냄 [최대 9강])
        int nextArtifactValue = currentArtifactValue + 1;                       //다음 강화시 나올 수치
        //int artifactStrength = (num + 1) * statuseScript.GetTypeAbility(kind);  //아이템의 순번이 높을수록 기본 능력치가 강하게 만듬   EX>3번째 무기이면 기본 뎀지가 3이다
        float artifactStrength = MathF.Pow(statuseScript.GetTypeAbility(kind), num + 1);     //아이템의 순번이 높을수록 기본 능력치가 강하게 만듬   EX>3번째 무기이면 기본 뎀지가 2의 3승이다[곱으로 할꺼기에 그냥 싹 다 2임]
        int requiredItems = statuseScript.Levelitem[currentArtifactValue];      //강화에 필요한 아이템 개수(강화수치에 맞게 다음 강화에 필요한 재료 개수를 나타냄 -> 10이 최대)
        int availableItems = statuseScript.artifactsNum[kind, num];             //현재 가지고 있는 아이템의 개수

        if (currentArtifactValue == 9)        //강화 수치가 9일 경우 max
        {
            enforceShameText.text = (artifactStrength + (artifactStrength *currentArtifactValue*0.1f)).ToString();
            enforceNumbeText.text = "강화 [ MAX ]";
        }
        else
        {
            enforceShameText.text = $"{artifactStrength + (artifactStrength * currentArtifactValue * 0.1f)} -> {artifactStrength + (artifactStrength * nextArtifactValue * 0.1f)}";
            enforceNumbeText.text = $"강화 [{colorText}/{availableItems}]";
        }
        //강화 [강화시 필요 아이템의 개수 / 현재 아이템의 개수]
    }

    public void EquipEffect()   //장착, 해제 
    {
        artifactScript.Equipq(kind, num);           //아티팩트 스크립트의 equipq 코드 실행
        Inventory.Instance.EquipItemEffect(kind, num);      //아이템 장착 및 해제
        Mounting.text = artifactScript.isEquipq[kind, num] == 0 ? "장착" : "해제";  //텍스트 변경
    }
    

}
