using Project.Managers;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Controllers.UI
{
    public class CharacteristicContainerController : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;

        [Space]
        [Header("STR")]
        [SerializeField] private TextMeshProUGUI _strText;
        [SerializeField] private Button _addSTRPointsButton;

        private int _currentSTRPoints;

        [Space]
        [Header("INT")]
        [SerializeField] private TextMeshProUGUI _intText;
        [SerializeField] private Button _addINTPointsButton;
        private int _currentINTPoints;

        [Space]
        [Header("AGL")]
        [SerializeField] private TextMeshProUGUI _aglText;
        [SerializeField] private Button _addAGLPointsButton;
        private int _currentAGLPoints;

        [Space]
        [Space]
        [SerializeField] private TextMeshProUGUI _freePointsText;
        [SerializeField] private Button _saveStatsButton;
        private int _freeStatsPoints;

        private GameManager _gameManager;

        public void CheckStats()
        {
            _gameManager = _uiManager.GameManager;
            _currentSTRPoints = _gameManager.PlayerManager.PlayerLevelingSystem.STR;
            _currentINTPoints = _gameManager.PlayerManager.PlayerLevelingSystem.INT;
            _currentAGLPoints = _gameManager.PlayerManager.PlayerLevelingSystem.AGL;
            _freeStatsPoints = _gameManager.PlayerManager.PlayerLevelingSystem.FreeStatsPoints;

            _strText.text = _currentSTRPoints.ToString();
            _intText.text = _currentINTPoints.ToString();
            _aglText.text = _currentAGLPoints.ToString();
            _freePointsText.text = _freeStatsPoints.ToString();

            if (_freeStatsPoints == 0)
            {
                _addSTRPointsButton.gameObject.SetActive(false);
                _addINTPointsButton.gameObject.SetActive(false);
                _addAGLPointsButton.gameObject.SetActive(false);
                RemoveButtonsLister();
            }
            else
            {
                _addSTRPointsButton.gameObject.SetActive(true);
                _addINTPointsButton.gameObject.SetActive(true);
                _addAGLPointsButton.gameObject.SetActive(true);
                AddButtonsLister();
            }

            
            
        }

        private void AddButtonsLister()
        {
            _addSTRPointsButton.onClick.AddListener(AddSTRPoint);
            _addAGLPointsButton.onClick.AddListener(AddAGLPoint);
            _addINTPointsButton.onClick.AddListener(AddINTPoint);
            _saveStatsButton.onClick.AddListener(SaveStats);
        }

        private void RemoveButtonsLister() 
        {
            _addSTRPointsButton.onClick.RemoveListener(AddSTRPoint);
            _addAGLPointsButton.onClick.RemoveListener(AddAGLPoint);
            _addINTPointsButton.onClick.RemoveListener(AddINTPoint);
            _saveStatsButton.onClick.RemoveListener(SaveStats);
        }

        private void AddSTRPoint()
        {
            if (_freeStatsPoints > 0)
            {
                _freeStatsPoints--;
                _currentSTRPoints++;
                _freePointsText.text = _freeStatsPoints.ToString();
                _strText.text = _currentSTRPoints.ToString();
            }
        }

        private void AddINTPoint()
        {
            if (_freeStatsPoints > 0)
            {
                _freeStatsPoints--;
                _currentINTPoints++;
                _freePointsText.text = _freeStatsPoints.ToString();
                _intText.text = _currentINTPoints.ToString();
            }
        }

        private void AddAGLPoint() 
        {
            if (_freeStatsPoints > 0)
            {
                _freeStatsPoints--;
                _currentAGLPoints++;
                _freePointsText.text = _freeStatsPoints.ToString();
                _aglText.text = _currentAGLPoints.ToString();
            }
        }

        private void SaveStats()
        {
            EventBus.Publish(new AddStatsEvent(_currentSTRPoints, _currentINTPoints, _currentAGLPoints, _freeStatsPoints));
            CheckStats();
        }
    }
}