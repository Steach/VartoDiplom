using UnityEngine;

public class ItemCharacteristics : MonoBehaviour
{
    public ItemsID ItemID { get; private set; }
    public ItemType ItemType { get; private set; }
    public ArmorType ArmorType { get; private set; }
    public GenderType Gender { get; private set; }

    public void Init(ItemsID itemsID, ItemType itemType)
    {
        ItemID = itemsID;
        ItemType = itemType;
        //ArmorType = armorType;
        //Gender = genderType;
    }
}
