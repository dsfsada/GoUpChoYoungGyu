using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReStatuseValueButtonEvent : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    private Statuse statuseScript;
    private UnderBar underBar;
    private StatuseText statuseText;

    private Coroutine holdButtonCoroutine; // �ڷ�ƾ ��ü�� ����
    private bool isButtonHeld = false;

    public int buttonKind; // ��ư�� ������ Ȯ��(0,1,2,3) ��/ü/ũ��/ũȮ

    private float repeatTime = 0.3f; // �ʱ� �ݺ� �ð�
    private const float minRepeatTime = 0.1f; // �ּ� �ݺ� �ð�
    private const float repeatTimeDecreaseRate = 0.05f; // �ݺ� �ð� ������

    // Start is called before the first frame update
    void Start()
    {
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();
        underBar = GameObject.Find("Underbar").GetComponent<UnderBar>();
        statuseText = GameObject.Find("Statuse_scroll").GetComponent<StatuseText>();
    }

    public void OnPointerDown(PointerEventData eventData)       //��ư�� ������ �̺�Ʈ
    {
        isButtonHeld = true;
        UpgradeStat(buttonKind); // ��ư�� ������ ��� ��ȭ
        if (holdButtonCoroutine == null)
        {
            holdButtonCoroutine = StartCoroutine(HoldButtonCoroutine());
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonHeld = false;
        if (holdButtonCoroutine != null)
        {
            StopCoroutine(holdButtonCoroutine);
            holdButtonCoroutine = null;
        }
        repeatTime = 0.3f; // �ݺ� �ð� �ʱ�ȭ
    }

    private IEnumerator HoldButtonCoroutine()
    {
        while (isButtonHeld)
        {
            if (underBar.uis[0].activeSelf)
            {
                yield return new WaitForSecondsRealtime(repeatTime); // �ݺ� �ð� ���� ����(����Ÿ������ �����ؼ� ��ӿ� ������ ���� �ʴ´�.)
                UpgradeStat(buttonKind); // �������� ��ȭ
                repeatTime = Mathf.Max(minRepeatTime, repeatTime - repeatTimeDecreaseRate); // �ݺ� �ð��� ���ҽ�Ű�� �ּ� �ݺ� �ð� ���Ϸδ� �������� �ʵ��� ��
            }
            else
            {
                break; // UI�� Ȱ��ȭ���� ���� ��� ���� ����
            }
        }
        holdButtonCoroutine = null; // �ڷ�ƾ�� ������ null�� ����
    }

    private void UpgradeStat(int kind) // ���׷��̵� ��ư Ŭ�� �� kind���� �ٸ� ��ġ�� ����
    {
        if (statuseScript.reCoin >= (statuseScript.reUpgradeState[kind]>0? statuseScript.reUpgradeState[kind]*5f : 1f))
        {
            Debug.Log("���� �Һ�");
            statuseScript.ReCoinUsage(statuseScript.reUpgradeState[kind]*5f); // ���� �Һ�
            statuseScript.reUpgradeState[kind]++; // Statuse ��ũ��Ʈ�� ��ȣ�ۿ�
            statuseScript.StatuseUpdate(); // Statuse ��ũ��Ʈ ������Ʈ
        }
        else
        {
            Debug.Log("���� ����");
        }
        statuseText.UpdateStatuseText();         //�������ͽ��ʿ��� ������ ���̴� ��ġ�� ������Ʈ
        // else ����� �߰��Ͽ� ������ ������ �� ó���� ���� �߰� ����
    }
}
