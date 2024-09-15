using Project.Controllers.Player;
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

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Slider _playerExp;
    [SerializeField] private TextMeshProUGUI _playerLevelDebug;
    [SerializeField] private CharacteristicContainerController _characterContainerController;
    [SerializeField] private InventoryContainerController _inventoryContainerController;
    [SerializeField] private GameObject _characteristicsContainer;
    [SerializeField] private GameObject _inventoryContainer;
    private ControlsSystem _inputActions;
    private int _score = 0;

    public GameManager GameManager { get { return _gameManager; } }
    public ControlsSystem InputActions { get { return _inputActions; } }
    public GameObject InventoryContainer { get { return _inventoryContainer; } }

    private void OnEnable()
    {
        _inputActions = _gameManager.PlayerManager.PlayerController.ControlSystem;
        _inputActions.UIController.Enable();
        _inputActions.UIController.Characteristics.performed += EnableDisableCharacteristicsContainer;
        _inputActions.UIController.Inventory.performed += EnableDisableInventoryContainer;

        EventBus.Subscribe<IncreasingScoreEvent>(IncreasScore);
        EventBus.Subscribe<ChangePlayerExperienceEvent>(ChangePlayerExpSlider);
        EventBus.Subscribe<LevelUpEvent>(PlayerLevelUp);

        _characteristicsContainer.SetActive(false);
        _inventoryContainer.SetActive(false);
    }

    private void OnDisable()
    {
        _inputActions.UIController.Disable();
        _inputActions.UIController.Characteristics.performed -= EnableDisableCharacteristicsContainer;
        _inputActions.UIController.Inventory.performed -= EnableDisableInventoryContainer;

        EventBus.Unsubscribe<IncreasingScoreEvent>(IncreasScore);
        EventBus.Unsubscribe<ChangePlayerExperienceEvent>(ChangePlayerExpSlider);
        EventBus.Unsubscribe<LevelUpEvent>(PlayerLevelUp);
    }

    private void IncreasScore(IncreasingScoreEvent increasingScoreEvent)
    {
        _score += increasingScoreEvent.Score;
        _scoreText.text = _score.ToString();
    }

    private void ChangePlayerExpSlider(ChangePlayerExperienceEvent changePlayerExperienceEvent)
    {
        _playerExp.maxValue = changePlayerExperienceEvent.MaxLevelExp;
        _playerExp.value = changePlayerExperienceEvent.CurrentExp;
    }

    private void PlayerLevelUp(LevelUpEvent levelUpEvent)
    {
        _playerExp.maxValue = levelUpEvent.NextLevelExp;
        _playerExp.value = levelUpEvent.CurrentExp;
        _playerLevelDebug.text = levelUpEvent.NewLevel.ToString();
    }

    private void EnableDisableCharacteristicsContainer(InputAction.CallbackContext context)
    {
        _characteristicsContainer.gameObject.SetActive(!_characteristicsContainer.gameObject.activeInHierarchy);
        if (_characteristicsContainer.activeInHierarchy)
        {
            _inputActions.PlayerController.Disable();
            _characterContainerController.CheckStats();
        }
        else if (!_characteristicsContainer.activeInHierarchy)
        {
            _inputActions.PlayerController.Enable();
        }
    }

    private void EnableDisableInventoryContainer(InputAction.CallbackContext context)
    {
        _inventoryContainer.gameObject.SetActive(!_inventoryContainer.gameObject.activeInHierarchy);
        if (_inventoryContainer.gameObject.activeInHierarchy)
        {
            _inputActions.PlayerController.Disable();
            EventBus.Publish<UpdateInventoryVisual>(new UpdateInventoryVisual(true));
        }
        else if (!_inventoryContainer.gameObject.activeInHierarchy)
        {
            _inputActions.PlayerController.Enable();
        }
    }
}
