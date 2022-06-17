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

    public string Input;
    public string Output;
    public float MaxPower;
    public float SelfPower;

    private string inputUnsaved;
    private string outputUnsaved;
    private float maxPowerUnsaved;
    private float selfPowerUnsaved;

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
        Output = outputUnsaved;
        Input = inputUnsaved;
        MaxPower = maxPowerUnsaved;
        SelfPower = selfPowerUnsaved;
        base.Save();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Abort
    public override void Abort()
    {
        outputUnsaved = Output;
        inputUnsaved = Input;
        maxPowerUnsaved = MaxPower;
        selfPowerUnsaved = SelfPower;
        base.Abort();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Change fields
    public void UpdateFieldValues()
    {
        OutputField.text = Output;
        InputField.text = Input;
        MaxPowerField.text = MaxPower.ToString();
        SelfPowerField.text = SelfPower.ToString();
    }
}
