using UnityEngine;

namespace LemApperson_3DGame
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health, _maxHealth,  _minHealth;

        private void Start() {
            _health = _maxHealth;
        }
        
        public void Damage(int damageAmount) {
            _health -= damageAmount;
            if (_health <= _minHealth) {
                Destroy(gameObject);
            }
        }
    }
}