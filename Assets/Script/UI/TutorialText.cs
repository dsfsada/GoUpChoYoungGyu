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
            Debug.LogError("QuestManager�� ã�� �� �����ϴ�.");
            enabled = false; // ��ũ��Ʈ ��Ȱ��ȭ
            return;
        }
    }

    private void Start()
    {
        stSelectUi = GameObject.Find("StSelectUi").GetComponent<StSelectUi>();
        // QuestManager�� ����Ʈ�� �ε��� ������ ����մϴ�.
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
        CheckQuestFlashing();   //����Ʈ ������ ǥ��
    }

    private IEnumerator WaitForQuestsToLoad()
    {
        // QuestManager�� ����Ʈ�� �ε��� ������ ����մϴ�.
        while (questManager.quests == null || questManager.quests.Count == 0)
        {
            yield return null;
        }

        UpdateQuestText();
        //CheckQuestFlashing();   //����Ʈ ������ ǥ��

    }

    private void UpdateQuestText()  // json���Ͽ��� �ε��� ����Ʈ�� ���������� ����Ʈ text�� ǥ��
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
            questText.text = "����Ʈ�� ã�� �� �����ϴ�.";
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
            questText.text = "��� ����Ʈ�� �Ϸ��߽��ϴ�.";
        }
    }

    private int CheckQuest()
    {
        questType = questManager.quests[questOrder].questType;           //����Ʈ Ÿ��(�ɷ�ġ ��ȭ�� ����Ʈ����, ���� ��� ����Ʈ���� ��)
        targetValue = questManager.quests[questOrder].targetValue;

        switch (questType)      //����Ʈ ��ư Ŭ���� �̺�Ʈ �߻�
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
                    underBar.OnClickButton(0);      //�������ͽ� ��ȭ ȭ��
                    stSelectUi.OnClickButton(0);    //�������ͽ� ��ȭ ȭ�鿡�� �⺻ �ɷ�ġ ��ȭ ȭ��
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
                    underBar.OnClickButton(0);      //�������ͽ� ��ȭ ȭ��
                    stSelectUi.OnClickButton(0);    //�������ͽ� ��ȭ ȭ�鿡�� �⺻ �ɷ�ġ ��ȭ ȭ��
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
        questType = questManager.quests[questOrder].questType;           //����Ʈ Ÿ��(�ɷ�ġ ��ȭ�� ����Ʈ����, ���� ��� ����Ʈ���� ��)
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
