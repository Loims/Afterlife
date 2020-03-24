using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float moveSpeed;

    private void OnEnable()
    {
        moveSpeed = 10f;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.Q))
        {
            ResetPlane();
        }
    }

    public void ResetPlane()
    {
        transform.position = Vector3.zero;
    }
}
