[System.Serializable]
public class Quest
{
    //public int number;
    public string questName;
    public string questType;
    public int targetValue;  // Nullable<int> ����Ͽ� null ���� ���
    public int rewardPoints; // Nullable<int> ����Ͽ� null ���� ���
}
