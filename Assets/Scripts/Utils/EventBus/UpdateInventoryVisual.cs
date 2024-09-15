public class UpdateInventoryVisual
{
    public bool NeedUpdate { get; private set; }

    public UpdateInventoryVisual (bool needUpdate)
    {
        NeedUpdate = needUpdate;
    }
}
