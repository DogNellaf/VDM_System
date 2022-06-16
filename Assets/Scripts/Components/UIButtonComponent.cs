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

    // Big Panel is open
    [SerializeField] private bool isBigPanelOpen = false;

    // When Scene starts
    public override void Start()
    {
        TextField.text = Text;
    }

    // When user pointing the mouse
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isBigPanelOpen)
        {
            Panel.SetActive(true);
        }
    }
    // When mouse pointing exits button
    public void OnPointerExit(PointerEventData eventData) => Panel.SetActive(false);


    // Change active status to the another position
    public void ChangePanelActive(GameObject panel)
    {
        OnPointerExit(null);
        isBigPanelOpen = !panel.activeSelf;
        panel.SetActive(isBigPanelOpen);
    }
}
