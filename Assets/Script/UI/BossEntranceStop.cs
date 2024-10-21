using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntranceStop : MonoBehaviour
{
    public GameObject Entrance;

    private void Start()
    {
        Entrance = transform.parent.gameObject;
    }
    //BossEntrance를 false로 바꾸는 문장, 애니메이터에다가 집어넣음
    public void stop()
    {
        gameObject.SetActive(false);
        Entrance.SetActive(false);
    }
}
