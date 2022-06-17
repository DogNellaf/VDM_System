using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutputComponent : ItemComponent
{
    [SerializeField] private TMP_InputField CountField;
    [SerializeField] private TMP_InputField PriceField;
    [SerializeField] private Slider PrioritySlider;
    [SerializeField] private TextMeshProUGUI PriorityLabel;

    public float Count;
    public float Price;
    public float Priority;

    private float countUnsaved;
    private float priceUnsaved;
    private float priorityUnsaved;

    public override void Start()
    {
        priorityUnsaved = 50;
        base.Start();
    }

    public override List<string> GetProperties()
    {
        return new List<string> { $"{Count}", $"{Price}", $"{Priority}" };
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

    // Changing priority
    public void ChangePriority(float priority)
    {
        priorityUnsaved = Mathf.Round(priority);
        PriorityLabel.text = $"{priorityUnsaved}%";
    }

    // Save
    public override void Save()
    {
        Count = countUnsaved;
        Price = priceUnsaved;
        Priority = priorityUnsaved;
        base.Save();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Abort
    public override void Abort()
    {
        countUnsaved = Count;
        priceUnsaved = Price;
        priorityUnsaved = Priority;
        base.Abort();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Change fields
    public void UpdateFieldValues()
    {
        CountField.text = Count.ToString();
        PriceField.text = Price.ToString();
        PrioritySlider.value = priorityUnsaved;
    }
}
