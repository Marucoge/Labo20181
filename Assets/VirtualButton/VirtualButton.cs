using System.Collections.Generic;
//using UnityEngine.UI;
//using UnityEditor;
using UnityEngine;
using System;


namespace Labo{
    public class VirtualButton : MonoBehaviour{
        public bool IsPressed { get; private set; }

        public void Press() {
            IsPressed = true;
        }

        public void Release() {
            IsPressed = false;
        }
    }
}