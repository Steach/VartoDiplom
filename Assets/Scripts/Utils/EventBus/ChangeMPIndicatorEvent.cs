public class ChangeMPIndicatorEvent
{
    public float CurrentMP { get; private set; }
    public float MaxMP { get; private set; }

    public ChangeMPIndicatorEvent(float currentMP, float maxMP)
    {
        CurrentMP = currentMP;
        MaxMP = maxMP;
    }
}
