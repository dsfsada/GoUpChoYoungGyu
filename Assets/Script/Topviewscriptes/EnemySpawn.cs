using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    Transform rb;

    public GameObject enemy;
    public GameObject bossGolem;
    public GameObject bossDeer;
    public GameObject bossSkul;
    public int spawnPos_x, spawnPos_y, spawnCount;

    public GameObject Entrance;
    public GameObject BossEntrance;

    public int bossLv;
    // Start is called before the first frame update
    public void Start()
    {
        bossLv = 0;

        Entrance.SetActive(false);
        BossEntrance.SetActive(false);

        rb = GetComponent<Transform>();
        rb.transform.position = new Vector3(spawnPos_x, spawnPos_y, 5);
        SetEnemySpawn();
    }

    public void SetEnemySpawn()
    {
        for (int i = 2; i < spawnCount * 2 + 2; i++)
        {
            if (i % 2 == 0)
            {
                Instantiate(enemy, new Vector3(spawnPos_x + i * 2.5f, spawnPos_y - 1, 0), Quaternion.identity);
            }
            else
            {
                if (UnityEngine.Random.Range(0, 10) < 4)
                {
                    Instantiate(enemy, new Vector3(spawnPos_x + i * 2.5f, spawnPos_y - 1, 0), Quaternion.identity);
                }
            }
        }
    }

    public void SetBossSpawn(int _kind)
    {
        switch (_kind)
        {
            case 0:
                Instantiate(bossGolem, new Vector3(spawnPos_x * 5, spawnPos_y - 0.2f, 0), Quaternion.identity);
                Entrance.SetActive(true);
                BossEntrance.SetActive(true);
                break;
            case 1:
                Instantiate(bossDeer, new Vector3(spawnPos_x * 5, spawnPos_y - 0.2f, 0), Quaternion.identity);
                Entrance.SetActive(true);
                BossEntrance.SetActive(true);
                break;
            case 2:
                Instantiate(bossSkul, new Vector3(spawnPos_x * 5, spawnPos_y - 0.2f, 0), Quaternion.identity);
                Entrance.SetActive(true);
                BossEntrance.SetActive(true);
                break;
            default:
                break;
        }

    }

   /* public void setBossGolemSpawn()
    {
        Instantiate(bossGolem, new Vector3(spawnPos_x * 5, spawnPos_y - 0.2f, 0), Quaternion.identity);
        BossEntrance.SetActive(true);
    }

    public void setBossDeerSpawn()
    {
        Instantiate(bossDeer, new Vector3(spawnPos_x * 5, spawnPos_y - 0.2f, 0), Quaternion.identity);
        BossEntrance.SetActive(true);
    }
*/
    public void SetBossLv(int _value)
    {
        bossLv = _value;    //보스 레벨 정하기
    }
}
