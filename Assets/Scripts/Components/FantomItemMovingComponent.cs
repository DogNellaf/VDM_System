using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantomItemMovingComponent : Element
{
    [SerializeField] private GameObject orignal;

    [SerializeField] private bool isThisPlaceBusy = false;

    // Update is called once per frame
    public override void FixedUpdate()
    {
        Ray ray = application.Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var point = hit.point;
            var x = Mathf.Round(point.x);
            var y = transform.position.y;
            var z = Mathf.Round(point.z);
            transform.position = new Vector3(x, y, z);
        }
    }

    // When User click
    public override void OnMouseDown()
    {
        if (!isThisPlaceBusy)
        {
            var realItem = Instantiate(orignal);
            realItem.transform.SetParent(transform.parent);
            realItem.transform.position = transform.position;
            (application.View as WorkspaceView).ActivateCanvas();
            Destroy(gameObject);
        }
    }


    // When start collision
    public void OnTriggerEnter()
    {
        isThisPlaceBusy = true;
    }

    // When end collision
    public void OnTriggerExit()
    {
        isThisPlaceBusy = false;
    }
}
