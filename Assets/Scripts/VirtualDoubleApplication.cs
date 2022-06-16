using UnityEngine;

// Main application script
public class VirtualDoubleApplication : MonoBehaviour
{
    // The model of current scene
    public Element Model { get; private set; }

    // The view of current scene
    public Element View { get; private set; }

    // The controller of current scene
    public Element Controller { get; private set; }

    // The main Camera
    public Camera Camera { get => Camera.main; }

    // It is debug mode
    public bool IsDebug = false;

    // Start is called before the first frame update
    void Start()
    {
        Model = GetElement("Model");
        View = GetElement("View");
        Controller = GetElement("Controller");

        if (IsDebug)
        {
            CheckElementNull("Model", Model);
            CheckElementNull("View", View);
            CheckElementNull("Controller", Controller);
        }
    }

    private void CheckElementNull(string name, Element element)
    {
        if (element is not null)
        {
            Debug.Log($"{name} of type {element.GetType().Name} has been loaded");
        }
        else
        {
            Debug.LogError($"No {name} has been loaded");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Controller.FixedUpdate();
    }

    #region Utils

    private Element GetElement(string name)
    {
        return transform.Find(name).GetComponent<Element>();
    }

    #endregion
}
