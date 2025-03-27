using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CameraSet : MonoBehaviour
{
    Transform Target;
    Vector3 referenceVelocity;

    public float distance = 20;
    public float height = 15f;
    public float angle = 0;
    public float smoothness = 0f;

    private void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        HandleCamera();
    }

    private void HandleCamera()
    {
        if (!Target)
        {
            return;
        }

        Vector3 worldPos = (Vector3.forward * -distance) + (Vector3.up * height);
        Vector3 angleVector = Quaternion.AngleAxis(angle, Vector3.up) * worldPos;

        Vector3 flatPos = Target.position;

        flatPos.y = 0;

        Vector3 finalPos = flatPos + angleVector; 

        transform.position = Vector3.SmoothDamp(transform.position, finalPos, ref referenceVelocity, smoothness);




        transform.LookAt(flatPos);
    }

    private void Update()
    {
        HandleCamera();
    }
}
