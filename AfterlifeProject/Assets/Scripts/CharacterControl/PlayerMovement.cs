using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum State
    {
        NULL,
        WHALE,
        PLANE,
        FLARE
    }

    [SerializeField] private State playerState;

    public float xyspeed = 8f;

    private float objRotationX;
    private float objRotationY;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if(playerState == State.NULL)
        {
            playerState = State.WHALE;
            ChangeStateData(playerState);
        }
    }

    private void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        LocalMove(h, v, xyspeed);
        ClampPosition();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            playerState += 1;
            ChangeStateData(playerState);
        }
    }
    
    private void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
    }

    private void ClampPosition()
    {
        float xClamp = Screen.width / 8;
        float yClamp = Screen.height / 8;

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, Screen.width - (Screen.width - xClamp), Screen.width - xClamp);
        pos.y = Mathf.Clamp(pos.y, Screen.height - (Screen.height - yClamp), Screen.height - yClamp);
        transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    private void ChangeStateData(State state)
    {
        playerState = state;

        if(state == State.WHALE)
        {
            if(xyspeed != 8)
            {
                xyspeed = 8;
            }
        }

        if (state == State.PLANE)
        {
            if (xyspeed != 12)
            {
                xyspeed = 12;
            }
        }

        if (state == State.FLARE)
        {
            if (xyspeed != 10)
            {
                xyspeed = 10;
            }
        }
    }

    public State GetCurrentState()
    {
        return playerState;
    }
}
