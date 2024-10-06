using Project.Controllers.UI;
using Project.Managers;
using Project.Systems.ControlsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [Space]
    [Header("Debug")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [Space]
    [Header("PlayerGUI")]
    [SerializeField] private TextMeshProUGUI _playerLevelDebug;
    [SerializeField] private Image _playerNewExp;
    [Space]
    [Header("Containers")]
    [SerializeField] private CharacteristicContainerController _characterContainerController;
    [SerializeField] private InventoryContainerController _inventoryContainerController;
    [SerializeField] private PlayerIndicatorsController _playerIndicatorsController;
    [SerializeField] private GameObject _characteristicsContainer;
    [SerializeField] private GameObject _inventoryContainer;
    [SerializeField] private GameObject _playerIndicatorsContainer;

    [SerializeField] private GameObject[] _containers;

    private const int playerIndicatorContainerIndex = 0;
    private const int characteristicsContainerIndex = 1;
    private const int inventoryContainerIndex = 2;

    private ControlsSystem _inputActions;

    public GameManager GameManager { get { return _gameManager; } }
    public ControlsSystem InputActions { get { return _inputActions; } }
    public GameObject InventoryContainer { get { return _inventoryContainer; } }

    private void OnEnable()
    {
        _inputActions = _gameManager.PlayerManager.PlayerController.ControlSystem;
        _inputActions.UIController.Enable();
        _inputActions.UIController.Characteristics.performed += EnableDisableCharacteristicsContainer;
        _inputActions.UIController.Inventory.performed += EnableDisableInventoryContainer;

        _characteristicsContainer.SetActive(false);
        _inventoryContainer.SetActive(false);
    }

    private void OnDisable()
    {
        _inputActions.UIController.Disable();
        _inputActions.UIController.Characteristics.performed -= EnableDisableCharacteristicsContainer;
        _inputActions.UIController.Inventory.performed -= EnableDisableInventoryContainer;
    }

    private void EnableDisableCharacteristicsContainer(InputAction.CallbackContext context)
    {
        if (!_characteristicsContainer.activeInHierarchy)
        {
            _inputActions.PlayerController.Disable();
            _characterContainerController.CheckStats();
            EnableActualyInterface(characteristicsContainerIndex);
        }
        else if (_characteristicsContainer.activeInHierarchy)
        {
            _inputActions.PlayerController.Enable();
            EnableActualyInterface(playerIndicatorContainerIndex);
        }
    }

    private void EnableDisableInventoryContainer(InputAction.CallbackContext context)
    {
        if (!_inventoryContainer.gameObject.activeInHierarchy)
        {
            EnableActualyInterface(inventoryContainerIndex);
            _inputActions.PlayerController.Disable();
            EventBus.Publish<UpdateInventoryVisual>(new UpdateInventoryVisual(true));
        }
        else if (_inventoryContainer.gameObject.activeInHierarchy)
        {
            _inputActions.PlayerController.Enable();
            EnableActualyInterface(playerIndicatorContainerIndex);
        }
    }

    private void EnableActualyInterface(int index)
    {
        for (int i = 0; i < _containers.Length; i++)
        {
            if(i == index)
                _containers[i].gameObject.SetActive(true);
            else
                _containers[i].gameObject.SetActive(false);
        }
    }
}
