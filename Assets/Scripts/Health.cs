using UnityEngine;

namespace LemApperson_3DGame
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health, _maxHealth,  _minHealth, _healDelay,  _healAmount;
        private float _healTime = -1;

        private void Start() {
            _health = _maxHealth;
        }

        private void Update() {
            Heal();
        }

        private void Heal() {
            if (Time.time > _healTime) {
                _healTime = Time.time + _healDelay;
                _health += _healAmount;
                if (_health > _maxHealth) {
                    _health = _maxHealth;
                }
            }
        }

        public void Damage(int damageAmount) {
            _health -= damageAmount;
            if (_health <= _minHealth) {
                Destroy(gameObject);
            }
        }
    }
}