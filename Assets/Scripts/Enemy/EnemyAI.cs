using UnityEngine;

namespace LemApperson_3DGame.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        public enum EnemyState {
                Idle,
                Attack,
                Patrol,
                Chase,
                Dead
        }
        [SerializeField] private CharacterController _cc;
        [SerializeField] private float _gravityValue = -9.81f, _enemySpeed = 2;
        private Transform _player;
        private Health _playerHealth;
        [SerializeField] private float _attackDelay = 1.5f;
        private float _nextAttack;
        private  Vector3 _enemyVelocity;
        [SerializeField] private EnemyState _currentState = EnemyState.Chase;

        private void Awake() {
            if (_cc == null) {
              _cc = GetComponent<CharacterController>();  
            }
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _playerHealth = _player.GetComponent<Health>();
            if (_player == null || _playerHealth == null) {
                Debug.LogError("Player or PlayerHealth is null");
            }
        }

        private void LateUpdate() {
            if(transform.position.y < -10f) Destroy(gameObject);
            switch (_currentState)
            {
                case EnemyState.Idle:
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Patrol:
                    break;
                case EnemyState.Chase:
                    CalculateMovement();
                    break;
                case EnemyState.Dead:
                    break;
                default:
                    break;
            }
        }

        private void Attack()
        {
            if(Time.time > _nextAttack) {
                _nextAttack = Time.time + _attackDelay;
                if(_playerHealth != null){
                    _playerHealth.Damage(10);
                }
            }
        }

        private void CalculateMovement()
        {
            if (_cc.isGrounded) {
                Vector3 direction = (_player.position - transform.position).normalized;
                transform.localRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
                _enemyVelocity = direction * _enemySpeed;
            }
            _enemyVelocity.y += _gravityValue * Time.deltaTime;
            _cc.Move(_enemyVelocity * Time.deltaTime);
        }

        public void ChangeState(EnemyState newState) {
            _currentState = newState;
        }
    }
}