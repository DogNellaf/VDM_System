using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#nullable enable

public class ItemComponent : Element
{
    [SerializeField] protected GameObject InputConnection;
    [SerializeField] protected GameObject OutputConnection;
    [SerializeField] protected GameObject UI;
    [SerializeField] protected TMP_InputField NameField;
    [SerializeField] protected string nameUnsaved;

    protected WorkspaceModel model => TwinApplication.GetModel<WorkspaceModel>();
    protected string? inputName
    {
        get
        {
            try
            {
                return InputConnection.gameObject.name;
            }
            catch
            {
                return null;
            }
        }
    }
    protected string? outputName
    {
        get
        {
            try
            {
                return OutputConnection.gameObject.name;
            }
            catch
            {
                return null;
            }
        }
    }

    public WorkerComponent Worker;
    public List<ItemComponent> Inputs = new();
    public float Value;

    // Start function
    public override void Start()
    {
        if (NameField != null)
        {
            NameField.text = gameObject.name;
            nameUnsaved = gameObject.name;
        }
    }

    // Add input in the list
    public void AddInput(ItemComponent item)
    {
        Inputs.Add(item);
        Debug.Log($"Связь между {item.name} и {name} создана");

        var start = item.transform.Find("ConnectionOutput").transform.position;
        var end = InputConnection.transform.position;
        DrawLine(start, end, transform);
    }

    // When User click
    public override void OnMouseDown()
    {
        Ray ray = TwinApplication.Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("true");
            string hitObjectName = hit.collider.gameObject.name;
            if (hitObjectName == name)
            {
                UI.SetActive(true);
            }
            else if (hitObjectName == inputName)
            {
                var inputObject = model.SelectedOutputConnection;
                var connection = inputObject.GetComponent<ItemComponent>();
                if (connection is not null)
                {
                    if (connection.gameObject != gameObject)
                    {
                        if (!Inputs.Contains(connection))
                        {
                            AddInput(connection);
                            model.SelectedOutputConnection = null;
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
                    Debug.Log("Сперва выберите результирующее соединение");
                }
            }
            else if (hitObjectName == outputName)
            {
                TwinApplication.GetModel<WorkspaceModel>().SelectedOutputConnection = gameObject;
                Debug.Log($"Результирующее соединение в объекте {name} выбрано");
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
    public void ChangeName(string name) => nameUnsaved = name;

    // Save
    public virtual void Save()
    {
        gameObject.name = nameUnsaved;
        NameField.text = gameObject.name;
        DisactivaveUI();
    }

    // Abort
    public virtual void Abort()
    {
        nameUnsaved = gameObject.name;
        NameField.text = nameUnsaved;
        DisactivaveUI();
    }

    protected static void DrawLine(Vector3 start, Vector3 end, Transform transform)
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
}
