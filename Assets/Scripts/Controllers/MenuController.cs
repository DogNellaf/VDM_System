using UnityEngine;
using UnityEngine.SceneManagement;

// Script for main menu interactions
public class MenuController : Element
{
    // Var for refactor code
    private MenuModel Model;

    // Start of menu
    public override void Start()
    {
        Model = this.application.Model as MenuModel;
        Model.VersionTextArea.text = Model.Version;
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
        SceneManager.LoadScene(Model.MainSceneName);
        //TODO: load function to csv/json/xml files
    }

    // Start Main Frame with empty default VirtualDouble
    public void ShowDefaultMainFrame()
    {
        SceneManager.LoadScene(Model.MainSceneName);
        //TODO: Add default Main Frame
    }
}
