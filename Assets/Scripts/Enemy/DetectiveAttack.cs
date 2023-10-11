using UnityEngine;

namespace LemApperson_3DGame.Enemy
{
    public class DetectiveAttack : MonoBehaviour
    {
        private EnemyAI _enemyAI;

        private void Start()
        {
            _enemyAI = GetComponentInParent<EnemyAI>();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.CompareTag("Player")) {
                if (_enemyAI != null) {
                    _enemyAI.ChangeState(EnemyAI.EnemyState.Attack);
                }
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.CompareTag("Enemy")) {
                if (_enemyAI != null) {
                    _enemyAI.ChangeState(EnemyAI.EnemyState.Chase);
                }
            }
        }
    }
}