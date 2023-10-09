using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LemApperson_3DGame
{
    public class Target : MonoBehaviour
    {
        public void ChangeColor() {
            GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
    }
}