using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class MachineComponent : ItemComponent
{
    [SerializeField] private TMP_InputField OutputField;
    [SerializeField] private TMP_InputField InputField;
    [SerializeField] private TMP_InputField MaxPowerField;
    [SerializeField] private TMP_InputField SelfPowerField;

    public string InputCount;
    public string OutputCount;
    public float MaxPower;
    public float SelfPower;

    private string inputUnsaved;
    private string outputUnsaved;
    private float maxPowerUnsaved;
    private float selfPowerUnsaved;

    public override List<string> GetProperties()
    {
        return new List<string> { $"{InputCount}", $"{OutputCount}", $"{MaxPower}", $"{SelfPower}" };
    }

    public override void Simulate()
    {
        var model = TwinApplication.GetModel<WorkspaceModel>();
        var globalInputs = model.DigitalTwin.transform.GetComponentsInChildren<ItemComponent>();
        var currentInputs = globalInputs.Where(x => x.Type == ItemType.Input && x.Outputs.Contains(this));
        var performance = Worker.GetComponent<WorkerComponent>().Performance + SelfPower;
        float value = 0;

        foreach (var input in currentInputs)
        {
            if (value < MaxPower)
            {
                //input.Value * input.
            }
        }
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
