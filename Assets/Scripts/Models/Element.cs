using UnityEngine;

// Base class for all elements
public class Element : MonoBehaviour
{
    // Function called with application start
    public virtual void Start()
    {
        
    }

    // Function called with every frame update
    public virtual void FixedUpdate()
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
