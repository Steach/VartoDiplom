public class LevelUpEvent
{
    public int NextLevelExp { get; }
    public int NewLevel { get; }
    public int CurrentExp { get; }

    public LevelUpEvent (int currentExp, int nextLevelExp, int newLevel)
    {
        NextLevelExp = nextLevelExp;
        NewLevel = newLevel;
        CurrentExp = currentExp;
    }
}
