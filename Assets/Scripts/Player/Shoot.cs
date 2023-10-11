using LemApperson_3DGame.Manager;
using UnityEngine;

namespace LemApperson_3DGame.Player
{
    public class Shoot : MonoBehaviour
    {
        private Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _bloodSplatter;
        private InputActions _inputActions;

        public void ShootGun() {
            Ray ray = _camera.ViewportPointToRay(rayOrigin);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy"))) {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    AudioManager.Instance.SFX(1);
                    if (hit.collider.GetComponent<Target>() != null) {
                        hit.collider.gameObject.GetComponent<Target>().ChangeColor();
                    }
                    Health health = hit.collider.gameObject.GetComponent<Health>();
                    if (health != null) {
                        health.Damage(50);
                        GameObject bloodSplatter = Instantiate(_bloodSplatter, hit.point, Quaternion.LookRotation(hit.normal));
                        bloodSplatter.transform.parent = hit.collider.gameObject.transform;
                    }
                }
            }
        }

        private void Awake() {
            _inputActions = new InputActions();
            _inputActions.Player.Shoot.performed += ctx => ShootGun();
        }

        private void OnEnable() {
            _inputActions.Player.Shoot.Enable();
        }

        private void OnDisable() {
            _inputActions.Player.Shoot.Disable();
        }
    }
}