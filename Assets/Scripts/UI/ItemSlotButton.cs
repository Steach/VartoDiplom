using Project.Systems.ItemSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotButton : MonoBehaviour
{
    [SerializeField] private Button _itemButton;
    [SerializeField] private GameObject _infoPopup;

    private bool _isInitialize = false;
    private int _slotID = 0;
    private bool _isEmpty = true;

    public void Init(GameObject infopopup, int id)
    {
        if (!_isInitialize)
        {
            _isInitialize = true;
            _infoPopup = infopopup;
            _slotID = id;
        }
    }

    private void OnEnable()
    {
        _itemButton.onClick.AddListener(ButClick);
    }

    private void OnDisable()
    {
        _itemButton?.onClick.RemoveListener(ButClick);
    }

    public void SetIsEmpty(bool isEmpty)
    {
        _isEmpty = isEmpty;
    }

    private void ButClick()
    {
        if (_infoPopup == null)
        {
            Debug.Log("_infoPopup not init");
            return;
        }

        if(!_isEmpty)
        {
            var popupRect = _infoPopup.gameObject.GetComponent<RectTransform>();
            var thisRect = this.gameObject.GetComponent<RectTransform>();

            popupRect.GetComponent<PopupButtonController>().Init(_slotID);

            _infoPopup.SetActive(!_infoPopup.activeInHierarchy);
            popupRect.position = thisRect.position;

            EventBus.Publish<ClickInItemSlotEvent>(new ClickInItemSlotEvent(_slotID));
        }
    }
}