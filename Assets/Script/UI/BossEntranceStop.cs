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
    //BossEntrance�� false�� �ٲٴ� ����, �ִϸ����Ϳ��ٰ� �������
    public void stop()
    {
        gameObject.SetActive(false);
        Entrance.SetActive(false);
    }
}
