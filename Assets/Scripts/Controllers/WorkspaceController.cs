using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkspaceController : Element
{
    // Var for refactor code
    private WorkspaceModel model;

    // Start of workspace
    public override void Start()
    {
        model = this.application.Model as WorkspaceModel;
    }

    // Every frame
    public override void FixedUpdate()
    {

    }
}
