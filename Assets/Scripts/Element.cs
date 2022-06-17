using UnityEngine;

// Base class for all elements
public class Element : MonoBehaviour
{
    // Function called with application start
    public virtual void Start() { }

    // Function called with every frame update
    public virtual void FixedUpdate() { }

    // Function called with player collider or UI click
    public virtual void OnMouseDown() { }

    // Function called with player collider or UI drag
    public virtual void OnMouseDrag() { }

    // Function called with player collider or UI click exit
    public virtual void OnMouseUp() { }

    // Get current application and all of instances
    public VirtualDoubleApplication TwinApplication
    {
        get
        {
            return GameObject.Find("Application").GetComponent<VirtualDoubleApplication>();
        }
    }
}
