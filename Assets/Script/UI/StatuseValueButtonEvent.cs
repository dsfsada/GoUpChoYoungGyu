using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//스테이터스 버튼 당 부여되는 스크립트
public class StatuseValueButtonEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Statuse statuseScript;
    private UnderBar underBar;
    private StatuseText statuseText;

    private Coroutine holdButtonCoroutine; // 코루틴 객체를 저장
    private bool isButtonHeld = false;

    public int buttonKind; // 버튼의 종류를 확인(0,1,2,3) 공/체/크공/크확

    private float repeatTime = 0.3f; // 초기 반복 시간
    private const float minRepeatTime = 0.1f; // 최소 반복 시간
    private const float repeatTimeDecreaseRate = 0.05f; // 반복 시간 감소율

    void Start()
    {
        statuseScript = GameObject.Find("Statuse").GetComponent<Statuse>();
        underBar = GameObject.Find("Underbar").GetComponent<UnderBar>();
        statuseText = GameObject.Find("Statuse_scroll").GetComponent<StatuseText>();
    }

    public void OnPointerDown(PointerEventData eventData)       //버튼이 눌릴시 이벤트
    {
        isButtonHeld = true;
        UpgradeStat(buttonKind); // 버튼을 누르는 즉시 강화
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
        repeatTime = 0.3f; // 반복 시간 초기화
    }

    private IEnumerator HoldButtonCoroutine()
    {
        while (isButtonHeld)
        {
            if (underBar.uis[0].activeSelf)
            {
                yield return new WaitForSecondsRealtime(repeatTime); // 반복 시간 간격 설정(리얼타임으로 설정해서 배속에 영향을 받지 않는다.)
                UpgradeStat(buttonKind); // 지속적인 강화
                repeatTime = Mathf.Max(minRepeatTime, repeatTime - repeatTimeDecreaseRate); // 반복 시간을 감소시키되 최소 반복 시간 이하로는 감소하지 않도록 함
            }
            else
            {
                break; // UI가 활성화되지 않은 경우 루프 종료
            }
        }
        holdButtonCoroutine = null; // 코루틴이 끝나면 null로 설정
    }

    private void UpgradeStat(int kind) // 업그레이드 버튼 클릭 시 kind별로 다른 수치가 오름
    {
        if (statuseScript.coin >= statuseScript.upgradeState[kind]+1f)
        {
            statuseScript.CoinUsage(statuseScript.upgradeState[kind] + 1f); // 코인 소비
            statuseScript.upgradeState[kind]++; // Statuse 스크립트와 상호작용
            statuseScript.StatuseUpdate(); // Statuse 스크립트 업데이트
            //if (kind == 1) GameObject.Find("Player").GetComponent<Player>().health += 20;
        }
        statuseText.UpdateStatuseText();         //스테이터스쪽에서 눈으로 보이는 수치들 업데이트
        // else 블록을 추가하여 코인이 부족할 때 처리할 내용 추가 가능
    }
}
