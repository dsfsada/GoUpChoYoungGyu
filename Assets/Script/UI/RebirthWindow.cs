using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebirthWindow : MonoBehaviour
{
    public Button rebirthBtn;
    public Text reCoinText;

    public Statuse status;
    public Player player;
    public StatuseText statuseText;


    private void Start()
    {
        //rebirthGameObject = this.gameObject;

        status = GameObject.Find("Statuse").GetComponent<Statuse>();
        player = GameObject.Find("Player").GetComponent<Player>();
        statuseText = GameObject.Find("Statuse_scroll").GetComponent<StatuseText>();

        player.ActivateRebirthButton();     //플레이어에서 리버스 버튼 참조를 rebirthButton스크립트에서 가능하게 만들어줌

        reCoinText.text = $"{status.reCoin} + {(status.floor/10f).ToString("F0")}";
        

        rebirthBtn.onClick.AddListener(RebirthOnClick);
    }

    void OnEnable()     //setActive가 활성화 될때마다 코드 실행
    {
        reCoinText.text = $"{GameObject.Find("Statuse").GetComponent<Statuse>().reCoin} + {(GameObject.Find("Statuse").GetComponent<Statuse>().floor / 10f).ToString("F0")}";
    }

    private void RebirthOnClick()
    {
        // 초기화 해야할 목록 coin floor enhance 
        player.isFadeCoroutineActive = false;

        for (int i = 0; i < 5; i++)
            status.upgradeState[i] = 0;

        status.reCoin += (int)(status.floor / 10.0f);
        status.rebirthCount++;

        status.floor = 1;
        status.coin = 100;
        player.pSpeed = 1;

        statuseText.UpdateStatuseText();
        status.StatuseUpdate();
    }
}
