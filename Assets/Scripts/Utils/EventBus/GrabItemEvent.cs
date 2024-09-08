using UnityEngine;

public class GrabItemEvent
{
    public int ItemID { get; private set; } 

    public GrabItemEvent(int itemID)
    {
        ItemID = itemID;
    }
}
