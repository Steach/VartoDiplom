public class EquipItemEvent
{
    public int SlotId { get; private set; }

    public EquipItemEvent(int slotId)
    {
        SlotId = slotId;
    }
}
