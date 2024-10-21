[System.Serializable]
public class Quest
{
    //public int number;
    public string questName;
    public string questType;
    public int targetValue;  // Nullable<int> 사용하여 null 값을 허용
    public int rewardPoints; // Nullable<int> 사용하여 null 값을 허용
}
