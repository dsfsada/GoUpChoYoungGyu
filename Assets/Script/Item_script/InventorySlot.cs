using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputManagerEntry;


public class InventorySlot : MonoBehaviour
{
    //�κ��丮 �� �ִ� ������ ������ ������ ���� ��ũ��Ʈ

    //public Image icon;
    
    //private Inventory inventory;

    public int kind, num;   //��� ��ȣ
    private Button button;



    //�̹��� ��������
    public Image spriteRenderer;
    public Sprite mountedImage;
    public Sprite clearImage;

    public Image[] childSpriteRenderer;
    public Sprite[] itemImage;      //��������Ʈ�� �迭 ����

    private void Awake()
    {
        spriteRenderer = GetComponent<Image>(); //�ڱ� �ڽ��� ��������Ʈ ������
        childSpriteRenderer = GetComponentsInChildren<Image>(true);  //�ڱ� ���� �ڽ� ��������Ʈ ������

        mountedImage = Resources.Load<Sprite>("Sprites/Ui/ItemUi/ex");
        clearImage = Resources.Load<Sprite>("Sprites/Ui/ItemUi/64 Light");
        itemImage = Resources.LoadAll<Sprite>("ItemIcon/Item");

        button = GetComponent<Button>();

       

        if (GameObject.Find("Statuse").GetComponent<Statuse>().artifactsNum[kind, num] == 0 && GameObject.Find("Statuse").GetComponent<Statuse>().artifactsValues[kind, num] == 0)
        {
            button.interactable = false;            //�������� ���� ��� ��ư�� ��Ȱ��ȭ
            ImgInactive();
        }

        /*foreach (var image in childSpriteRenderer)      //�ڱ� �ڽ��� �̹����� ����
        {
            // ���� ������Ʈ�� �ִ� Image�� �����ϰ� �۾� ����
            if (image.gameObject != this.gameObject)
            {
                image.sprite = itemImage[num + kind * 12]; //Ư�� �迭�� ��������Ʈ�� ������
            }
        }*/
    }

    private void Start()
    {
        equipEffect();                          //���� ���� �� ������ ���� Ȱ��ȭ�� �̹��� ������ ���� start���� �־��
        button.onClick.AddListener(OnClickButton);
        foreach (var image in childSpriteRenderer)      //�ڱ� �ڽ��� �̹����� ����
        {
            // ���� ������Ʈ�� �ִ� Image�� �����ϰ� �۾� ����
            if (image.gameObject != this.gameObject)
            {
                image.sprite = itemImage[num + kind * 12]; //Ư�� �迭�� ��������Ʈ�� ������
            }
        }

    }

    public void Additem()
    {
        if (button.interactable == false)
        {
            //StartCoroutine(AdditemCoroutine());
            button.interactable = true;                 
            ImgActivation();                            //���� ������ ���� ����
        }
    }

    public void ImgInactive()          //�������� ó�� ��Ȱ��ȭ�� ������ �������� ����
    {
        foreach (Transform child in transform)   // �ڽ� ������Ʈ���� Image ������Ʈ�� ������
        {
            Image img = child.GetComponent<Image>();

            if (img != null)
            {
                img.color = Color.gray;                 //������ ȸ������ ����
            }
        }
    }
    
    public void ImgActivation()
    {
        foreach (Transform child in transform)   // �ڽ� ������Ʈ���� Image ������Ʈ�� ������
        {
            Image img = child.GetComponent<Image>();

            if (img != null)
            {
                img.color = Color.white;                 //������ ������� ����
            }
        }
    }
    //�� 3���� �ڵ�� ��ư Ȱ��ȭ�� ������

    public void SetSlotKindNum(int _kind, int _num) //kind�� num ��ȣ ����
    {
        kind = _kind;
        num  = _num;
    }

    public void OnClickButton()
    {
        Inventory.Instance.OnSetActive(kind,num);//��ư Ŭ���� ���׷��̵� ȭ�� on   
    }

    public void equipEffect()   //�ڽ��� ������Ʈ�� ����, ������ �̹��� ���� 
    {

        if (GameObject.Find("ArtifactE").GetComponent<Artifacts>().isEquipq[kind, num] == 1)  //Ư�� ��ȣ(���� ������)�� ��������(1,0) 1�̸� ����on
        {
            spriteRenderer.sprite = mountedImage;   //���������� icon
        }
        else
        {
            spriteRenderer.sprite = clearImage; //���������� icon
        }
    }
}
