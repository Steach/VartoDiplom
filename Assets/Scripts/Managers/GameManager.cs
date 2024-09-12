using Project.Managers.Player;
using Project.Systems.ItemSystem;
using UnityEngine;

namespace Project.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager _playerManager;
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private ItemDataBase _itemDataBase;

        public PlayerManager PlayerManager { get { return _playerManager; } }
        public UIManager UIManager { get { return _uiManager; } }
        public ItemDataBase ItemDataBase { get {  return _itemDataBase; } }
    }
}

