public class GetDamagePlayerEvent
{
    public float CurrentHP { get; private set; }
    public float MaxHP { get; private set; }

    public GetDamagePlayerEvent (float currentHP, float maxHP)
    {
        CurrentHP = currentHP;
        MaxHP = maxHP;
    }
}
