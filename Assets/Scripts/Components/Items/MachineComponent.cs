using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineComponent : ItemComponent
{
    [SerializeField] private TMP_InputField OutputField;
    [SerializeField] private TMP_InputField InputField;
    [SerializeField] private TMP_InputField MaxPowerField;
    [SerializeField] private TMP_InputField SelfPowerField;

    [SerializeField] private GameObject InputConnection;
    [SerializeField] private GameObject OutputConnection;

    public List<InputComponent> Inputs = new();
    public List<WorkerComponent> Workers = new();
    public List<(Vector3 start, Vector3 end)> Lines = new();

    public string InputCount;
    public string OutputCount;
    public float MaxPower;
    public float SelfPower;

    private string inputUnsaved;
    private string outputUnsaved;
    private float maxPowerUnsaved;
    private float selfPowerUnsaved;

    private WorkspaceModel model => TwinApplication.GetModel<WorkspaceModel>();


    public override void OnMouseDown()
    {
        Ray ray = TwinApplication.Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            string hitObjectName = hit.collider.gameObject.name;
            if (hitObjectName == InputConnection.name)
            {
                var inputObject = model.SelectedOutputConnection;
                var connection = inputObject.GetComponent<InputComponent>();
                if (connection is not null)
                {
                    if (connection.gameObject != gameObject)
                    {
                        if (!Inputs.Contains(connection))
                        {
                            AddInput(connection);
                            Debug.Log($"Связь между {inputObject.name} и {name} создана");

                            var start = inputObject.transform.Find("ConnectionOutput").transform.position;
                            var end = InputConnection.transform.position;

                            Debug.Log(start);
                            Debug.Log(end);
                            DrawLine(start, end);

                            //Debug.DrawLine(start, end, Color.green, float.PositiveInfinity);
                            //Lines.Add((start, end));
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
            else if (hitObjectName == OutputConnection.name)
            {

            }
            else
            {
                base.OnMouseDown();
            }

            //foreach (var line in Lines)
            //{
            //    Debug.DrawLine(line.start, line.end, Color.green, 10000000f);
            //}
        }
    }

    private static void DrawLine(Vector3 start, Vector3 end)
    {
        GameObject segObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        var p3 = (start + end) * 0.5f;
        segObj.transform.localPosition = p3;
        segObj.transform.rotation = Quaternion.Euler(0, -90, 0);
        segObj.transform.localScale = new Vector3(0.01f, 0.01f, Vector3.Distance(start, end));
        segObj.transform.position = p3; //placebond here
        segObj.transform.LookAt(end);
    }

    public void AddInput(InputComponent input)
    {
        Inputs.Add(input);
        Debug.DrawLine(input.gameObject.transform.position, InputConnection.transform.position);
    }    

    // Changing input
    public void ChangeInput(string input) => inputUnsaved = input;

    // Changing output
    public void ChangeOutput(string output) => outputUnsaved = output;

    // Changing max power
    public void ChangeMaxPower(string maxPower)
    {
        if (float.TryParse(maxPower, out float result))
        {
            maxPowerUnsaved = result;
        }
        else
        {
            MaxPowerField.text= MaxPower.ToString();
            Debug.LogError("Incorrect Max Power");
        }
    }

    // Changing self power
    public void ChangeSelfPower(string selfPower)
    {
        if (float.TryParse(selfPower, out float result))
        {
            selfPowerUnsaved = result;
        }
        else
        {
            SelfPowerField.text = SelfPower.ToString();
            Debug.LogError("Incorrect Self Power");
        }
    }

    // Save
    public override void Save()
    {
        OutputCount = outputUnsaved;
        InputCount = inputUnsaved;
        MaxPower = maxPowerUnsaved;
        SelfPower = selfPowerUnsaved;
        base.Save();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Abort
    public override void Abort()
    {
        outputUnsaved = OutputCount;
        inputUnsaved = InputCount;
        maxPowerUnsaved = MaxPower;
        selfPowerUnsaved = SelfPower;
        base.Abort();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Change fields
    public void UpdateFieldValues()
    {
        OutputField.text = OutputCount;
        InputField.text = InputCount;
        MaxPowerField.text = MaxPower.ToString();
        SelfPowerField.text = SelfPower.ToString();
    }
}
