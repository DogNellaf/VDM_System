using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputComponent : ItemComponent
{
    [SerializeField] private TMP_InputField IncreaseField;
    [SerializeField] private TMP_InputField LimitField;

    public float Increase;
    public float Limit;

    private float increaseUnsaved;
    private float limitUnsaved;

    // Simulation
    public override void Simulate()
    {
        if (Value < Limit)
        {
            Value += Increase;
        }
        else
        {
            Value = Limit;
        }
        
        //foreach (var output in Outputs)
        //{
        //    output.Simulate();
        //}
    }

    // Changing increase value
    public void ChangeIncrease(string increase)
    {
        if (float.TryParse(increase, out float result))
        {
            increaseUnsaved = result;
        }
        else
        {
            IncreaseField.text = increaseUnsaved.ToString();
            Debug.LogError("Incorrect Increase Value");
        }
    }

    // Changing limit
    public void ChangeLimit(string limit)
    {
        if (float.TryParse(limit, out float result))
        {
            limitUnsaved = result;
        }
        else
        {
            LimitField.text = limitUnsaved.ToString();
            Debug.LogError("Incorrect Limit");
        }
    }

    // Save
    public override void Save()
    {
        Increase = increaseUnsaved;
        Limit = limitUnsaved;
        base.Save();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Abort
    public override void Abort()
    {
        increaseUnsaved = Increase;
        limitUnsaved = Limit;
        base.Abort();
        UpdateFieldValues();
        DisactivaveUI();
    }

    #region Utils

    // Change fields
    public void UpdateFieldValues()
    {
        IncreaseField.text = Increase.ToString();
        LimitField.text = Limit.ToString();
    }

    public override List<string> GetProperties()
    {
        return new List<string> { $"{Increase}", $"{Limit}", $"{Priority}" };
    }

    #endregion
}
