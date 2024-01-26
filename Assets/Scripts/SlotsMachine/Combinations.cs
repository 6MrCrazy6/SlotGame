[System.Serializable]
public class Combinations 
{
    public enum SlotValue
    {
        Crown,
        Diamond,
        Seven,
        Cherry,
        Bar
    }

    public SlotValue FirstValue;
    public SlotValue SecondValue;
    public SlotValue ThirdValue;
    public int prize;
}
