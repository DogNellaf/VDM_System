using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public sealed class JsonSaveComponent : Element
{
    [SerializeField] GameObject Machine;
    [SerializeField] GameObject Input;
    [SerializeField] GameObject Output;
    [SerializeField] GameObject Worker;

    private WorkspaceModel model => TwinApplication.GetModel<WorkspaceModel>();
    private GameObject factory => model.DigitalTwin;

    public void Save()
    {
        string json = "";
        foreach (Transform child in factory.transform)
        {
            var item = new ItemJsonModel(child.gameObject);
            json += $"{JsonUtility.ToJson(item)}Е";
        }
        json = json.Trim('Е');
        File.WriteAllText("digital.json", json);
        Debug.Log("—охранение успешно");
    }

    public void Load(string path)
    {
        string raw = File.ReadAllText(path);
        var rawItems = raw.Split('Е');
        var models = new List<ItemJsonModel>();
        var objects = new List<GameObject>();
        foreach (string rawItem in rawItems)
        {
            var item = JsonUtility.FromJson<ItemJsonModel>(rawItem);
            if (item.Type != ItemType.Line)
            {
                GameObject newGameObject = CreateObjectByType(item.Type, item.Properties);
                newGameObject.transform.parent = factory.transform;
                newGameObject.transform.rotation = item.Rotation;
                newGameObject.transform.localScale = item.Scale;
                newGameObject.transform.position = item.Position;
                newGameObject.name = item.Name;
                models.Add(item);
                objects.Add(newGameObject);
            }
        }

        for (int i = 0; i < rawItems.Length; i++)
        {
            var item = models[i];
            if (item.Type != ItemType.Line)
            {
                var itemObject = objects[i];
                var component = itemObject.GetComponent<ItemComponent>();
                foreach (string connection in item.Connectons)
                {
                    var connectonObject = objects.Single(x => x.name == connection);
                    component.AddInput(connectonObject.GetComponent<ItemComponent>());
                }
            }
        }

        Debug.Log("÷ифровой двойник успешно загружен");
    }

    private GameObject CreateObjectByType(ItemType type, List<string> properties)
    {
        switch (type)
        {
            case ItemType.Input:
                var input = Instantiate(Input);
                var inputComponent = input.GetComponent<InputComponent>();
                inputComponent.ChangeIncrease(properties[0]);
                inputComponent.ChangeLimit(properties[1]);
                inputComponent.ChangePriority(float.Parse(properties[2]));
                inputComponent.Save();
                model.InputsCount++;
                return input;
            case ItemType.Output:
                var output = Instantiate(Output);
                var outputComponent = output.GetComponent<OutputComponent>();
                outputComponent.ChangeCount(properties[0]);
                outputComponent.ChangePrice(properties[1]);
                outputComponent.ChangePriority(float.Parse(properties[2]));
                outputComponent.Save();
                model.OutputsCount++;
                return output;
            case ItemType.Machine:
                var machine = Instantiate(Machine);
                var machineComponent = machine.GetComponent<MachineComponent>();
                machineComponent.ChangeInput(properties[0]);
                machineComponent.ChangeOutput(properties[1]);
                machineComponent.ChangeMaxPower(properties[2]);
                machineComponent.ChangeSelfPower(properties[3]);
                machineComponent.Save();
                model.MachinesCount++;
                return machine;
            case ItemType.Worker:
                var worker = Instantiate(Worker);
                var workerComponent = worker.GetComponent<WorkerComponent>();
                workerComponent.ChangePerformance(properties[0]);
                workerComponent.ChangeWorkTime(properties[1]);
                workerComponent.ChangeRelaxTime(properties[2]);
                workerComponent.Save();
                model.WorkersCount++;
                return worker;
            default:
                var line = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                line.layer = LayerMask.NameToLayer("Ignore Raycast");
                return line;

        }
    }
}
