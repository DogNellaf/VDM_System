using UnityEngine;

public class WorkspaceView : Element
{
    // Model for refactoring
    //private WorkspaceModel model;

    // Mouse movement offest
    private Vector3 offset;

    // Camera var for refactor
    private new Camera camera;

    // Check Mouse Middle
    private bool mouseMiddlePushed => Input.GetMouseButton(2);

    // Start is called before the first frame update
    public override void Start()
    {
        //model = application.Model as WorkspaceModel;
        camera = application.Camera;
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {

    }

    // User mouse down handler
    public override void OnMouseDown()
    {
        if (mouseMiddlePushed)
        {
            var movement = new Vector3(-Input.mousePosition.x, 0, Input.mousePosition.y);
            offset = camera.transform.position - camera.ScreenToWorldPoint(movement);
        }

    }

    // User mouse drag handler
    public override void OnMouseDrag()
    {
        if (mouseMiddlePushed)
        {
            var model = application.Model as WorkspaceModel;
            //Vector3 newPosition = new(Input.mousePosition.x, 0, 0);
            //newPosition *= model.UserMovementSpeed;
            Vector3 newPosition = new Vector3(-Input.mousePosition.x, 0, Input.mousePosition.y);
            var newVector = (camera.ScreenToWorldPoint(newPosition) + offset) * model.UserMovementSpeed;
            var oldVector = camera.transform.position;
            camera.transform.position = new Vector3(oldVector.x, oldVector.y, newVector.z);
            //MoveCamera(newPosition);
        }
    }

    // User mouse up handler
    public override void OnMouseUp()
    {
        if (mouseMiddlePushed)
        {
            offset = Vector3.zero;
        }
    }

    #region Utils

    // Little function for camera movement
    private void MoveCamera(Vector3 direction)
    {
        var model = application.Model as WorkspaceModel;
        direction *= model.UserMovementSpeed;
        
        var convertedDirection = camera.ScreenToWorldPoint(direction);
        camera.transform.position = convertedDirection + offset;
    }

    #endregion
}
