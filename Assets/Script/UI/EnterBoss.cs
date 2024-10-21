using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//boss_menu���� ����lv ���� enter���� ��ư�ʿ� �Ҵ�� ����
public class EnterBoss : MonoBehaviour
{
    public Button enterButton;
    private BossLvUi bossLvUiScript;
    // Start is called before the first frame update

    void Start()
    {
        enterButton = GetComponent<Button>();
        bossLvUiScript = transform.parent.parent.GetComponent<BossLvUi>();      //���� ��ġ���� �θ�,�θ��� ��ũ��Ʈ�� ������
        //0�� ��, 1�� �罿        , 1,2,3�ܰ�� ��������
        enterButton.onClick.AddListener(() => EnterButtonClick(bossLvUiScript.bossTypeValue, bossLvUiScript.bossLvValue));
    }

    private void EnterButtonClick(int _type, int _bossLv)
    {
        GameObject.Find("Player").GetComponent<Player>().MoveToStartPosition(true, _type, _bossLv);
    }

}
