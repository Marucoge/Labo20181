using System.Collections.Generic;
using UnityEngine;
using System;
using VirtualUI;


namespace Labo{
    public class CameraAngleCalculator3D {
        private GameObject face;
        private GameObject body;
        private VirtualStick stickR;
        private float upwardCompressStartAngle = 300.0f;
        private float downwardCompressStartAngle = 60.0f;
        private float verticalLimitEuler = 80.0f;
        public Vector2 Sensitivity { get; set; }


        public CameraAngleCalculator3D(GameObject faceObject, GameObject bodyObject, VirtualStick virtualStickR) {
            this.face = faceObject;
            this.body = bodyObject;
            //this.stick = stickIntegrator;
            this.stickR = virtualStickR;
            Sensitivity = Vector2.one * 3f;
        }

        
        public void Rotate() {
            float currentAngle = face.transform.eulerAngles.x;
            //float verticalRotation = -stick.RightInput.y * Sensitivity.y;
            //float horizontalRotation = stick.RightInput.x * Sensitivity.x;
            float verticalRotation =  stickR.StickInput.y * Sensitivity.y * -1;
            float horizontalRotation = stickR.StickInput.x * Sensitivity.x;


            // 現在の可動範囲 = 限界値 - 現在の角度
            // min(入力値, 現在の可動範囲) // 入力値が限界値までの残りの角度を超えると、残りの角度ぶんだけ回転する。
            verticalRotation = CompressUpwardRotation(verticalRotation, currentAngle);
            verticalRotation = CompressDownwardRotation(verticalRotation, currentAngle);

            AntiTiltRotate(body, horizontalRotation, 0);
            AntiTiltRotate(face, 0, verticalRotation);
        }


        private float CompressUpwardRotation(float rotateAngle, float currentAngle) {
            if (currentAngle > upwardCompressStartAngle) { return rotateAngle; } // 上方向を向いている場合でも、限界角度から遠い場合は無視する。
            if (currentAngle < 180) { return rotateAngle; }                                   // 真正面より上方向を向いていない。
            if (rotateAngle > 0) { return rotateAngle; }                                       // 上方向の回転ではない場合。

            currentAngle = 360f - currentAngle;   // 現在の角度を、真正面から上方向への角度に変換。
            rotateAngle = -rotateAngle;             // 回転量の絶対値
            float rotatableAngle = verticalLimitEuler - currentAngle;
            rotateAngle = Mathf.Min(rotatableAngle, rotateAngle);
            rotateAngle = -rotateAngle;
            return rotateAngle;
        }


        private float CompressDownwardRotation(float rotateAngle, float currentAngle) {
            if (currentAngle < downwardCompressStartAngle) { return rotateAngle; }      // 下方向を向いている場合でも、限界角度から遠い場合は無視する。
            if (currentAngle > 180) { return rotateAngle; }                                            // 真正面より下方向を向いていない。
            if (rotateAngle < 0) { return rotateAngle; }                                                // 下方向への回転ではない場合。

            float rotatableAngle = verticalLimitEuler - currentAngle;
            rotateAngle = Mathf.Min(rotatableAngle, rotateAngle);
            return rotateAngle;
        }


        /// <summary>
        /// 上下左右の回転のみで、Z軸の(ドアノブをひねるような)回転はしないRotate。
        /// </summary>
        /// <param name="objectToRotate"></param>
        /// <param name="horizontalRotation"></param>
        /// <param name="verticalRotation"></param>
        public static void AntiTiltRotate(GameObject objectToRotate, float horizontalRotation, float verticalRotation) {
            // 回転がローカル軸に対してか、ワールド軸に対してか指定することができる。
            // Y軸方向の回転はワールド軸でないと、あちこち回転しているうちにオブジェクトが傾いてしまう。
            objectToRotate.transform.Rotate(0, horizontalRotation, 0, Space.World);
            objectToRotate.transform.Rotate(verticalRotation, 0, 0);
        }
    }
}