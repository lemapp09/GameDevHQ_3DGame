using UnityEngine;
using UnityEngine.InputSystem;

namespace LemApperson_3DGame.Player
{
    public class Player : MonoBehaviour
    {
        private InputActions _inputs;
        private bool _isGrounded;
        [SerializeField] private float _jumpHeight = 1.0f, _playerSpeed = 2.0f, _gravityValue = -9.81f;
        private Vector3 _playerVelocity;
        [SerializeField] private CharacterController _charCntrlr;

        private void Awake() {
            if (GetComponent<CharacterController>() != null) {
                _charCntrlr = GetComponent<CharacterController>();
            }
            _inputs = new InputActions();
            _inputs.Player.Enable();
            _inputs.Player.Jump.performed += ctx => Jump(ctx);
        }

        private void Update() {
            _isGrounded = _charCntrlr.isGrounded;
            if (_isGrounded && _playerVelocity.y < 0) {
                _playerVelocity.y = 0f;
            }
            Vector3 move = new Vector3(_inputs.Player.Move.ReadValue<Vector2>().x, 0,
                   _inputs.Player.Move.ReadValue<Vector2>().y);
            _charCntrlr.Move(Time.deltaTime * _playerSpeed * move);
            if (move != Vector3.zero) {
                gameObject.transform.forward = move;
            }
            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _charCntrlr.Move(_playerVelocity * Time.deltaTime);
        }

        private void Jump(InputAction.CallbackContext context) {
            // Changes the height position of the player..
            if ( _isGrounded) {
                _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * _gravityValue);
            }
        }
    }
}