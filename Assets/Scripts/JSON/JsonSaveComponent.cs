using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        string json = "[";
        foreach (Transform child in factory.transform)
        {
            var item = new ItemJsonModel(child.gameObject);
            JsonWriter writer = new JsonWriter();
            writer.PrettyPrint = true;
            JsonMapper.ToJson(item, writer);
            json += $"{writer},\n";
        }
        json += ']';
        json = json.Replace(",\n]", "]");
        File.WriteAllText("digital.json", json);
        Debug.Log("Сохранение успешно");
    }

    public void Load(string path)
    {
        string raw = File.ReadAllText(path);
        raw = raw.Replace("[", "").Replace("]", "");
        var rawItems = raw.Split(',');
        foreach (string rawItem in rawItems)
        {
            var item = JsonMapper.ToObject<ItemJsonModel>(rawItem);
            GameObject newGameObject = CreateObjectByType(item.Type);
            newGameObject.transform.parent = factory.transform;
            newGameObject.transform.rotation = item.Rotation;
            newGameObject.transform.localScale = item.Scale;
            newGameObject.transform.position = item.Position;
            newGameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            newGameObject.name = item.Name;
        }
        Debug.Log("Цифровой двойник успешно загружен");
    }

    private GameObject CreateObjectByType(ItemType type)
    {
        switch (type)
        {
            case ItemType.Input:
                return Instantiate(Input);
            case ItemType.Output:
                return Instantiate(Output);
            case ItemType.Machine:
                return Instantiate(Machine);
            case ItemType.Worker:
                return Instantiate(Worker);
            default:
                var line = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                line.layer = LayerMask.NameToLayer("Ignore Raycast");
                return line;

        }
    }
}
