public class ChangePlayerExperienceEvent
{
    public int MaxLevelExp { get; }
    public int CurrentExp { get; }

    public ChangePlayerExperienceEvent(int maxLevelExp, int currentExp)
    {
        MaxLevelExp = maxLevelExp;
        CurrentExp = currentExp;
    }
}
