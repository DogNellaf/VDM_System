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
    public GameObject DigitalTwin;

    // Canvas
    public GameObject Canvas;

    // Global count of machines
    public int MachinesCount = 0;

    // Global count of inputs
    public int InputsCount = 0;

    // Global count of outputs
    public int OutputsCount = 0;

    // Global count of workers
    public int WorkersCount = 0;

    // Current selected input connection

    //public InputComponent SelectedInputConnection;

    // Current selected output connection
    public GameObject SelectedOutputConnection;

    // Simulation state
    public bool IsSimulationStarted = false;

    // Simulation speed
    public float SimulationSpeed = 1;

    // Text prefab
    public GameObject LinkTextFieldPrefab;
}
