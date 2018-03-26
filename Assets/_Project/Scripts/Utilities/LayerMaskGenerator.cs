using System.Collections.Generic;
using UnityEngine;
using System;


namespace Labo {
    /// <summary>
    /// レイヤーマスクの指定用。特定のレイヤーを無視するか、特定のレイヤー以外を無視するか。
    /// </summary>
    public enum IgnoreLayer { Specified, ExceptSpecified }

    public static class LayerMaskGenerator{
        #region レイヤーマスクについて
        // ビット操作でマスクを作る。
        // layerMask が ゼロのときは すべてのレイヤーと衝突しない。(つまりすべて false)
        #endregion
            

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ignoreLayer"></param>
        /// <param name="specifiedLayerIndexes">指定したレイヤーを無視、または指定したレイヤー以外を無視するように設定する。</param>
        /// <returns>ビット操作によって作られたレイヤーマスク。</returns>
        public static int Generate(IgnoreLayer ignoreLayer, params int[] specifiedLayerIndexes) {
            if (specifiedLayerIndexes.Length < 1) { return ~0; }
            int layerMask = 0;      // 0 だとすべてのレイヤーと衝突しない。-1だとすべてのレイヤーと衝突する(~0)。

            // マスクに穴を開けていく。
            foreach (int element in specifiedLayerIndexes) {
                int flag = 1 << element;                 // 1(つまり一桁目がtrue)をn桁シフトすることで、n桁にtrueを入れる。
                layerMask = layerMask | flag;         // or演算子 
                // Debug.Log("Specified layer: " + LayerMask.LayerToName(element)); // デバッグ用に残しておく。
            }

            // 指定レイヤーを無視したい場合、生成したビットマスクを反転させる。
            if (ignoreLayer == IgnoreLayer.Specified) { layerMask = ~layerMask; }

            return layerMask;
        }
    }
}