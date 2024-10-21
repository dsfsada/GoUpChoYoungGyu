using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ȭ�� ���� ���޽� ���̵� ȿ���� �ο��ϱ� ���� ��ũ��Ʈ
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
