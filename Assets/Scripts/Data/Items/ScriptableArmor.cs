using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Systems.ItemSystem
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Items/Armors/Armor", order = 10)]
    public class ScriptableArmor : ScriptableItem
    {
        [SerializeField] private ArmorType _armorType;
        [SerializeField] private GenderType _genderType;
        [SerializeField] private int _armor;
    }
}