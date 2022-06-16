// Virtual double in Workspace info
using UnityEngine;

public class WorkspaceModel : Element
{
    // Workspace speed of user movement
    public float UserMovementSpeed = 1.5f;

    // Height Increase speed of user movement
    public float UserHeightIncreaseSpeed = 10;

    // Max camera height
    public float MaxCameraHeight = 40;

    // Min camera height
    public float MinCameraHeight = 5;

    // Ground of the Digital Twin
    public GameObject Ground;

    // Digital Twin Object
    public GameObject DigitalTwin => GameObject.Find("Factory");

    // Canvas
    public GameObject Canvas;
}
