using UnityEngine;

namespace LemApperson_3DGame.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private CharacterController _cc;
        [SerializeField] private float _gravityValue = -9.81f, _enemySpeed = 2;
        private Transform _player;
        private  Vector3 _enemyVelocity;

        private void Awake() {
            if (_cc == null) {
              _cc = GetComponent<CharacterController>();  
            }
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate() {
            if(transform.position.y < -10f) Destroy(gameObject);
            if (_cc.isGrounded) {
                Vector3 direction = (_player.position - transform.position).normalized;
                transform.localRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
                _enemyVelocity = direction * _enemySpeed;
            }
            _enemyVelocity.y += _gravityValue * Time.deltaTime;
            _cc.Move(_enemyVelocity * Time.deltaTime);
        }
    }
}