using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceController : Element
{
    // Var for refactor code
    private WorkspaceModel model;

    // Up UI log
    private string log;

    // Up UI log queue
    private Queue logQueue = new Queue();

    // Start of workspace
    public override void Start()
    {
        base.Start();
    }

    // Every frame
    public override void FixedUpdate()
    {

    }

    public void IncreseCount(string objectName)
    {
        var model = TwinApplication.Model as WorkspaceModel;
        switch (objectName)
        {
            case "Machine":
                model.MachinesCount += 1;
                break;
            case "Input":
                model.InputsCount += 1;
                break;
            case "Output":
                model.OutputsCount += 1;
                break;
            case "Worker":
                model.WorkersCount += 1;
                break;
            default:
                throw new System.Exception("Unknown object");
        }
    }

    public int GetCount(string objectName)
    {
        var model = TwinApplication.Model as WorkspaceModel;
        switch (objectName)
        {
            case "Machine":
                return model.MachinesCount;
            case "Input":
                return model.InputsCount;
            case "Output":
                return model.OutputsCount;
            case "Worker":
                return model.WorkersCount;
            default:
                throw new System.Exception("Unknown object");
        }
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string newString = $"\n[{type}] {logString}";
        logQueue.Enqueue(newString);
        
        if (logQueue.Count > 4)
        {
            logQueue.Dequeue();
        }

        log = string.Empty;
        foreach (string mylog in logQueue)
        {
            log += mylog;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 300, 0, 600, 200), log);
    }
}
