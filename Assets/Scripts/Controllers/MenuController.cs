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
        model = this.application.Model as MenuModel;
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
    public void Quit() => Application.Quit();

    // Switch current scene to settings frame
    public void ShowSettings()
    {
        //TODO: Add settings frame and function to go
        throw new System.Exception("Functionality not added");
    }

    // Load virtual dublicates from file
    public void ShowLoadedMainFrame()
    {
        SceneManager.LoadScene(model.MainSceneName);
        //TODO: load function to csv/json/xml files
    }

    // Start Main Frame with empty default VirtualDouble
    public void ShowDefaultMainFrame()
    {
        SceneManager.LoadScene(model.MainSceneName);
        //TODO: Add default Main Frame
    }
}
