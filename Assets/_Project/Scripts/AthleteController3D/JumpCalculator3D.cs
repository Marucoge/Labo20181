using System.Collections.Generic;
using UnityEngine;
using System;




namespace Labo{
    public class JumpCalculator3D : IMovementCalculator3D {
        public Vector3 MovementPerFrame { get; private set; }
        private float jumpPower = 5f;
        private float jumpingDuration = 0;
        private float minimumJumpDuration = 0.5f;

        public bool IsJumpable { get; private set; }    // IsJumping とは別の独立した状態フラグ。
        public bool IsJumping { get; private set; }

        private VirtualButton jumpButton;
        private IGroundingDetector3D detector;




        public JumpCalculator3D(IGroundingDetector3D groundingDetector, VirtualButton vButton) {
            jumpButton = vButton;
            IsJumpable = true;
            IsJumping = false;
            detector = groundingDetector;
        }



        public void ManualUpdate() {
            if (jumpButton.IsPressed) {
                RequestJump();
                jumpButton.Release();
            }

            // ジャンプ中でなければ何もしない
            if (IsJumping == false) {  return;  }

            // ジャンプ中なら経過時間を測る
            jumpingDuration += Time.deltaTime;

            // 接地したらジャンプ状態終了
            if (jumpingDuration > minimumJumpDuration &&    // ジャンプした瞬間の接地判定を無視する
                detector.IsGrounding) {
                EndJump();
                return;
            }

            MovementPerFrame = jumpPower * Vector3.up;
        }


        // ジャンプのリクエスト
        public void RequestJump() {
            // ジャンプ可能か判定
            if (IsJumpable == false) { return; }
            if (IsJumping) { return; }

            // 可能ならジャンプ状態へ遷移
            IsJumping = true;
        }


        private void EndJump() {
            MovementPerFrame = Vector3.zero;
            jumpingDuration = 0;
            IsJumping = false;
        }
    }
}