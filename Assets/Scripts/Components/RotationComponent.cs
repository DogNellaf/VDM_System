using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationComponent : Element
{
    [SerializeField] private float speedX = 0;
    [SerializeField] private float speedY = 0;
    [SerializeField] private float speedZ = 1f;
    // Update is called once per frame
    public override void FixedUpdate()
    {
        transform.rotation *= Quaternion.Euler(speedX, speedY, speedZ);
    }
}
