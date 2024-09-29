using UnityEngine;

public class RangeAttakeEvent
{
    public int Damage { get; }
    public Vector3 MouseHitPotision { get; }

    public RangeAttakeEvent (int damage, Vector3 mouseHitPosition)
    {
        Damage = damage;
        MouseHitPotision = mouseHitPosition;
    }
}