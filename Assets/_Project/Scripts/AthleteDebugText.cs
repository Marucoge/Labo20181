using System.Collections.Generic;
using UnityEngine.UI;
//using UnityEditor;
using UnityEngine;
using System;
using System.Text;


namespace Labo{
    public class AthleteDebugText : MonoBehaviour{
        [SerializeField] private Text DebugText;
        [SerializeField] private GroundingDetector3D detector;
        private StringBuilder builder;


        private void Start() {
            builder = new StringBuilder();
            builder.Capacity = 512;
        }


        private void Update() {
            if(DebugText == null) { return; }
            if (detector == null) { return; }
            builder.Length = 0;     // 文字列のリセット。

            InformAboutGrounding();

            DebugText.text = builder.ToString();
        }


        private void InformAboutGrounding() {
            builder.Append("Grounding: ");
            builder.Append(detector.IsGrounding);

            if (detector.DetectedGround == null) {
                builder.Append(" on null");
                AppendNewLine();
                return;
            }

            // 接地している場合。
            // GameObjectをそのまま渡すと引数として bool が渡されたことになってしまう。対処方法がわからない。
            builder.Append(" on ");
            builder.Append(detector.DetectedGround.ToString());
            AppendNewLine();
        }


        private void AppendNewLine() {
            builder.Append(Environment.NewLine);
        }
    }
}