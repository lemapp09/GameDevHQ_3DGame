using UnityEngine;

namespace LemApperson_3DGame
{
    public class DeleteBloodSplatter : MonoBehaviour 
    { 
        private void Start() {
            Destroy(gameObject, 1.1f);
        }
    }
}