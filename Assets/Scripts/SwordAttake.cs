using UnityEngine;

public class SwordAttake : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("HIT");
        }
    }
}
