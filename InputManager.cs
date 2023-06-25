using UnityEngine;

namespace FirstPersonControllerKit
{
    public class InputManager : PersistentSingletonMonobehaviour<InputManager>
    {
        private StandardInputActions playerControlInput;

        public override void Awake()
        {
            base.Awake();
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

        public Vector2 GetPlayerMovement() => playerControlInput.Player.Movement.ReadValue<Vector2>();
        public Vector2 GetMouseDelta() => playerControlInput.Player.Look.ReadValue<Vector2>();
        public bool HasPlayerJumped() => playerControlInput.Player.Jump.triggered;
    }
}
