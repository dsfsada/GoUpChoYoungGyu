using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject[] itemPrefabs; // ������ Prefab �迭
    public int dropItemCount = 2;    // ����� ������ ����
    public float[] itemWeights = new float[4] { 0.4f, 0.3f, 0.2f, 0.1f }; // �� �������� ����ġ (0~3�� �������� Ȯ��)

    // �������� ���� �� ��
    public void DropItems(int value)
    {
        // ���� ����
        int startIndex = (value - 1) * 4; // value 1 -> 0, 2 -> 4, 3 -> 8

        // ������ �迭���� �������� ���õ� ���� ������ 2���� ������ ����
        for (int i = 0; i < dropItemCount; i++)
        {
            // ������ ���� ������ ����ġ�� ���� �������� ������ ����
            int randomIndex = GetRandomWeightedIndex(itemWeights);
            GameObject itemPrefab = itemPrefabs[startIndex + randomIndex];

            // ������ �ν��Ͻ� ����
            GameObject item = Instantiate(itemPrefab, transform.position, Quaternion.identity);

            // �����ۿ� ������ ����
            Vector2 force = new Vector2(Random.Range(0.2f, 0.8f), Random.Range(1f, 2f)).normalized * Random.Range(3f, 5f);
            item.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

    // ����ġ ��� ������ �ε��� ���� �Լ�
    private int GetRandomWeightedIndex(float[] weights)
    {
        float totalWeight = 0f;

        // ����ġ�� ���� ���
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        // ������ �� ����
        float randomValue = Random.Range(0, totalWeight);

        // ������ ���� ���� �ε��� ����
        for (int i = 0; i < weights.Length; i++)
        {
            if (randomValue < weights[i])
            {
                return i;
            }
            randomValue -= weights[i];
        }

        // ���� ��� ����ġ�� �ʰ��ϸ� ������ �ε����� ��ȯ
        return weights.Length - 1;
    }
}