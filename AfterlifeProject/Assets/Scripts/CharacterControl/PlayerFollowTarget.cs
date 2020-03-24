using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowTarget : MonoBehaviour
{
    [SerializeField] private Transform target;

    private float smoothSpeed;
    private float rotateSpeed;

    private void OnEnable()
    {
        target = transform.parent.GetChild(2).transform;
        smoothSpeed = 0.125f;
        rotateSpeed = 2f;
    }

    private void FixedUpdate()
    {
        MoveToTarget();
        RotateToTarget();
    }

    private void MoveToTarget()
    {
        Vector3 desiredPos = new Vector3(target.position.x, target.position.y, target.position.z - 2);
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
        transform.position = smoothedPos;
    }

    private void RotateToTarget()
    {
        Vector3 targetPosition = target.transform.position - transform.position;
        float rotateStep = rotateSpeed * Time.deltaTime;

        Vector3 direction = Vector3.RotateTowards(transform.forward, targetPosition, rotateStep, 0.0f);
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
