public class HpChangedEvent
{
    public int Hp { get; }
    public int MaxHp { get; }

    public HpChangedEvent(int hp, int maxHp)
    {
        Hp = hp;
        MaxHp = maxHp;
    }
}
