public class AddStatsEvent
{
    public int NewSTR { get; }
    public int NewINT { get; }
    public int NewAGL { get; }
    public int NewFreeStatPoints { get; }

    public AddStatsEvent(int newStr, int newInt, int newAgl, int newFreePoints)
    {
        NewSTR = newStr;
        NewINT = newInt;
        NewAGL = newAgl;
        NewFreeStatPoints = newFreePoints;
    }
}
