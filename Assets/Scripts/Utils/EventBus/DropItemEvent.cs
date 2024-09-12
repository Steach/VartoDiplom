public class DropItemEvent
{
    public int SlotId { get; private set; }

    public DropItemEvent(int slotId)
    {
        SlotId = slotId;
    }
}
