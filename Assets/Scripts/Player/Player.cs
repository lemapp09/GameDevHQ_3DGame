using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LemApperson_3DGame.Player
{
    public class Player : MonoBehaviour
    {
        private InputActions _inputs;
        private bool _isGrounded;

        [SerializeField] private float _jumpHeight = 1.0f, _playerSpeed = 2.0f,
            _gravityValue = -9.81f, _rotationSpeed = 2.0f,
            maxAngle = 20f;
        private Vector3 _playerVelocity;
        private Quaternion center;
        [SerializeField] private CharacterController _charCntrlr;
        [SerializeField] private Transform _camera;
        [SerializeField] private float _cameraSensitivity;

        private void Awake() {
            if (GetComponent<CharacterController>() != null) {
                _charCntrlr = GetComponent<CharacterController>();
            }
            if(_camera == null) {
                _camera =  GetComponentInChildren<Camera>().transform;
            }
            this.center = _camera.rotation;
            _inputs = new InputActions();
            _inputs.Player.Jump.performed += Jump;
            _inputs.Player.Quit.performed += UnlockCursor;
        }

        private void LateUpdate() { 
            CalculateMovement();
            CameraController();
        }

        private void CalculateMovement(){
            Vector2 input = _inputs.Player.Move.ReadValue<Vector2>();
            _playerVelocity = new Vector3(input.x, _playerVelocity.y, input.y);
            _playerVelocity.y += _gravityValue * Time.deltaTime;
            if (_charCntrlr.isGrounded && _playerVelocity.y < 0) {
                _playerVelocity.y = 0f;
            }
            _playerVelocity = transform.TransformDirection(_playerVelocity);
            _charCntrlr.Move(_playerSpeed * Time.deltaTime * _playerVelocity);
            
            if (input != Vector2.zero) {
                Quaternion toRotation = Quaternion.LookRotation(
                    new Vector3(_playerVelocity.x, 0, _playerVelocity.z), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,
                    _rotationSpeed * Time.deltaTime);            
            }
        }

        private void CameraController() {
            Vector2 cameraInput =  _cameraSensitivity * _inputs.Camera.Look.ReadValue<Vector2>();
            _camera.Rotate(Vector3.up, cameraInput.x * _cameraSensitivity * Time.deltaTime); 
            _camera.Rotate(Vector3.right, cameraInput.y * _cameraSensitivity * Time.deltaTime);
        }
        
        private void Jump(InputAction.CallbackContext obj) {
            _playerVelocity.y = _jumpHeight;
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