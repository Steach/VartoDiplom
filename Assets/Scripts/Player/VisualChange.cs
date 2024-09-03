using UnityEngine;

public class VisualChange : MonoBehaviour
{
    [Header("Armor_01_A")]
    [SerializeField] private GameObject Body_C;
    [SerializeField] private GameObject Boot_C;
    [SerializeField] private GameObject Cape_C;
    [SerializeField] private GameObject Gauntlets_C;
    [SerializeField] private GameObject Helmet_C;
    [SerializeField] private GameObject Legs_C;
    [SerializeField] private bool Body_C_Enable;
    [SerializeField] private bool Boot_C_Enable;
    [SerializeField] private bool Cape_C_Enable;
    [SerializeField] private bool Gauntlets_C_Enable;
    [SerializeField] private bool Helmet_C_Enable;
    [SerializeField] private bool Legs_C_Enable;
    [Space]
    [Header("Armor_05_C")]
    [SerializeField] private GameObject Body_A;
    [SerializeField] private GameObject Boot_A;
    [SerializeField] private GameObject Cape_A;
    [SerializeField] private GameObject Gauntlets_A;
    [SerializeField] private GameObject Helmet_A;
    [SerializeField] private GameObject Legs_A;
    [SerializeField] private bool Body_A_Enable;
    [SerializeField] private bool Boot_A_Enable;
    [SerializeField] private bool Cape_A_Enable;
    [SerializeField] private bool Gauntlets_A_Enable;
    [SerializeField] private bool Helmet_A_Enable;
    [SerializeField] private bool Legs_A_Enable;
    [Space]
    [Header("Cloth")]
    [SerializeField] private GameObject Body_Cloth;
    [SerializeField] private GameObject Boot_Cloth;
    [SerializeField] private GameObject Gauntlets_Cloth;
    [SerializeField] private GameObject Legs_Cloth;
    [SerializeField] private bool Body_Cloth_Enable;
    [SerializeField] private bool Boot_Cloth_Enable;
    [SerializeField] private bool Gauntlets_Cloth_Enable;
    [SerializeField] private bool Legs_Cloth_Enable;
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

    private void Update()
    {
        Body_C.SetActive(Body_C_Enable);
        Boot_C.SetActive(Boot_C_Enable);
        Cape_C.SetActive(Cape_C_Enable);
        Gauntlets_C.SetActive (Gauntlets_C_Enable);
        Helmet_C.SetActive(Helmet_C_Enable);
        Legs_C.SetActive(Legs_C_Enable);

        Body_A.SetActive(Body_A_Enable);
        Boot_A.SetActive(Boot_A_Enable);
        Cape_A.SetActive(Cape_A_Enable);
        Gauntlets_A.SetActive(Gauntlets_A_Enable);
        Helmet_A.SetActive(Helmet_A_Enable);
        Legs_A.SetActive(Legs_A_Enable);

        Body_Cloth.SetActive(Body_Cloth_Enable);
        Boot_Cloth.SetActive(Boot_Cloth_Enable);
        Gauntlets_Cloth.SetActive(Gauntlets_Cloth_Enable);
        Legs_Cloth.SetActive(Gauntlets_Cloth_Enable);

        Body_Naked.SetActive(Body_Naked_Enable);
        Boot_Naked.SetActive(Boot_Naked_Enable);
        Gauntlets_Naked.SetActive(Gauntlets_Naked_Enable);
        Legs_Naked.SetActive(Legs_Naked_Enable);

        Head_1.SetActive(Head_1_Enable);
        Head_2.SetActive(Head_2_Enable);

        Hair_1.SetActive(Hair_1_Enable);
        Hair_2.SetActive(Hair_2_Enable);

        Beard_1.SetActive(Beard_1_Enable);
        Beard_2.SetActive(Beard_2_Enable);
    }
}
