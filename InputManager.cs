using UnityEngine;

namespace FirstPersonControllerKit
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        private StandardInputActions playerControlInput;

        public void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else 
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            
            playerControlInput = new StandardInputActions();
        }

        private void OnEnable()
        {
            playerControlInput.Enable();
        }

        private void OnDisable()
        {
            playerControlInput.Disable();
        }

        public Vector2 GetWASDInput() => playerControlInput.Player.Movement.ReadValue<Vector2>();
        public Vector2 GetMouseInput() => playerControlInput.Player.Look.ReadValue<Vector2>();
        public bool GetSpaceInput() => playerControlInput.Player.Jump.triggered;
    }
}
