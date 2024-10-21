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
        animator = GetComponent<Animator>();        //�ڱ��ڽ��� �ִϸ��̼� ������
        //rebirthGameObject = this.gameObject;

        status = GameObject.Find("Statuse").GetComponent<Statuse>();
        player = GameObject.Find("Player").GetComponent<Player>();

        player.ActivateRebirthButton();     //�÷��̾�� ������ ��ư ������ rebirthButton��ũ��Ʈ���� �����ϰ� �������
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
            Destroy(animator);      //�ִϸ����� ������Ʈ �ı�
        }
        //rebirthBtn.onClick.AddListener(RebirthOnClick);

    }


    void OnEnable()
    {
        //setActive�Ǿ������� ȿ��
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

    public void ReButtonActive()        //�ѹ� ȯ���� ���� �߾ 100�� �����̸� ��ưŬ���� �ȵǰ� ����
    {
        if(gameObject.activeSelf == true)
        {
            if (status.floor >= 100)
            {
                reButton.enabled = true;  //��ư Ŭ�� ������
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
            return true;        //��Ƽ�� Ȱ��ȭ �Ǿ������� player�ʿ��� 100�� �Ѿ����� Ȯ��
        }
        else
        {
            return false;
        }
    }

    public void OnRebirthButton()       //100���� �Ѿ�� ������ ��ư Ȱ��ȭ
    {
        gameObject.SetActive(true);
    }

}
