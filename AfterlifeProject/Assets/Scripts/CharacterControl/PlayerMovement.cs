using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum State
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
        if(playerState == State.NULL)
        {
            playerState = State.WHALE;
            ChangeStateData(playerState);
        }
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

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
            if (xyspeed != 15)
            {
                xyspeed = 15;
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
}
