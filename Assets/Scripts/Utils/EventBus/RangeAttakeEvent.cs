using UnityEngine;

public class RangeAttakeEvent
{
    public int Damage { get; }
    public int Stamina { get; }
    public Vector3 MouseHitPotision { get; }

    public RangeAttakeEvent (int damage, Vector3 mouseHitPosition, int stamina)
    {
        Damage = damage;
        MouseHitPotision = mouseHitPosition;
        Stamina = stamina;
    }
}