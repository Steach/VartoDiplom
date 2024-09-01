public class ChangePlayerExperienceEvent
{
    public int MaxLevelExp { get; }
    public int CurrentExp { get; }

    public ChangePlayerExperienceEvent(int expToNextLevel, int currentExp)
    {
        MaxLevelExp = expToNextLevel;
        CurrentExp = currentExp;
    }
}
