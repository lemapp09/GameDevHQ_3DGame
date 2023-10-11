using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


namespace LemApperson_3DGame.Player
{
    public class Player : MonoBehaviour
    {
        private InputActions _inputs;
        [SerializeField] private CharacterController _charCntrlr;
        [SerializeField] private Animator _animator;
        private int _speedID, _deathID, _idleID, _walkID, _runningID; 
        [SerializeField] private Transform _cameraAimPoint;
        [SerializeField] private float _speed = 2, _cameraSensitivity = 2;
        private bool _isDead;

        private void Awake() {
            _inputs = new InputActions();
            _inputs.Player.Quit.performed += UnlockCursor;
            _animator = GetComponentInChildren<Animator>();
            _charCntrlr = GetComponent<CharacterController>();
            _speedID = Animator.StringToHash("Speed");
            _deathID = Animator.StringToHash("Death");
            _idleID = Animator.StringToHash("Idle");
            _walkID = Animator.StringToHash("Walk");
            _runningID = Animator.StringToHash("Running");
        }

        private void Update()
        {
            var velocity = _charCntrlr.velocity;
            _animator.SetFloat(_speedID, velocity.magnitude);
            CameraController();
        }       
        private void CameraController() {
            Vector2 cameraInput = Time.deltaTime *_cameraSensitivity * _inputs.Camera.Look.ReadValue<Vector2>() ;
            float AimX = cameraInput.x + _cameraAimPoint.transform.localPosition.x;
            float AimY = cameraInput.y + _cameraAimPoint.transform.localPosition.y;
            if (AimX < -1) {
                AimX = -1; 
            } else if (AimX > 1) {
                AimX = 1;
            }
            if (AimY < 1) {
                AimY = 1; 
            } else if (AimY > 3) {
                AimY = 3;
            }
            _cameraAimPoint.transform.localPosition = new Vector3(AimX, AimY, 0);
        }

        private void UnlockCursor(InputAction.CallbackContext obj)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        private void Death()
        {
            _isDead = true;
            _animator.SetTrigger(_deathID);
        }

        private void OnEnable() {
            _inputs.Enable();
        }

        private void OnDisable() {   
            _inputs.Disable();
        }

        public void OnAnimationStateEntered(int NameHash)
        {
            if (NameHash == _deathID) {
                
            } else if (NameHash == _idleID) {
                
            }  else if (NameHash == _walkID) {
                
            }  else if (NameHash == _runningID) {
                
            }
        }

        public void OnAnimationStateExited(int NameHash)
        {
            if (NameHash == _deathID) {
                
            } else if (NameHash == _idleID) {
                
            }  else if (NameHash == _walkID) {
                
            }  else if (NameHash == _runningID) {
                
            } 
        }
    }
}