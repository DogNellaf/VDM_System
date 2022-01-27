using UnityEngine;

// Base class for all elements
public class Element : MonoBehaviour
{
    // Function called with application start
    public virtual void Start()
    {
        
    }

    // Get current application and all of instances
    public VirtualDoubleApplication application
    {
        get
        {
            return GameObject.Find("Application").GetComponent<VirtualDoubleApplication>();
        }
    }
}
