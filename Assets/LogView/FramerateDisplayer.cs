using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEditor;
using UnityEngine;
using System;


namespace Labo{
    public class FramerateDisplayer : MonoBehaviour{
        [SerializeField] private Text Display;
        private float lastUpdatedTime = 0;
        private int numberOfFrames = 0;

        private void Update() {
            if (Display == null) { return; }

            // フレームレートを計算する方法として考えられるのは、
            // Time.deltaTime の逆数を計算する方法と、実際にフレーム数を数える方法。ここでは後者を用いる。
            numberOfFrames++;
            float time = Time.time - lastUpdatedTime;

            if (time < 1) { return; }
            Display.text = numberOfFrames.ToString();
            numberOfFrames = 0;
            lastUpdatedTime = Time.time;
        }
    }
}