using Project.Data;
using System.Xml.Serialization;

namespace Project.Systems.LevelingSystem
{
    public class PlayerLevelingSystem
    {
        private int _playerLevel;
        private int _expToNextLevel;
        private int _currentExp;
        private int _str;
        private int _int;
        private int _agl;
        private int _freeStatsPoints;
        private int _freeSkillPoints;

        private bool _isInittedOnce = true;
        private int _countOfInit = 0;

        public int STR { get { return _str; } }
        public int INT { get { return _int; } }
        public int AGL { get { return _agl; } }
        public int FreeStatsPoints { get { return _freeStatsPoints; } }
        public int FreeSkillPoints {  get { return _freeSkillPoints; } }

        public void Init()
        {
            if (_isInittedOnce && _countOfInit == 0)
            {
                _str = GameData.StartedStr;
                _int = GameData.StartedInt;
                _agl = GameData.StartedAgl;
                _expToNextLevel = GameData.LevelOneExperience;
                _freeSkillPoints = 0;
                _freeStatsPoints = 0;
                _playerLevel = 1;
                _currentExp = 0;
                _countOfInit++;
            }
        }

        public void OnEnableEvents()
        {
            EventBus.Subscribe<EnemyDieEvent>(GetExpirience);
            EventBus.Subscribe<AddStatsEvent>(AddNewStats);
        }

        public void RunOnStart()
        {
            EventBus.Publish(new AddStatsEvent(_str, _int, _agl, _freeSkillPoints));
            EventBus.Publish(new LevelUpEvent(_currentExp, _expToNextLevel, _playerLevel));
        }

        public void OnDisableEvents()
        {
            EventBus.Unsubscribe<EnemyDieEvent>(GetExpirience);
            EventBus.Unsubscribe<AddStatsEvent>(AddNewStats);
        }

        public void RunInUpdate()
        {
            CheckLevelUp();
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

        private void AddNewStats(AddStatsEvent addStatsEvent)
        {
            _str = addStatsEvent.NewSTR;
            _int = addStatsEvent.NewINT;
            _agl = addStatsEvent.NewAGL;
            _freeStatsPoints = addStatsEvent.NewFreeStatPoints;
        }
    }
}