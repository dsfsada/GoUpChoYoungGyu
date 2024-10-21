using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//boss_menu하위 보스lv 하위 enter보스 버튼쪽에 할당될 예정
public class EnterBoss : MonoBehaviour
{
    public Button enterButton;
    private BossLvUi bossLvUiScript;
    // Start is called before the first frame update

    void Start()
    {
        enterButton = GetComponent<Button>();
        bossLvUiScript = transform.parent.parent.GetComponent<BossLvUi>();      //현재 위치에섯 부모,부모의 스크립트를 가져옴
        //0은 골렘, 1은 사슴        , 1,2,3단계로 나뉘어짐
        enterButton.onClick.AddListener(() => EnterButtonClick(bossLvUiScript.bossTypeValue, bossLvUiScript.bossLvValue));
    }

    private void EnterButtonClick(int _type, int _bossLv)
    {
        GameObject.Find("Player").GetComponent<Player>().MoveToStartPosition(true, _type, _bossLv);
    }

}
