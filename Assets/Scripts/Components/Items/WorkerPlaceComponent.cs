using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPlaceComponent : Element
{
    public void OnTriggerEnter(Collider other)
    {
        var component = other.GetComponent<ItemComponent>();
        if (component is not null)
        {
            if (component.Type == ItemType.Worker)
            {
                transform.parent.gameObject.GetComponent<MachineComponent>().Worker = component as WorkerComponent;
                Debug.Log($"Рабочий {component.name} привязан к {transform.parent.name}");
            }
        }
    }
}
