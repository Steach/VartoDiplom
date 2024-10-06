public class MeleeAttakeEvent
{
    public int Damage { get; }
    public int Stamina { get; }

    public MeleeAttakeEvent (int damage, int stamina)
    {
        Damage = damage;
        Stamina = stamina;
    }
}