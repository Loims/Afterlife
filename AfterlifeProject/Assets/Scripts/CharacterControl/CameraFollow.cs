using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.5f;

    private bool constrainX = true;

    public Vector3 offset = new Vector3(0, 3, -10);

    private void OnEnable()
    {
        target = transform.parent.GetComponentInChildren<PlayerFollowTarget>().gameObject.transform;
    }

    /// <summary>
    /// Very simple camera follow script. Follows the player with a contrain to prevent the player from exiting the play space
    /// </summary>
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPos;
    }

    public void ReleaseXConstrain()
    {
        constrainX = false;
    }
}
