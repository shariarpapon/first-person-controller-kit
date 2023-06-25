using UnityEngine;

namespace FirstPersonControllerKit
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {

        /// <summary>
        /// Player speed should always be aquired through PlayerMoveSpeed variable instead.
        /// </summary>
        [SerializeField]
        private float walkSpeed = 5f;
        [SerializeField, Range(1, 3.5f)]
        private float sprintSpeedFactor = 1.13f;
        [SerializeField]
        private float jumpHeight = 1.0f;
        [SerializeField]
        private float gravityValue = -9.81f;
        [SerializeField]
        private bool lockCursor;

        [Space, SerializeField]
        private Camera playerCamera;

        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;

        /// <summary>
        /// Player speed should always be aquired through this.
        /// </summary>
        public float PlayerMoveSpeed
        {
            get
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    return walkSpeed * sprintSpeedFactor;
                }
                return walkSpeed;
            }
        }

        private InputManager inputManager { get { return InputManager.Instance; } }

        private void Start()
        {
            if (lockCursor)
                Cursor.lockState = CursorLockMode.Locked;

            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            LateralMovement();
            VerticalMovement();
        }

        private void LateralMovement()
        {
            Vector2 lateralInput = inputManager.GetWASDInput().normalized;
            float angle = Vector3.SignedAngle(Vector3.forward, playerCamera.transform.forward, Vector3.up);
            Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 motionVector = rotation * new Vector3(lateralInput.x, 0, lateralInput.y);
            controller.Move(motionVector * Time.deltaTime * PlayerMoveSpeed);
        }

        private void VerticalMovement()
        {
            if (inputManager.GetSpaceInput() && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
}