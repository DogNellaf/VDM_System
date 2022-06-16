using UnityEngine;
using UnityEngine.SceneManagement;

public class WorkspaceView : Element
{
    // Model for refactoring
    private WorkspaceModel model => application.Model as WorkspaceModel;

    // Check mouse middle push
    private bool isMiddleMouseDown => Input.GetMouseButton(2);

    // Value of mouse scroll whell
    private float mouseWhellScroll => Input.GetAxis("Mouse ScrollWheel");

    // Start position of camera
    private Vector3 cameraStartPosition;

    // Camera position
    private Vector3 cameraPosition 
    { 
        get
        {
            return application.Camera.transform.position;
        }
        set
        {
            application.Camera.transform.position = value;
        }
    }

    // Check factory visibility
    private bool isFactoryVisibile
    {
        get
        {
            return true;
        }
    }

    // Start is called before the first frame update
    public override void Start()
    {
        //model = application.Model as WorkspaceModel;
        //camera = application.Camera;
        cameraStartPosition = cameraPosition;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        if (isMiddleMouseDown)
        {
            OnMouseDrag();
        }

        if (mouseWhellScroll != 0)
        {
            HeightIncrease();
        }
    }

    //TODO: Make camera movement more smooth

    private void HeightIncrease()
    {
        bool isDownMovement = mouseWhellScroll > 0 && cameraPosition.y > model.MinCameraHeight;
        bool isUpMovement = mouseWhellScroll < 0 && cameraPosition.y < model.MaxCameraHeight;
        if (isDownMovement || isUpMovement)
        {
            cameraPosition += new Vector3(0, -mouseWhellScroll * model.UserHeightIncreaseSpeed, 0);
        }
        else if (application.IsDebug)
        {
            if (cameraPosition.y <= model.MinCameraHeight)
            {
                Debug.LogWarning("User tried move camera below MinCameraHeight");
                cameraPosition = new Vector3(cameraPosition.x, model.MinCameraHeight, cameraPosition.z);
            }
            else
            {
                Debug.LogWarning("User tried move camera higher MaxCameraHeight");
                cameraPosition = new Vector3(cameraPosition.x, model.MaxCameraHeight, cameraPosition.z);
            }
        }
    }

    // Go to the menu without saving
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Move camera to the start position
    public void CenterCamera()
    {
        cameraPosition = cameraStartPosition;
    }

    // Find the world position per click
    public void SearchPosition(Vector3 position)
    {
        Debug.Log(application.Camera.ScreenToWorldPoint(position));
    }

    // Make object create stage
    public void CreateObject(GameObject prefab)
    {
        DeactivateCanvas();
        var item = Instantiate(prefab);
        item.transform.parent = model.DigitalTwin.transform;
        //item.transform.position += new Vector3(0, y, 0);
        //item.transform.rotation = rotation;
    }

    // Deactivate canvas
    public void DeactivateCanvas()
    {
        model.Canvas.SetActive(false);
    }

    // Activate canvas
    public void ActivateCanvas()
    {
        model.Canvas.SetActive(true);
    }

    // User mouse drag handler
    public override void OnMouseDrag()
    {
        if (isFactoryVisibile)
        {
            var rightMovement = Vector3.right * Input.GetAxis("Mouse X");
            var forwardMovement = Vector3.forward * Input.GetAxis("Mouse Y");
            var move = (rightMovement + forwardMovement) * model.UserMovementSpeed;
            cameraPosition += move;
            if (!isFactoryVisibile)
            {
                cameraPosition -= move;
            }
        }
        else if (application.IsDebug)
        {
            Debug.LogWarning("The ground is out of sight");
        }
    }

    #region Utils


    #endregion
}
