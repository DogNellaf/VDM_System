using SFB;
using UnityEngine;
using UnityEngine.SceneManagement;


// Script for main menu interactions
public class MenuController : Element
{
    // Var for refactor code
    private MenuModel model;

    // Start of menu
    public override void Start()
    {
        model = TwinApplication.Model as MenuModel;
        model.VersionTextArea.text = model.Version;

        //if (factoryModel is null)
        //    factoryModel = GameObject.Find("factory_unity");
    }

    // Every frame model rotation
    public override void FixedUpdate()
    {
        var factory = model.FactoryModel;
        var speed = model.RotationSpeed;

        factory.transform.rotation *= new Quaternion(0, speed, 0, 1);
    }

    // Close the application
    public void Quit() => UnityEngine.Application.Quit();

    // Switch current scene to settings frame
    public void ShowSettings()
    {
        //TODO: Add settings frame and function to go
        throw new System.Exception("Functionality not added");
    }

    // Load virtual dublicates from file
    public void Load()
    {
        // Open file with filter
        var extensions = new[] {
            new ExtensionFilter("Digital Twin File", "json"),
        };
        var path = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false)[0];
        PlayerPrefs.SetString("LoadedDigitalTwin", path);
        ShowWorkspace();
    }

    // Start empty Digital Twin
    public void Create()
    {
        PlayerPrefs.SetString("LoadedDigitalTwin", "");
        ShowWorkspace();
    }

    // Start Workspace
    public void ShowWorkspace()
    {
        SceneManager.LoadScene(model.MainSceneName);
    }
}
