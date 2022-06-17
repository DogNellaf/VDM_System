using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkerComponent : ItemComponent
{
    [SerializeField] private TMP_InputField PerformanceField;
    [SerializeField] private TMP_InputField WorkField;
    [SerializeField] private TMP_InputField RelaxField;

    public float Performance;
    public float WorkTime;
    public float RelaxTime;

    private float performanceUnsaved;
    private float workTimeUnsaved;
    private float relaxTimeUnsaved;

    public override Dictionary<string, string> GetProperties()
    {
        return new Dictionary<string, string>
        {
            {"Performance", $"{Performance}"},
            {"WorkTime", $"{WorkTime}"},
            {"RelaxTime", $"{RelaxTime}"},
        };
    }

    // Changing performance value
    public void ChangePerformance(string performance)
    {
        if (float.TryParse(performance, out float result))
        {
            performanceUnsaved = result;
        }
        else
        {
            PerformanceField.text = performanceUnsaved.ToString();
            Debug.LogError("Incorrect Performance Value");
        }
    }

    // Changing work time
    public void ChangeWorkTime(string workTime)
    {
        if (float.TryParse(workTime, out float result))
        {
            workTimeUnsaved = result;
        }
        else
        {
            WorkField.text = workTimeUnsaved.ToString();
            Debug.LogError("Incorrect Work Time");
        }
    }

    // Changing relax time
    public void ChangeRelaxTime(string relaxTime)
    {
        if (float.TryParse(relaxTime, out float result))
        {
            relaxTimeUnsaved = result;
        }
        else
        {
            RelaxField.text = relaxTimeUnsaved.ToString();
            Debug.LogError("Incorrect Relax Time");
        }
    }

    // Save
    public override void Save()
    {
        Performance = performanceUnsaved;
        WorkTime = workTimeUnsaved;
        RelaxTime = relaxTimeUnsaved;
        base.Save();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Abort
    public override void Abort()
    {
        performanceUnsaved = Performance;
        workTimeUnsaved = WorkTime;
        relaxTimeUnsaved = RelaxTime;
        base.Abort();
        UpdateFieldValues();
        DisactivaveUI();
    }

    // Change fields
    public void UpdateFieldValues()
    {
        PerformanceField.text = Performance.ToString();
        WorkField.text = WorkTime.ToString();
        RelaxField.text = RelaxTime.ToString();
    }
}
