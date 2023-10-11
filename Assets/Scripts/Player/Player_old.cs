using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

namespace LemApperson_3DGame.Player
{
    public class Player_old : MonoBehaviour
    {
        private InputActions _inputs;
        private bool  _hasJumped;

        [SerializeField] private float _jumpHeight = 4.0f,
            _playerSpeed = 2.0f,
            _gravityValue = -9.81f;
        private Vector3 _playerVelocity;
        [SerializeField] private CharacterController _charCntrlr;
        [SerializeField] private Transform _cameraAimPoint;
        [SerializeField] private float _cameraSensitivity, _rotationSpeed;

        private void Awake() {
            if (GetComponent<CharacterController>() != null) {
                _charCntrlr = GetComponent<CharacterController>();
            }
            _inputs = new InputActions();
            _inputs.Player.Jump.performed += Jump;
            _inputs.Player.Quit.performed += UnlockCursor;
        }

        private void LateUpdate() { 
            CalculateMovement();
            CameraController();
        }

        private void CalculateMovement() 
        {
            Vector2 input = _inputs.Player.Move.ReadValue<Vector2>();
            this.transform.Rotate(new Vector3(0, input.x * _rotationSpeed * Time.deltaTime, 0));
            if (_charCntrlr.isGrounded ) {
                Vector3 direction = new Vector3(input.x, 0, input.y);
                _playerVelocity = direction * _playerSpeed;
                _playerVelocity = transform.TransformDirection(_playerVelocity);
                if (_hasJumped) {
                    _playerVelocity.y = _jumpHeight;
                    _hasJumped = false;
                } 
            }
            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _charCntrlr.Move( Time.deltaTime * _playerVelocity);
        }


        private void CameraController() {
            Vector2 cameraInput = Time.deltaTime *_cameraSensitivity * _inputs.Camera.Look.ReadValue<Vector2>() ;
            float AimX = cameraInput.x + _cameraAimPoint.transform.localPosition.x;
            float AimY = cameraInput.y + _cameraAimPoint.transform.localPosition.y;
            if (AimX < -3) {
                AimX = -3; 
            } else if (AimX > 3) {
                AimX = 3;
            }
            if (AimY < 0) {
                AimY = 0; 
            } else if (AimY > 3) {
                AimY = 3;
            }
            _cameraAimPoint.transform.localPosition = new Vector3(AimX, AimY, 0);
        }
        
        private void Jump(InputAction.CallbackContext obj) {
            _hasJumped = true;
        }
        
        private void OnEnable() {
            _inputs.Enable();
            UnityEngine.Cursor.visible = false;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }

        private void UnlockCursor(InputAction.CallbackContext obj) {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        private void OnDisable() {
            _inputs.Disable();
        }
    }
}