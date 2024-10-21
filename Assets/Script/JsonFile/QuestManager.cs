using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests = new();

    void Start()
    {
        // 코루틴 시작
        StartCoroutine(LoadQuestsFromJson("Quest.json"));
    }

    IEnumerator LoadQuestsFromJson(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        string dataAsJson = "";

        // Android에서는 UnityWebRequest를 사용하여 파일을 불러와야 함
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
            // Android가 아닌 경우, 직접 파일 읽기 가능
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

        // JSON 데이터가 로드되었다면 파싱하여 퀘스트 리스트에 추가
        if (!string.IsNullOrEmpty(dataAsJson))
        {
            Quest[] loadedQuests = JsonHelper.FromJson<Quest>(dataAsJson);
            quests.AddRange(loadedQuests);
        }
    }
}
