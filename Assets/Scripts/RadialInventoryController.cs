using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace LemApperson_3DGame
{
    public class RadialInventoryController : MonoBehaviour
    {
        [SerializeField] private Image _radialMenu;
        [SerializeField] private  Image[] _slot;
        private InputActions _inputActions;
        private DefaultInputActions _defaultInputActions;
        private bool _radialMenuActive;
        private int _previousSlot;

        private void Awake() {
            _radialMenu.enabled = false;
            _inputActions = new InputActions();
            _defaultInputActions = new DefaultInputActions();
            _inputActions.Menu.Enable();
            _inputActions.Menu.ToggleMenu.performed += ToggleRadialMenu;
        }

        private void Update()
        {
            if (_radialMenuActive)
            {
                // calculate the difference from the center point of the circle to my mouse position
                Vector2 mousePos = _defaultInputActions.UI.Point.ReadValue<Vector2>();
                Vector2 delta = new Vector2(mousePos.x - _radialMenu.transform.position.x, mousePos.y - _radialMenu.transform.position.z);
                var angle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
                angle += 180f;
                int slot = (int)(angle / 30);
                slot = Mathf.Clamp(slot, 0, 11);
                if (slot != _previousSlot) {
                    Debug.Log(slot);
                    for (int i = 0; i < _slot.Length; i++) {
                        _slot[i].enabled = false;
                    }
                    _previousSlot = slot;
                    _slot[slot].enabled = true;
                }
            }
        }

        private void ToggleRadialMenu(InputAction.CallbackContext obj) {
            _radialMenu.enabled = _radialMenuActive;
            _radialMenuActive = !_radialMenuActive;
        }

        private void OnEnable() {
            _inputActions.Menu.Enable();
            _defaultInputActions.Enable();
        }
        
        private void OnDisable() {
            _inputActions.Menu.Disable();
            _defaultInputActions.Disable();
        }
    }
}
