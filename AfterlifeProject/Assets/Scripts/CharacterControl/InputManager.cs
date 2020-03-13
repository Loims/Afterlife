using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Afterlife.InputManager
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;

        public InputData player;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public InputData GetInputData()
        {
            return player;
        }

        private void Update()
        {
            if (player != null)
            {
                player.PollInput();
            }
        }
    }

    public class InputData
    {
        public InputType verticalInput;
        public InputType horizontalInput;

        public void PollInput()
        {
            verticalInput.Update(Input.GetAxis(verticalInput.axis));
            horizontalInput.Update(Input.GetAxis(horizontalInput.axis));
        }

        [System.Serializable]
        public struct InputType
        {
            public string axis;
            public bool pressed;
            public float value;
            public float lastValue;
            public float timePressed;
            public float timeBeingPressed;

            public void SetAxis(string _key)
            {
                axis = _key;
            }

            public void Update(float _pressed)
            {
                lastValue = value;
                value = _pressed;
                pressed = _pressed != 0 ? true : false;

                if (pressed)
                {
                    timePressed = Time.time;
                    timeBeingPressed = Time.time - timePressed;
                }
                else
                {
                    timePressed = 0;
                    timeBeingPressed = 0;
                }
            }
        }
    }

    
}
