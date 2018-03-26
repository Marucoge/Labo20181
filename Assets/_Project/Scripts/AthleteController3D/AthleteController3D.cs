using System.Collections.Generic;
using UnityEngine;
using System;
using VirtualUI;


namespace Labo {
    /// <summary>
    /// 移動量を計算するクラスのためのインターフェイス。
    /// </summary>
    public interface IMovementCalculator3D {
        /// <summary>
        /// AthleteController3D でこの値を合計する。最後に Time.deltaTime 掛けて秒あたりの移動量に変換するので、これはフレームあたりのでよい。
        /// </summary>
        Vector3 MovementPerFrame { get; }
        /// <summary>
        /// AthleteController3D で毎フレーム呼び出す。
        /// </summary>
        void ManualUpdate();
    }



    public class AthleteController3D : MonoBehaviour {
        [SerializeField] private VirtualStick VirtualStickL;
        [SerializeField] private VirtualStick VirtualStickR;
        [SerializeField] private GameObject face;

        public Vector3 TotalMovementPerSecond { get; private set; }

        private List<IMovementCalculator3D> calculators;
        private CharacterController character;
        private IGroundingDetector3D detector;
        private CameraAngleCalculator3D cameraman;


        private void Start() {
            character = GetComponent<CharacterController>();
            detector = GetComponent<IGroundingDetector3D>();
            cameraman = new CameraAngleCalculator3D(face, this.gameObject, VirtualStickR);

            // 移動量を計算するクラスをそれぞれここで登録する。
            calculators = new List<IMovementCalculator3D>();
            calculators.Add(new GravityCalculator3D(detector));
            calculators.Add(new WalkCalculator3D(this.gameObject, VirtualStickL));
        }


        private void Update() {
            TotalMovementPerSecond = Vector3.zero;
            cameraman.Rotate();     // タイミングは深く考えていない。

            // 移動量を計算するクラスでManualUpdate を呼び、計算した移動量を合計する。毎フレーム。
            foreach (IMovementCalculator3D element in calculators) {
                element.ManualUpdate();
                TotalMovementPerSecond += element.MovementPerFrame;
            }

            // ワールド座標で移動する。Translate では CharacterController が傾斜に沿って歩いてくれない。
            //transform.Translate(TotalMovement * Time.deltaTime, Space.World);
            TotalMovementPerSecond *= Time.deltaTime;   // フレームあたりの速度を秒あたりの速度に変換
            character.Move(TotalMovementPerSecond);
        }
    }
}
