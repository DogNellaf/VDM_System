using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonComponent : Element, IPointerEnterHandler, IPointerExitHandler
{
    // Hidden panel
    [SerializeField] private GameObject Panel;

    // Field for button message
    [SerializeField] private TextMeshProUGUI TextField;

    // Button message
    [SerializeField] private string Text;

    // When Scene starts
    public override void Start()
    {
        TextField.text = Text;
    }

    // When user pointing the mouse
    public void OnPointerEnter(PointerEventData eventData) => Panel.SetActive(true);

    // When mouse pointing exits button
    public void OnPointerExit(PointerEventData eventData) => Panel.SetActive(false);
}
