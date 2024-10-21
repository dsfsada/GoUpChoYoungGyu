using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour
{
    private string questType;
    private int targetValue;

    public Text questText;
    public Button questBtn;
    public int questOrder = 1;

    private QuestCompletionEffect questCompletionEffectScript;
    public Statuse statuse;
    private QuestManager questManager;
    public StSelectUi stSelectUi;
    private UnderBar underBar;
    

    private void Awake()
    {
        questText = GetComponent<Text>();
        questBtn = GetComponent<Button>();
        questCompletionEffectScript = GetComponent<QuestCompletionEffect>();
        questManager = FindObjectOfType<QuestManager>();
    
        underBar = FindObjectOfType<UnderBar>();
        statuse = FindObjectOfType<Statuse>();

        if (questManager == null)
        {
            Debug.LogError("QuestManager를 찾을 수 없습니다.");
            enabled = false; // 스크립트 비활성화
            return;
        }
    }

    private void Start()
    {
        stSelectUi = GameObject.Find("StSelectUi").GetComponent<StSelectUi>();
        // QuestManager가 퀘스트를 로드할 때까지 대기합니다.
        StartCoroutine(WaitForQuestsToLoad());
        questBtn.onClick.AddListener(TaskOnClick);
    }

    private void OnDestroy()
    {
        questBtn.onClick.RemoveListener(TaskOnClick);
    }

    private void FixedUpdate()
    {
        UpdateQuestText();
        CheckQuestFlashing();   //퀘스트 깜빡임 표시
    }

    private IEnumerator WaitForQuestsToLoad()
    {
        // QuestManager가 퀘스트를 로드할 때까지 대기합니다.
        while (questManager.quests == null || questManager.quests.Count == 0)
        {
            yield return null;
        }

        UpdateQuestText();
        //CheckQuestFlashing();   //퀘스트 깜빡임 표시

    }

    private void UpdateQuestText()  // json파일에서 로드한 리스트를 지속적으로 퀘스트 text에 표시
    {
        Quest quest = questManager.quests[questOrder];
        if (quest != null && questOrder < questManager.quests.Count)
        {
            questText.text = quest.questName;
            if (quest.questType == "DefeatEnemies")
                return;
            else if (quest.questType == "IncreaseAttack")
                questText.text += "\n( " + statuse.upgradeState[0] + " / " + quest.targetValue + " )";
            else if (quest.questType == "IncreaseHealth")
                questText.text += "\n( " + statuse.upgradeState[1] + " / " + quest.targetValue + " )";
            else if (quest.questType == "ReachFloor")
                questText.text += "\n( " + statuse.floor + " / " + quest.targetValue + " )";
        }
        else
        {
            questText.text = "퀘스트를 찾을 수 없습니다.";
        }
    }

    private void TaskOnClick()
    {
        if (questOrder < questManager.quests.Count - 1)
        {
            statuse.coin += CheckQuest();
            UpdateQuestText();
        }
        else
        {
            questText.text = "모든 퀘스트를 완료했습니다.";
        }
    }

    private int CheckQuest()
    {
        questType = questManager.quests[questOrder].questType;           //퀘스트 타입(능력치 강화형 퀘스트인지, 몬스터 잡는 퀘스트인지 등)
        targetValue = questManager.quests[questOrder].targetValue;

        switch (questType)      //퀘스트 버튼 클릭시 이벤트 발생
        {
            case "DefeatEnemies":
                questOrder++;
                break;

            case "IncreaseAttack":
                if (statuse.upgradeState[0] >= targetValue)
                {
                    questOrder++;
                    return (int)questManager.quests[questOrder].rewardPoints;
                }
                else
                {
                    underBar.OnClickButton(0);      //스테이터스 강화 화면
                    stSelectUi.OnClickButton(0);    //스테이터스 강화 화면에서 기본 능력치 강화 화면
                }
                break;

            case "IncreaseHealth":
                if (statuse.upgradeState[1] >= targetValue)
                {
                    questOrder++;
                    return (int)questManager.quests[questOrder].rewardPoints;
                }
                else
                {
                    underBar.OnClickButton(0);      //스테이터스 강화 화면
                    stSelectUi.OnClickButton(0);    //스테이터스 강화 화면에서 기본 능력치 강화 화면
                }
                break;

            case "ReachFloor":
                if (statuse.floor > targetValue)
                {
                    questOrder++;
                    return (int)questManager.quests[questOrder].rewardPoints;
                }
                    
                break;

            default:
                questOrder++;
                break;
        }

        return 0;
    }

    private void CheckQuestFlashing()
    {
        questType = questManager.quests[questOrder].questType;           //퀘스트 타입(능력치 강화형 퀘스트인지, 몬스터 잡는 퀘스트인지 등)
        targetValue = questManager.quests[questOrder].targetValue;

        bool shouldFlash = false;

        switch (questType)
        {
            case "DefeatEnemies":
                shouldFlash = true;
                break;
            case "IncreaseAttack":
                shouldFlash = statuse.upgradeState[0] >= targetValue;
                break;
            case "IncreaseHealth":
                shouldFlash = statuse.upgradeState[1] >= targetValue;
                break;
            case "ReachFloor":
                shouldFlash = statuse.floor > targetValue;
                break;
        }

        if (shouldFlash)
            questCompletionEffectScript.StartFlashing();
        else
            questCompletionEffectScript.StopFlashing();
    }
}
