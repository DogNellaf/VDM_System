using TMPro;
using UnityEngine;

// Class with menu info
public class MenuModel : Element
{
    // Current verison
    public string Version = "Version 0.0.1";

    // Text area component to version text
    public TextMeshProUGUI VersionTextArea;

    // Name of workspace scene
    public string MainSceneName = "Workspace";

    // Background model rotation speed
    public float RotationSpeed;

    // Background model
    public GameObject FactoryModel;
}
