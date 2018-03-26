using System.Collections.Generic;
using UnityEngine;
using System;

namespace Labo{
    /// <summary>
    /// 単振動の動きを実装する。Rigidbody無しのColliderを動かすのは誤作動を招く恐れがあるので
    /// Colliderつきで動かす場合はRigidbodyをKinematicとして取り付ける必要があることに注意。
    /// 試験用に作ったスクリプトであるためfloatの誤差や、Time.timeが巨大な数値になった時のことは考えていない。
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]   // 注:物理挙動を使わない場合でも必要。Kinematicとして使う。
    public class SimpleHarmonicMotion : MonoBehaviour {
        /// <summary>
        /// 運動の中心となる座標。Transformの座標をインスペクタからいじってもこのスクリプトは書き換えてしまう。
        /// </summary>
        [SerializeField] private Vector3 CenterPosition = Vector3.zero;

        /// <summary>
        /// 運動の速さ。一秒あたりの振動数(frequency)...?
        /// </summary>
        [SerializeField] private Vector3 Speed = Vector3.one;

        /// <summary>
        /// 振幅(amplitude)。
        /// </summary>
        [SerializeField] private Vector3 Scale = new Vector3(5, 0, 5);
        
        [SerializeField] private bool XDelays = false;
        [SerializeField] private bool YDelays = false;
        [SerializeField] private bool ZDelays = true;

        private float delayer = 1.50f;


        private void Update() {
            // System.Timers.timer を使う方法は不正確+奇妙な挙動が見られたのでやめておく。
            
            // Delays がTrue なら周期を半分ずらす。"半分"はFrequencyの値によって変化する。
            // (ゼロ除算のような)極めて小さい数字による除算が起きそうな場合、周期をずらす計算はしない。
            float xDelay = 0;
            float yDelay = 0;
            float zDelay = 0;
            bool isXLargeEnough = Mathf.Abs(Speed.x) > 0.001f;
            bool isYLargeEnough = Mathf.Abs(Speed.y) > 0.001f;
            bool isZLargeEnough = Mathf.Abs(Speed.z) > 0.001f;

            if (XDelays && isXLargeEnough) {  xDelay = delayer / Speed.x; }
            if (YDelays && isYLargeEnough) {  yDelay = delayer / Speed.y; }
            if (ZDelays && isZLargeEnough) {  zDelay = delayer / Speed.z; }

            Vector3 delayedTime = new Vector3(
                Time.time - xDelay,
                Time.time - yDelay,
                Time.time - zDelay
                );

            Vector3 sin = new Vector3(
                Mathf.Sin(delayedTime.x * Speed.x),
                Mathf.Sin(delayedTime.y * Speed.y),
                Mathf.Sin(delayedTime.z * Speed.z)
                );

            Vector3 scaledPosition = new Vector3(
                sin.x * Scale.x,
                sin.y * Scale.y,
                sin.z * Scale.z
                );

            // Translateにもできるだろうけど面倒なのでこのまま。
            transform.localPosition = CenterPosition + scaledPosition;
        }
    }
}