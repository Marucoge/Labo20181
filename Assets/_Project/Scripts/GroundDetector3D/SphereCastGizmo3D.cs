#if UNITY_EDITOR  // Unity Editor 以外の環境ではコンパイルしない

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;


namespace NewNamespace {
    /// <summary>
    /// デバッグ用。SphereCast の様子を OnDrawGizmos() で描画する。
    /// 完全に独立したクラスで、SphereCaster の値を参照さえしていない(値は手動で設定する必要がある)。
    /// </summary>
    public class SphereCastGizmo3D : MonoBehaviour {
        [SerializeField] private bool IsEnable = true;
        [SerializeField] private Vector3 Offset = new Vector3(0, 0, 0);
        [SerializeField] private float Radius = 0.5f;
        [SerializeField] private float Length = 10.0f;


        // ギズモが描画されている間なら実行停止していても呼ばれ続けるので注意。
        private void OnDrawGizmos() {
            if (IsEnable == false) { return; }
            if (UnityEditor.EditorApplication.isPlaying == false) { return; } // エディタが実行中の場合のみ処理を行うように。

            Vector3 origin = transform.position + Offset;
            Vector3 direction = Vector3.down;
            int layermask = ~0;
            var hitInfo = new RaycastHit();

            bool isHit =
                Physics.SphereCast(origin, Radius, direction, out hitInfo, Length, layermask, QueryTriggerInteraction.Ignore);

            // 何にもヒットしなかった場合、レイキャストの最大距離まで伸び切った様子を描画する。
            if (isHit == false) {
                Gizmos.color = new UnityEngine.Color32(255, 0, 0, 255);      // Red
                Gizmos.DrawRay(origin, Vector3.down * Length);
                Gizmos.DrawWireSphere(origin + (direction * Length), Radius);
                return;
            }

            // ヒットしたら、その Sphere をその場所に描画。
            Gizmos.color = new UnityEngine.Color32(0, 255, 255, 255);      // Cyan
            Gizmos.DrawRay(origin, Vector3.down * hitInfo.distance);
            Gizmos.DrawWireSphere(origin + (direction * hitInfo.distance), Radius);
        }
    }
}

#endif  // Unity Editor 以外の環境ではコンパイルしない