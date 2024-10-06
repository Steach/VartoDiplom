public class ChangeStaminaImdicatorEvent
{ 
    public float CurrentST { get; private set; }
    public float MaxST { get; private set; }

    public ChangeStaminaImdicatorEvent(float currentST, float maxST)
    {
        CurrentST = currentST;
        MaxST = maxST;
    }
}
