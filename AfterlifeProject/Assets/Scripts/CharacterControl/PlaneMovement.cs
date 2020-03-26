using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    private PlayerMovement movementComp;

    public float moveSpeed;

    private void OnEnable()
    {
        movementComp = GetComponentInChildren<PlayerMovement>();
    }
    void Update()
    {
        moveSpeed = movementComp.xyspeed + 2;
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void ResetPlane()
    {
        transform.position = Vector3.zero;
    }
}
