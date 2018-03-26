using System.Collections.Generic;
using UnityEngine;
using System;


namespace Labo{
    public interface IGroundingDetector3D {
        /// <summary>
        ///  SphereCast の結果。基本的には使わなくていいはず。
        /// </summary>
        RaycastHit HitInfo { get; }
        /// <summary>
        /// 足場となっているオブジェクト。空中にいる間は null が入る。
        /// </summary>
        GameObject DetectedGround { get; }
        bool IsGrounding { get; }
    }


    // 未対応
    // SphereCast 開始点のオブジェクトを検知 (開始点を手前気味に設定する or CheckSphere/OverlapSphere を使用する


    /// <summary>
    /// 接地判定のためのコンポーネント。
    /// </summary>
    public class GroundingDetector3D : MonoBehaviour, IGroundingDetector3D{
        public RaycastHit HitInfo { get; private set; }
        public GameObject DetectedGround { get; private set; }
        public bool IsGrounding { get; private set; }
        private int layermask = ~0;


        private void Start() {
            layermask = LayerMaskGenerator.Generate(IgnoreLayer.Specified, this.gameObject.layer);
        }


        private void Update() {
            HitInfo = Cast(this.transform);
            IsGrounding = (HitInfo.collider != null);
            DetectedGround =
                IsGrounding ? HitInfo.collider.gameObject : null;
            // Debug.Log("Grounding: " + IsGrounding + " " + DetectedGround);
        }


        private RaycastHit Cast(Transform caster) {
            Vector3 offsetToCastOrigin = Vector3.down * 0.2f;
            Vector3 castOrigin = caster.transform.position + offsetToCastOrigin;
            Vector3 castDirection = Vector3.down;
            float castLength = 0.2f;
            float castRadius = 0.5f;

            var hitInfo = new RaycastHit();
            Physics.SphereCast(castOrigin, castRadius, castDirection, out hitInfo, castLength, layermask, QueryTriggerInteraction.Ignore);

            return hitInfo;
        }
    }
}