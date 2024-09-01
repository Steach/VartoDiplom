using Project.Managers.Player;
using UnityEngine;

namespace Project.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private UIManager _uiManager;

        public PlayerManager PlayerManager { get { return _playerManager; } }
        public UIManager UIManager { get { return _uiManager; } }
    }
}

