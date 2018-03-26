using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;


namespace VirtualUI{
    public class StickHead : MonoBehaviour {
        [SerializeField] private RectTransform StickHeadImage;
        [SerializeField] private Text DebugText;
        [SerializeField] private bool EnableStickHead = true;
        [SerializeField] private bool EnableDebugText = true;

        private float StickRangeRadius;
        private VirtualStick stick;         // インターフェイスを型にすると、public や [SerializeField]などでもインスペクタに表示することはできない。

        
        private void Start() {
            stick = GetComponent<VirtualStick>();
            var rect = GetComponent<RectTransform>();
            StickRangeRadius = rect.sizeDelta.x / 2;
        }


        private void Update() {
            if (StickHeadImage == null) { return; }
            if (DebugText == null) { return; }
            if (StickRangeRadius < 0.1f) { return; }

            if (EnableStickHead) {
                StickHeadImage.anchoredPosition = stick.StickInput * StickRangeRadius;
            }

            if (EnableDebugText) {
                DebugText.text = stick.StickInput.ToString();
            }
        }
    }
}