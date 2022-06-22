using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

#nullable enable


public class ItemComponent : Element
{
    public ItemType Type;
    public WorkerComponent Worker;
    public List<ItemComponent> Outputs = new();
    private float value = 0;
    public float Value { 
        get 
        { 
            return value;
        }
        set
        {
            this.value = value;
            if (ValueField is not null)
            {
                ValueField.text = $"{this.value}";
            }
        } 
    }
    public float Priority;

    [SerializeField] protected GameObject InputConnection;
    [SerializeField] protected GameObject OutputConnection;
    [SerializeField] protected GameObject UI;
    [SerializeField] protected TMP_InputField NameField;
    [SerializeField] protected Slider PrioritySlider;
    [SerializeField] protected TextMeshProUGUI PriorityLabel;
    [SerializeField] protected TextMeshPro ValueField;
    [SerializeField] protected string NameUnsaved;
    [SerializeField] protected float PriorityUnsaved;

    protected WorkspaceModel Model => TwinApplication.GetModel<WorkspaceModel>();
    protected List<ItemComponent> Inputs => Model.DigitalTwin.transform.GetComponentsInChildren<ItemComponent>().Where(x => x.Outputs.Contains(this)).ToList();

    protected string? InputName
    {
        get
        {
            try
            {
                return InputConnection.name;
            }
            catch
            {
                return null;
            }
        }
    }
    protected string? OutputName
    {
        get
        {
            try
            {
                return OutputConnection.name;
            }
            catch
            {
                return null;
            }
        }
    }

    #region Unity

    // Start function
    public override void Start()
    {
        PriorityUnsaved = 50;
        if (NameField != null)
        {
            NameField.text = gameObject.name;
            NameUnsaved = gameObject.name;
        }
    }

    // When User click
    public override void OnMouseDown()
    {
        // Create the ray
        Ray ray = TwinApplication.Camera.ScreenPointToRay(Input.mousePosition);

        // Send the raycast to the click point
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Get the gameobject name of the hit
            string hitObjectName = hit.collider.gameObject.name;

            // If this is current gameobject
            if (hitObjectName == name)
            {
                // Activate layout
                UI.SetActive(true);
            }

            // If this is the input connection
            else if (hitObjectName == InputName)
            {
                // Get output connection
                var inputObject = Model.SelectedOutputConnection;

                // Get the item component of input connection
                var connection = inputObject.GetComponent<ItemComponent>();

                // If connection exists
                if (connection is not null)
                {
                    // And connection is not current object
                    if (connection.gameObject != gameObject)
                    {
                        // And same connection doesn't exists
                        if (!connection.Outputs.Contains(this))
                        {
                            // Add connection
                            connection.AddOutput(this);

                            // Set selected output connection to null
                            Model.SelectedOutputConnection = null;
                        }
                        else
                        {
                            Debug.LogError("Такое соединение уже есть");
                        }
                    }
                    else
                    {
                        Debug.LogError("Создание цикличных связей запрещено");
                    }
                }
                else
                {
                    Debug.Log("Сперва выберите входное соединение");
                }
            }

            // If this is the output connection
            else if (hitObjectName == OutputName)
            {
                // Set it to the model
                TwinApplication.GetModel<WorkspaceModel>().SelectedOutputConnection = gameObject;
                Debug.Log($"Входное соединение в объекте {name} выбрано");
            }
        }
    }

    #endregion

    // Add output in the list
    public void AddOutput(ItemComponent item)
    {
        // Add output to the list
        Outputs.Add(item);
        Debug.Log($"Связь между {item.name} и {name} создана");

        // Get start, end positions and draw line
        DrawLine(item.transform.Find("ConnectionInput").gameObject, OutputConnection, transform);
    }

    // Disactive panel
    public void DisactivaveUI()
    {
        UI.SetActive(false);
    }

    // Delete the item
    public void Delete() => Destroy(gameObject);

    // Changing name
    public void ChangeName(string name) => NameUnsaved = name;

    // Changing priority
    public void ChangePriority(float priority)
    {
        PriorityUnsaved = Mathf.Round(priority);
        if (PriorityLabel is not null)
        {
            PriorityLabel.text = $"{PriorityUnsaved}%";
        }
    }

    // Save
    public virtual void Save()
    {
        Priority = PriorityUnsaved;
        UpdateValues(NameUnsaved);
        DisactivaveUI();
    }

    // Abort
    public virtual void Abort()
    {
        NameUnsaved = gameObject.name;
        NameField.text = gameObject.name;

        PriorityUnsaved = Priority;
        PrioritySlider.value = PriorityUnsaved;
        DisactivaveUI();
    }

    #region Utils

    // Update fields values
    protected void UpdateValues(string value)
    {
        gameObject.name = value;
        NameField.text = value;
        if (PrioritySlider is not null)
        {
            PrioritySlider.value = Priority;
        }
    }

    // Draw lines for connection
    public static void DrawLine(GameObject start, GameObject end, Transform transform)
    {
        GameObject line = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        var component = line.AddComponent<LineComponent>();
        component.StartObject = start;
        component.EndObject = end;
        component.Draw(transform.parent);
    }

    protected LineComponent GetLine(GameObject start, GameObject end) => Model.DigitalTwin.GetComponentsInChildren<LineComponent>().Single(x => x.EndObject == start && x.StartObject == end);

    #endregion

    #region Empty virtuals

    // Get property for JSON
    public virtual List<string> GetProperties()
    {
        throw new System.Exception();
    }

    // Digital Twin simulation
    public virtual void Simulate()
    {
        throw new System.Exception("The method is not implemented");
    }

    #endregion
}
