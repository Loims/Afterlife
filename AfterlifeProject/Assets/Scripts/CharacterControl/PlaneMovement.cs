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
        if (movementComp.playerState == PlayerMovement.State.WHALE)
        {
            moveSpeed = 8f;
        }
        else if (movementComp.playerState == PlayerMovement.State.PLANE)
        {
            moveSpeed = 30f;
        }
        else if (movementComp.playerState == PlayerMovement.State.FLARE)
        {
            moveSpeed = 15f;
        }
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    public void ResetPlane()
    {
        transform.position = Vector3.zero;
    }
}
