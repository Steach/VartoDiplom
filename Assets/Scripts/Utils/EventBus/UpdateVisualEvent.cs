public class UpdateVisualEvent
{
    public bool IsUpdated { get; private set; }

    public UpdateVisualEvent(bool isUpdate)
    {
        IsUpdated = isUpdate;
    }
}
