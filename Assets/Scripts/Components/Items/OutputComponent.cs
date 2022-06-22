using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutputComponent : ItemComponent
{
    [SerializeField] private TMP_InputField CountField;
    [SerializeField] private TMP_InputField PriceField;

    public float Count;
    public float Price;

    private float countUnsaved;
    private float priceUnsaved;

    // Simulation
    public override void Simulate()
    {
        Value = 0;
        foreach (var input in Inputs)
        {
            var inputElement = input.gameObject.transform.Find("ConnectionOutput").gameObject;
            var line = GetLine(inputElement, InputConnection);
            if (Value + input.Value <= Count)
            {
                Value += input.Value * Price;
                line.UpdateText(input.Value);
                input.Value = 0;
            }
            else if (Value < Count)
            {
                var difference = Count - Value;
                Value += difference * Price;
                line.UpdateText(difference);
                input.Value -= difference;
            }
        }
    }

    // Changing increase value
    public void ChangeCount(string count)
    {
        if (float.TryParse(count, out float result))
        {
            countUnsaved = result;
        }
        else
        {
            CountField.text = countUnsaved.ToString();
            Debug.LogError("Incorrect Count");
        }
    }

    // Changing limit
    public void ChangePrice(string price)
    {
        if (float.TryParse(price, out float result))
        {
            priceUnsaved = result;
        }
        else
        {
            PriceField.text = priceUnsaved.ToString();
            Debug.LogError("Incorrect Price");
        }
    }

    // Save
    public override void Save()
    {
        Count = countUnsaved;
        Price = priceUnsaved;
        base.Save();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Abort
    public override void Abort()
    {
        countUnsaved = Count;
        priceUnsaved = Price;
        base.Abort();
        UpdateFieldValues();
        DisactivaveUI();
    }



    #region utils

    public override List<string> GetProperties()
    {
        return new List<string> { $"{Count}", $"{Price}", $"{Priority}" };
    }

    // Change fields
    public void UpdateFieldValues()
    {
        CountField.text = Count.ToString();
        PriceField.text = Price.ToString();
    }

    #endregion
}
