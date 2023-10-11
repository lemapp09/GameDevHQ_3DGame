using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


namespace LemApperson_3DGame.Player
{
    public class Player : MonoBehaviour
    {
        private InputActions _inputs;
        [SerializeField] private CharacterController _charCntrlr;
        [SerializeField] private Animator _animator;
        private int _speedID, _deathID, _idleXID;
        [SerializeField] private Transform _cameraAimPoint;
        [SerializeField] private float _speed = 2, _cameraSensitivity = 2;
        private bool _isDead;

        private void Awake() {
            _inputs = new InputActions();
            _inputs.Player.Quit.performed += UnlockCursor;
            _animator = GetComponentInChildren<Animator>();
            StartCoroutine(SwitchIdle());
            _charCntrlr = GetComponent<CharacterController>();
            _speedID = Animator.StringToHash("Speed");
            _deathID = Animator.StringToHash("Death");
            _idleXID = Animator.StringToHash("IdleX");
        }

        private void Update()
        {
            _speed = _charCntrlr.velocity.magnitude;
            _animator.SetFloat(_speedID, _speed);
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

        public IEnumerator  SwitchIdle() {
            while (!_isDead) {
                float randomValue = Random.Range(0, 4) * 0.33f;
                _animator.SetFloat(_idleXID, randomValue);
                yield return new WaitForSeconds(2.5f);
            }
        }

        public void OnAnimationStateEntered(float stateInfoLength)
        {
            throw new System.NotImplementedException();
        }

        public void OnAnimationStateExited(int stateInfoShortNameHash)
        {
            throw new System.NotImplementedException();
        }
    }
}