using System.Collections.Generic;
using UnityEngine;

// Equipment (tools, places and soon) model
public class Equipment : MonoBehaviour
{
    // Attached employees
    public List<Employee> Workers = null;

    // Max employess count
    public int MaxEmployeesCount;

    // Quantity input materials per one cicle 
    public float InputQuantity;

    // Quantity output things per one cicle
    public float OutputQuantity;

    // Time for one circle (in seconds)
    public float CicleTicksCount;

    // The timestamp of current cicle
    public float CurrentCicleCount;
}
