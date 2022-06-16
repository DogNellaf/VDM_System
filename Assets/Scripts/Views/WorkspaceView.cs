using UnityEngine;

public class WorkspaceView : Element
{
    // Model for refactoring
    private WorkspaceModel model => application.Model as WorkspaceModel;

    // Mouse movement offest
    private Vector3 offset;

    // Camera var for refactor
    //private new Camera camera;

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
            //Vector3 screenPoint = Camera.main.WorldToViewportPoint(model.Ground.transform.position);
            //return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
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

    public void CenterCamera()
    {
        cameraPosition = cameraStartPosition;
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

    // Little function for camera movement
    //private void MoveCamera(Vector3 direction)
    //{
    //    direction *= model.UserMovementSpeed;

    //    var convertedDirection = camera.ScreenToWorldPoint(direction);
    //    camera.transform.position = convertedDirection + offset;
    //}

    private void ChangeCameraY(float y)
    {
        cameraPosition += new Vector3(0, y, 0);
    }

    #endregion
}
