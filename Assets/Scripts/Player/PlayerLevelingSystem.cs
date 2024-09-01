using Project.Data;
using UnityEngine;

namespace Project.Systems.LevelingSystem
{
    public class PlayerLevelingSystem : MonoBehaviour
    {
        private int _playerLevel;
        [SerializeField] private int _expToNextLevel;
        [SerializeField] private int _currentExp;
        [SerializeField] private int _str;
        [SerializeField] private int _int;
        [SerializeField] private int _agl;
        [SerializeField] private int _freeStatsPoints;
        [SerializeField] private int _freeSkillPoints;

        private void Awake()
        {
            Init();
            EventBus.Subscribe<EnemyDieEvent>(GetExpirience);
        }

        private void Start()
        {
            EventBus.Publish(new LevelUpEvent(_currentExp, _expToNextLevel, _playerLevel));
        }

        private void Update()
        {
            CheckLevelUp();
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe<EnemyDieEvent>(GetExpirience);
        }

        private void Init()
        {
            _str = GameData.StartedStr;
            _int = GameData.StartedInt;
            _agl = GameData.StartedAgl;
            _expToNextLevel = GameData.LevelOneExperience;
            _freeSkillPoints = 0;
            _freeStatsPoints = 0;
            _playerLevel = 1;
            _currentExp = 0;
        }

        private void CheckLevelUp()
        {
           switch (_currentExp)
           {
               case int n when n < _expToNextLevel:
                    break;
               case int n when n >= _expToNextLevel:
                    LevelUp(_currentExp - _expToNextLevel);
                    break;
                default:
                    break;
           }
        }

        private void LevelUp(int remainderExp)
        {
            _currentExp = remainderExp;
            _playerLevel += 1;
            _freeSkillPoints += 1;
            _freeStatsPoints += 2;
            _str += 1;
            _int += 1;
            _agl += 1;

            switch (_playerLevel)
            {
                case 1:
                    _expToNextLevel = GameData.LevelOneExperience;
                    break;
                case 2:
                    _expToNextLevel = GameData.LevelTwoExperience;
                    break;
                case 3:
                    _expToNextLevel = GameData.LevelThreeExperience;
                    break;
                case 4:
                    _expToNextLevel = GameData.LevelFourExperience;
                    break;
                case 5:
                    _expToNextLevel = GameData.LevelFiveExperience;
                    break;
                case 6:
                    _expToNextLevel = GameData.LevelSixExperience;
                    break;
                case 7:
                    _expToNextLevel = GameData.LevelSevenExperience;
                    break;
                case 8:
                    _expToNextLevel = GameData.LevelEightExperience;
                    break;
                case 9:
                    _expToNextLevel = GameData.LevelNineExperience;
                    break;
                case 10:
                    _expToNextLevel = GameData.LevelTenExperience;
                    break;
                default:
                    break;
            }

            EventBus.Publish(new LevelUpEvent(_currentExp, _expToNextLevel, _playerLevel));
        }

        private void GetExpirience(EnemyDieEvent enemyDieEvent)
        {
            _currentExp += enemyDieEvent.Exp;
            EventBus.Publish(new LevelUpEvent(_currentExp, _expToNextLevel, _playerLevel));
        }
    }
}