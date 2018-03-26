using UnityEngine.UI;
using UnityEngine;
using System;


namespace Labo{
    public class LogView : MonoBehaviour{
        // static だとインスペクタに表示されないのでワンクッション入れる。
        [SerializeField] private bool AlsoUseDebugLog = false;
        [SerializeField] private Toggle autoScrollToggle;
        [SerializeField] private Text logText;
        [SerializeField] private ScrollRect scrollRect;

        private static Toggle toggle;
        private static Text log;
        private static ScrollRect sRect;
        private static bool alsoUseDebugLog = false;

        private static readonly float bottomThreshold = 0.00001f;
        private static bool scrollRequest = false;


        private void Awake() {
            toggle = autoScrollToggle;
            log = logText;
            sRect = scrollRect;
            alsoUseDebugLog = AlsoUseDebugLog;
            LogView.Log("LogView is ready.");
        }
        

        private void Update() {
            if (scrollRequest == false) { return; }
            if (sRect == null) { return; }

            // scrollRequest が true かつ、まだ最下部までスクロールしていないと判断された場合のみ、スクロールを行う。
            if (sRect.verticalNormalizedPosition > bottomThreshold) {
                sRect.verticalNormalizedPosition = 0;
                scrollRequest = false;
            }
        }


        /// <summary>
        /// スクロールビューにログを書き出す。
        /// </summary>
        /// <param name="text"></param>
        public static void Log(string text) {
            if (log == null) { return; }
            // 改行->テキスト の順だと自動スクロールの見た目がスムーズになる。
            log.text += Environment.NewLine;
            log.text += text;
            UseDebugLog(text);
            AutoScroll();
        }


        private static void UseDebugLog(string text) {
            if (alsoUseDebugLog == false) { return; }
            Debug.Log(text);
        }


        private static void AutoScroll() {
            if (toggle == null) { return; }
            if (toggle.isOn == false) { return; }
            // AutoScroll のトグルボタンがONの場合、最下部までスクロールする。
            // 瞬時に最下部へスクロールしようとするとうまくいかないようなので、わざと回りくどいやり方をしている。
            scrollRequest = true;
        }
    }
}