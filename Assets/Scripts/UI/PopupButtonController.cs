using UnityEngine;
using UnityEngine.UI;

public class PopupButtonController : MonoBehaviour
{
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _dropButton;

    private int _currentSlotId;

    public void Init(int slotId) =>
        _currentSlotId = slotId;

    private void OnEnable()
    {
        _equipButton.onClick.AddListener(ButtonEquipEvent);
        //_dropButton.onClick.AddListener(DropButtonEvent);
    }

    private void OnDisable()
    {
        _equipButton.onClick.RemoveAllListeners();
        //_dropButton.onClick.RemoveAllListeners();
    }

    private void ButtonEquipEvent()
    {
        EventBus.Publish<EquipItemEvent>(new EquipItemEvent(_currentSlotId));
        EventBus.Publish<UpdateInventoryVisual>(new UpdateInventoryVisual(true));
    }
        

    private void DropButtonEvent()
    {
        EventBus.Publish<DropItemEvent>(new DropItemEvent(_currentSlotId));
        EventBus.Publish<UpdateInventoryVisual>(new UpdateInventoryVisual(true));
    }
}
