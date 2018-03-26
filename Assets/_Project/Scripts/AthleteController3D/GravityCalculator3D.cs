using System.Collections.Generic;
using UnityEngine;
using System;


namespace Labo{
    /// <summary>
    /// 重力による下方向への移動量を(簡易に)計算するクラス。AthleteController3D で使う。
    /// </summary>
    public class GravityCalculator3D : IMovementCalculator3D {
        public Vector3 MovementPerFrame { get; private set; }
        public float FloatingTime { get; private set; }
        public float GravityAccel { get; private set; }
        public float MaximumSpeed { get; private set; }
        //public float ConstantGravity { get; private set; }
        private IGroundingDetector3D detector;


        public GravityCalculator3D(IGroundingDetector3D gDetector) {
            this.detector = gDetector;
            FloatingTime = 0.00f;
            GravityAccel = 10.00f;      // 標準重力加速度(1G)はおよそ 9.8 m/s2  (加速度の単位であることに注意。1秒につき速度が 9.8m/s速くなるような加速をあらわす。)
            MaximumSpeed = 100f;
        }

        
        public void ManualUpdate() {
            // 接地していたらフィールドをリセットして return。
            if (detector.IsGrounding) {
                MovementPerFrame = Vector3.zero;
                FloatingTime = 0.00f;
                return;
            }

            // 接地していない場合、移動量を計算する。
            FloatingTime += Time.deltaTime;
            float currentSpeed = GravityAccel * FloatingTime;                               // 加速度*経過時間=この時点での速度
            float restrictedSpeed = Mathf.Min(currentSpeed, MaximumSpeed);      // 最大速度を制限する。
            MovementPerFrame =  restrictedSpeed * Vector3.down;                     // MovementPerFrame を合計するときに秒あたりの移動量に直すので、ここではフレームあたりのでよい。
        }
    }
}