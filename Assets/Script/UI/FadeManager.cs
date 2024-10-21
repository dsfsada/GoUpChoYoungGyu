using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//화면 끝에 도달시 페이드 효과를 부여하기 위한 스크립트
public class FadeManager : MonoBehaviour
{
    public SpriteRenderer black;
    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);
/*
    public void FadeOut(float _speed = 0.02f)
    {
        //StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(_speed));
    }*/
    public IEnumerator FadeOutCoroutine(float _speed)
    {
        color = black.color;
        while(color.a < 1f)
        {
            color.a += _speed;
            black.color = color;
            yield return waitTime;
        }
    }

/*    public void FadeIn(float _speed = 0.02f)
    {
        //StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(_speed));
    }*/
    public IEnumerator FadeInCoroutine(float _speed)
    {
        color = black.color;
        while (color.a > 0f)
        {
            color.a -= _speed;
            black.color = color;
            yield return waitTime;
        }
    }
}
