using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#nullable enable


public class ItemComponent : Element
{
    public ItemType Type;
    public WorkerComponent Worker;
    public List<ItemComponent> Outputs = new();
    public float Value;

    [SerializeField] protected GameObject InputConnection;
    [SerializeField] protected GameObject OutputConnection;
    [SerializeField] protected GameObject UI;
    [SerializeField] protected TMP_InputField NameField;
    [SerializeField] protected string NameUnsaved;

    protected WorkspaceModel Model => TwinApplication.GetModel<WorkspaceModel>();
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

    // Start function
    public override void Start()
    {
        if (NameField != null)
        {
            NameField.text = gameObject.name;
            NameUnsaved = gameObject.name;
        }
    }

    // Digital Twin simulation
    public virtual void Simulate()
    {

    }

    // Add output in the list
    public void AddOutput(ItemComponent item)
    {
        // Add output to the list
        Outputs.Add(item);
        Debug.Log($"Связь между {item.name} и {name} создана");

        // Get start, end positions and draw line
        var start = transform.Find("ConnectionOutput").transform.position;
        var end = item.InputConnection.transform.position;
        DrawLine(start, end, transform);
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

    // Disactive panel
    public void DisactivaveUI()
    {
        UI.SetActive(false);
    }

    // Delete the item
    public void Delete() => Destroy(gameObject);

    // Changing name
    public void ChangeName(string name) => NameUnsaved = name;

    // Save
    public virtual void Save()
    {
        gameObject.name = NameUnsaved;
        NameField.text = gameObject.name;
        DisactivaveUI();
    }

    // Abort
    public virtual void Abort()
    {
        NameUnsaved = gameObject.name;
        NameField.text = NameUnsaved;
        DisactivaveUI();
    }

    public static void DrawLine(Vector3 start, Vector3 end, Transform transform)
    {
        GameObject line = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        var p3 = (start + end) * 0.5f;
        line.transform.parent = transform.parent;
        line.transform.localPosition = p3;
        line.transform.rotation = Quaternion.Euler(0, -90, 0);
        line.transform.localScale = new Vector3(0.01f, 0.01f, Vector3.Distance(start, end));
        line.transform.position = p3; //placebond here
        line.transform.LookAt(end);
        line.layer = LayerMask.NameToLayer("Ignore Raycast");
        line.name = $"line |{start}| to |{end}|";
    }
    
    public virtual List<string> GetProperties()
    {
        throw new System.Exception();
    }
}
