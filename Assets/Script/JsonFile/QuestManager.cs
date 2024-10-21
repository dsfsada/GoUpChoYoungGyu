using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new();

    void Start()
    {
        // �ڷ�ƾ ����
        StartCoroutine(LoadQuestsFromJson("Quest.json"));
    }

    IEnumerator LoadQuestsFromJson(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        string dataAsJson = "";

        // Android������ UnityWebRequest�� ����Ͽ� ������ �ҷ��;� ��
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            using var request = UnityEngine.Networking.UnityWebRequest.Get(filePath);
            yield return request.SendWebRequest();

            if (request.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError("Cannot find file: " + filePath + ", Error: " + request.error);
            }
            else
            {
                dataAsJson = request.downloadHandler.text;
            }
        }
        else
        {
            // Android�� �ƴ� ���, ���� ���� �б� ����
            if (File.Exists(filePath))
            {
                dataAsJson = File.ReadAllText(filePath);
            }
            else
            {
                Debug.LogError("Cannot find file: " + filePath);
                yield break;
            }
        }

        // JSON �����Ͱ� �ε�Ǿ��ٸ� �Ľ��Ͽ� ����Ʈ ����Ʈ�� �߰�
        if (!string.IsNullOrEmpty(dataAsJson))
        {
            Quest[] loadedQuests = JsonHelper.FromJson<Quest>(dataAsJson);
            quests.AddRange(loadedQuests);
        }
    }
}
