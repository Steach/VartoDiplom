public class IncreasingScoreEvent
{
    public int Score { get; }

    public IncreasingScoreEvent(int score)
    {
        Score = score;
    }
}
