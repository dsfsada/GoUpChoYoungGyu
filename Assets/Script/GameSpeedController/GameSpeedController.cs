using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSpeedController : MonoBehaviour
{
    public float setSpeed = 1f;     //�⺻ ��� ����

    public Button speedButton;

    public Image mainImage;
    public Sprite[] spirteImgs;

    void Start()
    {
        Time.timeScale = setSpeed;
        speedButton = GetComponent<Button>();
        mainImage = GetComponent<Image>();
        mainImage.sprite = spirteImgs[0];

        speedButton.onClick.AddListener(OnClickButton);
    }

    /*public void SetGameSpeed(float speedMultiplier)     //�ܺο��� �ӵ� ����
    {
        setSpeed = speedMultiplier;
        Time.timeScale = setSpeed;
    }*/

    public void OnClickButton()
    {
        if(setSpeed == 1f)  //1�̾����� Ŭ������ ���
        {
            mainImage.sprite = spirteImgs[1];
            setSpeed = 2f;
        }else if (setSpeed == 2f)
        {
            mainImage.sprite = spirteImgs[2];
            setSpeed = 3f;
        }
        else
        {
            mainImage.sprite = spirteImgs[0];
            setSpeed = 1f;
        }

        Time.timeScale = setSpeed;
    }
}
