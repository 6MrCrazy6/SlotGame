[System.Serializable]
public class PixelCombinations 
{
    public enum SlotValue
    {
        Bell,
        Seven,
        Cherry,
        Bar
    }

    public SlotValue FirstValue;
    public SlotValue SecondValue;
    public SlotValue ThirdValue;
    public int prize;

}
