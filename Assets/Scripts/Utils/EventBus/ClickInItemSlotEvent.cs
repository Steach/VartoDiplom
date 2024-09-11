public class ClickInItemSlotEvent
{
    public int SlotId { get; private set; }

    public ClickInItemSlotEvent(int slotId)
    {
        SlotId = slotId;
    }
}
