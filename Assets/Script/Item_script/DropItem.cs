using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject[] itemPrefabs; // 아이템 Prefab 배열
    public int dropItemCount = 2;    // 드롭할 아이템 개수
    public float[] itemWeights = new float[4] { 0.4f, 0.3f, 0.2f, 0.1f }; // 각 아이템의 가중치 (0~3번 아이템의 확률)

    // 보스한테 적용 될 듯
    public void DropItems(int value)
    {
        // 범위 설정
        int startIndex = (value - 1) * 4; // value 1 -> 0, 2 -> 4, 3 -> 8

        // 아이템 배열에서 무작위로 선택된 범위 내에서 2개의 아이템 선택
        for (int i = 0; i < dropItemCount; i++)
        {
            // 지정된 범위 내에서 가중치에 따라 무작위로 아이템 선택
            int randomIndex = GetRandomWeightedIndex(itemWeights);
            GameObject itemPrefab = itemPrefabs[startIndex + randomIndex];

            // 아이템 인스턴스 생성
            GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

            // 아이템에 움직임 적용
            Vector2 force = new Vector2(Random.Range(0.2f, 0.8f), Random.Range(1f, 2f)).normalized * Random.Range(3f, 5f);
            item.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

    // 가중치 기반 무작위 인덱스 선택 함수
    private int GetRandomWeightedIndex(float[] weights)
    {
        float totalWeight = 0f;

        // 가중치의 총합 계산
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        // 무작위 값 생성
        float randomValue = Random.Range(0, totalWeight);

        // 무작위 값에 따라 인덱스 선택
        for (int i = 0; i < weights.Length; i++)
        {
            if (randomValue < weights[i])
            {
                return i;
            }
            randomValue -= weights[i];
        }

        // 만약 모든 가중치를 초과하면 마지막 인덱스를 반환
        return weights.Length - 1;
    }
}