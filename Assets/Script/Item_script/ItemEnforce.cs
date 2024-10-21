using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;

//ui -> Item_enforce_area -> ���� ������Ʈ �����θ��� �ο��� ��ũ��Ʈ

public class ItemEnforce : MonoBehaviour
{
    public int kind, num;

    public Text enforceShameText;
    public Text enforceNumbeText;
    public Text Mounting;


    private string colorText;           //��ȭ ��ư�� �ؽ�Ʈ�� ���� �ο�
    //private GameObject artifact;
    //private GameObject isEquip;

    public Button upgradeButton;
    public Button mountingButton;

    public Sprite mountedImage; //��� ������ �̹���
    public Sprite clearImage; //��� ������ �̹���

    private Artifacts artifactScript;   //��Ƽ��Ʈ ��ũ��Ʈ ����
    private Statuse statuseScript;   //�������ͽ� ��ũ��Ʈ ����

    void Start()
    {
        //artifact = GameObject.Find("Statuse");
        //isEquip = GameObject.Find("ArtifactE");
        artifactScript = GameObject.Find("ArtifactE")?.GetComponent<Artifacts>();   //��Ƽ��Ʈ ��ũ��Ʈ �ҷ�����
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();  //�������ͽ� ��ũ��Ʈ �ҷ�����

        SetColorText();
        UpdateText();

        if (artifactScript.isEquipq[kind, num] == 0)  //��ư�� ���� �� ���� ȭ���� Ŭ�����ϱ� ����
        {
            Mounting.text = "����";
        }else
        {
            Mounting.text = "����";
        }

        upgradeButton.onClick.AddListener(OnButtonClick);
        mountingButton.onClick.AddListener(EquipEffect);   
    }

    private void Update()
    {
        UpdateText();
        //equipEffect();
    }

    private void SetColorText()         //��ȭ text�� ���� ���� �κ�
    {
        int requiredItems = statuseScript.Levelitem[statuseScript.artifactsValues[kind, num]];
        int availableItems = statuseScript.artifactsNum[kind, num];

        string color = requiredItems > availableItems ? "<color=#FF0000>" : "<color=#546E00>";      //��ȭ ��ᰡ �����ϸ� ����, �ùٸ��� �ʷ����� ��Ÿ��
        colorText = $"{color}{requiredItems}{"</color>"}";
    }

    public void OnButtonClick()         //��ư Ŭ����
    {
        //kind�� ����� 0����, 1ü��, 2ũ���� ���� # num ���ι�ȣ��
        statuseScript.UpgradeItem(kind, num);
        statuseScript.StatuseUpdate();             //��ġ �ݿ�
        UpdateText();
 
    }
    public void UpdateText()   //���׷��̵� â�� text�� ������Ʈ ����
    {
        SetColorText();

        int currentArtifactValue = statuseScript.artifactsValues[kind, num];    //���� ��ȭ ��ġ(������� ��Ÿ�� [�ִ� 9��])
        int nextArtifactValue = currentArtifactValue + 1;                       //���� ��ȭ�� ���� ��ġ
        //int artifactStrength = (num + 1) * statuseScript.GetTypeAbility(kind);  //�������� ������ �������� �⺻ �ɷ�ġ�� ���ϰ� ����   EX>3��° �����̸� �⺻ ������ 3�̴�
        float artifactStrength = MathF.Pow(statuseScript.GetTypeAbility(kind), num + 1);     //�������� ������ �������� �⺻ �ɷ�ġ�� ���ϰ� ����   EX>3��° �����̸� �⺻ ������ 2�� 3���̴�[������ �Ҳ��⿡ �׳� �� �� 2��]
        int requiredItems = statuseScript.Levelitem[currentArtifactValue];      //��ȭ�� �ʿ��� ������ ����(��ȭ��ġ�� �°� ���� ��ȭ�� �ʿ��� ��� ������ ��Ÿ�� -> 10�� �ִ�)
        int availableItems = statuseScript.artifactsNum[kind, num];             //���� ������ �ִ� �������� ����

        if (currentArtifactValue == 9)        //��ȭ ��ġ�� 9�� ��� max
        {
            enforceShameText.text = (artifactStrength + (artifactStrength *currentArtifactValue*0.1f)).ToString();
            enforceNumbeText.text = "��ȭ [ MAX ]";
        }
        else
        {
            enforceShameText.text = $"{artifactStrength + (artifactStrength * currentArtifactValue * 0.1f)} -> {artifactStrength + (artifactStrength * nextArtifactValue * 0.1f)}";
            enforceNumbeText.text = $"��ȭ [{colorText}/{availableItems}]";
        }
        //��ȭ [��ȭ�� �ʿ� �������� ���� / ���� �������� ����]
    }

    public void EquipEffect()   //����, ���� 
    {
        artifactScript.Equipq(kind, num);           //��Ƽ��Ʈ ��ũ��Ʈ�� equipq �ڵ� ����
        Inventory.Instance.EquipItemEffect(kind, num);      //������ ���� �� ����
        Mounting.text = artifactScript.isEquipq[kind, num] == 0 ? "����" : "����";  //�ؽ�Ʈ ����
    }
    

}
