using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompletionEffect : MonoBehaviour
{
    private Image childImage;    

    private bool isFlashing = false;
    private Coroutine flashCoroutine;

    private void Start()
    {
        childImage = GetComponentInChildren<Image>();           //자식 이미지를 가져옴
    }

    // 퀘스트 완료 시 깜빡임 효과 시작
    public void StartFlashing()
    {
        // 깜빡임 효과 시작
        if (!isFlashing)
        {
            isFlashing = true;
            flashCoroutine = StartCoroutine(FlashButton());
        }

    }

    // 깜빡임을 중지하는 메서드
    public void StopFlashing()
    {
        if (isFlashing)
        {
            isFlashing = false;
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
                flashCoroutine = null;
            }

            // 버튼 색상을 원래대로 복구
            childImage.color = new Color32(120, 120, 120, 69); //회색
        }
    }

    private IEnumerator FlashButton()
    {
        while (isFlashing)
        {
            // 버튼 색상 변경
            childImage.color = new Color32(255, 255, 255, 150); //흰색
            yield return new WaitForSeconds(0.3f);
            childImage.color = new Color32(120, 120, 120, 69); // 회색
            yield return new WaitForSeconds(0.3f);
        }
    }
}
