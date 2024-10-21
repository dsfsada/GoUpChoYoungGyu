using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    private BossEvent boss;

    private void Start()
    {
        boss = GetComponent<BossEvent>();
        slider.maxValue = boss.health;
    }

    void Update()
    {
        slider.value = boss.health;
    }
}
