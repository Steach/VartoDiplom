using UnityEngine;
using UnityEngine.EventSystems;

public class DisableUIOnClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
