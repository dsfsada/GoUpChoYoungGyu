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
        childImage = GetComponentInChildren<Image>();           //�ڽ� �̹����� ������
    }

    // ����Ʈ �Ϸ� �� ������ ȿ�� ����
    public void StartFlashing()
    {
        // ������ ȿ�� ����
        if (!isFlashing)
        {
            isFlashing = true;
            flashCoroutine = StartCoroutine(FlashButton());
        }

    }

    // �������� �����ϴ� �޼���
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

            // ��ư ������ ������� ����
            childImage.color = new Color32(120, 120, 120, 69); //ȸ��
        }
    }

    private IEnumerator FlashButton()
    {
        while (isFlashing)
        {
            // ��ư ���� ����
            childImage.color = new Color32(255, 255, 255, 150); //���
            yield return new WaitForSeconds(0.3f);
            childImage.color = new Color32(120, 120, 120, 69); // ȸ��
            yield return new WaitForSeconds(0.3f);
        }
    }
}
