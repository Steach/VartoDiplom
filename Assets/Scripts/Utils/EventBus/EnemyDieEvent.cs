using UnityEngine;

public class EnemyDieEvent
{
    public int Exp { get; }
    public Vector3 Position { get; }

    public EnemyDieEvent (int exp, object data = null)
    {
        Exp = exp;

        if (data != null && data is Vector3)
        {
            Position = (Vector3)data;
        }
    }
}
