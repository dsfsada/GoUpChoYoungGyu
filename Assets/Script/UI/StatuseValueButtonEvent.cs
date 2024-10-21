using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�������ͽ� ��ư �� �ο��Ǵ� ��ũ��Ʈ
public class StatuseValueButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
        if (statuseScript.coin >= statuseScript.upgradeState[kind]+1f)
        {
            statuseScript.CoinUsage(statuseScript.upgradeState[kind] + 1f); // ���� �Һ�
            statuseScript.upgradeState[kind]++; // Statuse ��ũ��Ʈ�� ��ȣ�ۿ�
            statuseScript.StatuseUpdate(); // Statuse ��ũ��Ʈ ������Ʈ
            //if (kind == 1) GameObject.Find("Player").GetComponent<Player>().health += 20;
        }
        statuseText.UpdateStatuseText();         //�������ͽ��ʿ��� ������ ���̴� ��ġ�� ������Ʈ
        // else ����� �߰��Ͽ� ������ ������ �� ó���� ���� �߰� ����
    }
}
