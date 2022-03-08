using UnityEngine;

public class MovementController : Element
{
    #region Local vars
    private Vector3? mousePosition = null;
    private new GameObject camera => Camera.main.gameObject;
    private Vector3 currentMousePosition => Camera.main.ScreenToViewportPoint(Input.mousePosition);
    private Vector3 cameraPosition
    {
        get
        {
            return camera.transform.position;
        }
        set
        {
            camera.transform.position = value;
        }

    }
    
    #endregion

    #region Preferences
    [SerializeField] private float speedLimiter = 5;

    [SerializeField] private (float Up, float Down) yLimit = (10, 1);

    [SerializeField] private (float Left, float Right) xLimit = (-38, 38);
    #endregion

    public void Update()
    {
        // средн€€ кнопка мыши
        if (Input.GetMouseButton(2))
        {
            //Debug.Log(currentMousePosition);
            if (mousePosition == null)
            {
                mousePosition = currentMousePosition;
            }
            else
            {
                // x movement
               
                var movementVector = new Vector3(cameraPosition.x - GetDiffirence("X"), cameraPosition.y, cameraPosition.z);

                if (application.IsDebug)
                {
                    Debug.Log($"X difference: {GetDiffirence("X")}");
                }

                if (movementVector.x > xLimit.Left && movementVector.x < xLimit.Right)
                {
                    cameraPosition = movementVector;
                }
                else if (application.IsDebug)
                {
                    Debug.Log($"X difference: {GetDiffirence("X")}");
                    if (movementVector.x <= xLimit.Left)
                    {
                        Debug.LogWarning("Camera reached left limit");
                    }
                    else
                    {
                        Debug.LogWarning("Camera reached right limit");
                    }
                }

                //y movement

                movementVector = new Vector3(cameraPosition.x, cameraPosition.y - GetDiffirence("Y"), cameraPosition.z);

                if (application.IsDebug)
                {
                    Debug.Log($"Y difference: {GetDiffirence("Y")}");
                }


                if (movementVector.y > yLimit.Down && movementVector.y < yLimit.Up)
                {
                    cameraPosition = movementVector;
                }
                else if (application.IsDebug)
                {
                    if (movementVector.y <= yLimit.Down)
                    {
                        Debug.LogWarning("Camera reached down limit");
                    }
                    else
                    {
                        Debug.LogWarning("Camera reached up limit");
                    }
                }

                mousePosition = currentMousePosition;
            }
        }
        else
        {
            mousePosition = null;
        }
    }

    private float GetDiffirence(string axis = "Y")
    {
        switch (axis)
        {
            case "X":
                return (mousePosition.Value.x - currentMousePosition.x) / speedLimiter;
            default:
                return (mousePosition.Value.y - currentMousePosition.y) / speedLimiter;
        }
    }
}
