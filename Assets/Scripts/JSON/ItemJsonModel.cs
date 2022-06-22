using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemJsonModel
{
    public string Name;
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;
    public ItemType Type;
    public List<string> Properties;
    public List<string> Connectons;

    public ItemJsonModel()
    {

    }

    public ItemJsonModel(GameObject gameObject)
    {
        Name = gameObject.name;
        Position = gameObject.transform.position;
        Rotation = gameObject.transform.rotation;
        Scale = gameObject.transform.localScale;

        var component = gameObject.GetComponent<ItemComponent>();
        if (component is not null)
        {
            Type = component.Type;
            Properties = component.GetProperties();
            Connectons = new List<string>();
            foreach (var connection in component.Outputs)
            {
                Connectons.Add(connection.name);
            }
        }
        else
        {
            Type = ItemType.Line;
        }
    }
}
