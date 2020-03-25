using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.5f;

    public Vector3 offset = new Vector3(0, 1, -10);

    private void OnEnable()
    {
        target = transform.parent.GetComponentInChildren<PlayerFollowTarget>().gameObject.transform;
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        Vector3 clampedPos = new Vector3(Mathf.Clamp(smoothedPos.x, -1.5f, 1.5f), Mathf.Clamp(smoothedPos.y, -1.5f, 1.5f), smoothedPos.z);
        transform.position = clampedPos;
    }
}
