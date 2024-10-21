using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditText : MonoBehaviour
{
    public Text floorText;
    public Text coinText;
    public Text ReText;

    public Statuse statuse;

    private float currentCoin = 0;
    private float currentReCoin = 0;

    private void Start()
    {
        UpdateFloor();

        statuse = GameObject.Find("Statuse").GetComponent<Statuse>();

        UpdateCoin(statuse.coin);
        UpdateReCoin(statuse.reCoin);
    }

    private void Update()
    {
        if (currentCoin != statuse.coin)  //���ΰ��� ������ ������ ���� ������Ʈ
        {
            UpdateCoin(statuse.coin);
            currentCoin = statuse.coin;
        }

        if (currentReCoin != statuse.reCoin)  //���ΰ��� ������ ������ ���� ������Ʈ
        {
            UpdateReCoin(statuse.reCoin);
            currentReCoin = statuse.reCoin;
        }
    }

    public void UpdateFloor()
    {
        floorText.text = "STAGE\n" + GameObject.Find("Statuse").GetComponent<Statuse>().floor;
    }
    public void UpdateCoin(float _coin)
    {
        coinText.text = _coin.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
    }
    public void UpdateReCoin(float _recCoin)
    {
        ReText.text = _recCoin.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
    }

}
