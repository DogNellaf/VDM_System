using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputComponent : ItemComponent
{
    [SerializeField] private TMP_InputField IncreaseField;
    [SerializeField] private TMP_InputField LimitField;
    [SerializeField] private Slider PrioritySlider;
    [SerializeField] private TextMeshProUGUI PriorityLabel;

    public float Increase;
    public float Limit;
    public float Priority;

    private float increaseUnsaved;
    private float limitUnsaved;
    private float priorityUnsaved;

    public override void Start()
    {
        priorityUnsaved = 50;
        base.Start();
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

    // Changing priority
    public void ChangePriority(float priority)
    {
        priorityUnsaved = Mathf.Round(priority);
        PriorityLabel.text = $"{priorityUnsaved}%";
    }

    // Save
    public override void Save()
    {
        Increase = increaseUnsaved;
        Limit = limitUnsaved;
        Priority = priorityUnsaved;
        base.Save();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Abort
    public override void Abort()
    {
        increaseUnsaved = Increase;
        limitUnsaved = Limit;
        priorityUnsaved = Priority;
        base.Abort();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Change fields
    public void UpdateFieldValues()
    {
        IncreaseField.text = Increase.ToString();
        LimitField.text = Limit.ToString();
        PrioritySlider.value = priorityUnsaved;
    }
}
