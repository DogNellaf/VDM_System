using UnityEngine;

// Script for main menu interactions
internal class MenuInteraction : MonoBehaviour
{
    // Close the application
    public void Quit() => Application.Quit();

    // Switch current scene to settings frame
    public void ShowSettings()
    {
        //TODO: Add settings frame and function to go
    }

    // Load virtual dublicates from file
    public void ShowLoadedMainFrame()
    {
        //TODO: Add Main Frame and load function to csv/json/xml files
    }

    // Start Main Frame with empty default VirtualDouble
    public void ShowDefaultMainFrame()
    {
        //TODO: Add default Main Frame
    }
}
