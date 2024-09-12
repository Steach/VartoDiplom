using Project.Managers.Player;
using System.Collections.Generic;
using UnityEngine;

public class VisualChange : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;

    [Space]
    [Header("Naked")]
    [SerializeField] private GameObject Body_Naked;
    [SerializeField] private GameObject Boot_Naked;
    [SerializeField] private GameObject Gauntlets_Naked;
    [SerializeField] private GameObject Legs_Naked;
    [SerializeField] private bool Body_Naked_Enable;
    [SerializeField] private bool Boot_Naked_Enable;
    [SerializeField] private bool Gauntlets_Naked_Enable;
    [SerializeField] private bool Legs_Naked_Enable;

    [Space]
    [Header("Helmet")]
    [SerializeField] private GameObject Heavy_Helmet;
    [SerializeField] private bool Heavy_Helmet_Enable;
    [SerializeField] private GameObject Light_Helmet;
    [SerializeField] private bool Light_Helmet_Enable;
    [Space]
    [Header("Body")]
    [SerializeField] private GameObject Heavy_Body;
    [SerializeField] private bool Heavy_Body_Enable;
    [SerializeField] private GameObject Light_Body;
    [SerializeField] private bool Light_Body_Enable;
    [SerializeField] private GameObject Rags_Body_Cloth;
    [SerializeField] private bool Rags_Body_Cloth_Enable;
    [Space]
    [Header("Legs")]
    [SerializeField] private GameObject Heavy_Legs;
    [SerializeField] private bool Heavy_Legs_Enable;
    [SerializeField] private GameObject Light_Legs;
    [SerializeField] private bool Light_Legs_Enable;
    [SerializeField] private GameObject Rags_Legs_Cloth;
    [SerializeField] private bool Rags_Legs_Cloth_Enable;
    [Space]
    [Header("Guantlets")]
    [SerializeField] private GameObject Heavy_Gauntlets;
    [SerializeField] private bool Heavy_Gauntlets_Enable;
    [SerializeField] private GameObject Light_Gauntlets;
    [SerializeField] private bool Light_Gauntlets_Enable;
    [SerializeField] private GameObject Rags_Gauntlets_Cloth;
    [SerializeField] private bool Rags_Gauntlets_Cloth_Enable;
    [Space]
    [Header("Boots")]
    [SerializeField] private GameObject Heavy_Boot;
    [SerializeField] private bool Heavy_Boot_Enable;
    [SerializeField] private GameObject Light_Boot;
    [SerializeField] private bool Light_Boot_Enable;
    [SerializeField] private GameObject Rags_Boot_Cloth;
    [SerializeField] private bool Rags_Boot_Cloth_Enable;
    [Space]
    [Header("Cape")]
    [SerializeField] private GameObject Heavy_Cape;
    [SerializeField] private bool Heavy_Cape_Enable;
    [SerializeField] private GameObject Light_Cape;
    [SerializeField] private bool Light_Cape_Enable;

    [Space]
    [Header("Head")]
    [SerializeField] private GameObject Head_1;
    [SerializeField] private GameObject Head_2;
    [SerializeField] private bool Head_1_Enable;
    [SerializeField] private bool Head_2_Enable;
    [Space]
    [Header("Hair")]
    [SerializeField] private GameObject Hair_1;
    [SerializeField] private GameObject Hair_2;
    [SerializeField] private bool Hair_1_Enable;
    [SerializeField] private bool Hair_2_Enable;
    [Space]
    [Header("Beard")]
    [SerializeField] private GameObject Beard_1;
    [SerializeField] private GameObject Beard_2;
    [SerializeField] private bool Beard_1_Enable;
    [SerializeField] private bool Beard_2_Enable;

    [Space]
    [Header("Available Armors for this Character")]
    [SerializeField] private Dictionary<ItemsID, GameObject> _items = new Dictionary<ItemsID, GameObject>();
    private void Awake()
    {
        AddItemsToEnableItems();
    }

    private void OnEnable()
    {
        EventBus.Subscribe<UpdateVisualEvent>(CheckEquipedArmor);
    }

    private void Start()
    {
        CheckEquipedArmor(new UpdateVisualEvent(true));
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<UpdateVisualEvent>(CheckEquipedArmor);
    }

    private void CheckEquipedArmor(UpdateVisualEvent updateVisualEvent)
    {
        foreach (var item in _items.Values) 
        {
            item.SetActive(false);
        }

        foreach (var equip in _playerManager.PlayerInventory.EquipedArmor)
        {
            if (_items.TryGetValue((ItemsID)equip.Value, out GameObject equipItem))
            {
                equipItem.SetActive(true);
            }

            if (equip.Key == ArmorType.Body && equip.Value != 0)
                Body_Naked.SetActive(false);
            else if (equip.Key == ArmorType.Body && equip.Value == 0)
                Body_Naked.SetActive(true);

            if (equip.Key == ArmorType.Legs && equip.Value != 0)
                Legs_Naked.SetActive(false);
            else if (equip.Key == ArmorType.Legs && equip.Value == 0)
                Legs_Naked.SetActive(true);

            if (equip.Key == ArmorType.Boot && equip.Value != 0)
                Boot_Naked.SetActive(false);
            else if (equip.Key == ArmorType.Boot && equip.Value == 0)
                Boot_Naked.SetActive(true);

            if (equip.Key == ArmorType.Gauntlets && equip.Value != 0)
                Gauntlets_Naked.SetActive(false);
            else if (equip.Key == ArmorType.Gauntlets && equip.Value == 0)
                Gauntlets_Naked.SetActive(true);
        }
    }

    private void AddItemsToEnableItems()
    {
        _items.Add(ItemsID.HeavyBodyMale, Heavy_Body);
        _items.Add(ItemsID.HeavyHelmetMale, Heavy_Helmet);
        _items.Add(ItemsID.HeavyBootsMale, Heavy_Boot);
        _items.Add(ItemsID.HeavyCapeMale, Heavy_Cape);
        _items.Add(ItemsID.HeavyGauntletsMale, Heavy_Gauntlets);
        _items.Add(ItemsID.HeavyLegsMale, Heavy_Legs);

        _items.Add(ItemsID.LightBodyMale, Light_Body);
        _items.Add(ItemsID.LightBootsMale, Light_Boot);
        _items.Add(ItemsID.LightCapeMale, Light_Cape);
        _items.Add(ItemsID.LightGauntletsMale, Light_Gauntlets);
        _items.Add(ItemsID.LightHelmetMale, Light_Helmet);
        _items.Add(ItemsID.LightLegsMale, Light_Legs);

        _items.Add(ItemsID.RagsBodyMale, Rags_Body_Cloth);
        _items.Add(ItemsID.RagsBootsMale, Rags_Boot_Cloth);
        _items.Add(ItemsID.RagsGauntletsMale, Rags_Gauntlets_Cloth);
        _items.Add(ItemsID.RagsLegsMale, Rags_Legs_Cloth);
    }
}
