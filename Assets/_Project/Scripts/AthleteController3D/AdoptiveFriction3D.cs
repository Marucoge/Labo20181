using System.Collections.Generic;
//using UnityEngine.UI;
//using UnityEditor;
using UnityEngine;
using System;


namespace Labo{
    /// <summary>
    /// 足場との間に起こる摩擦を実装する。足場が動けば自分も動く。
    /// 摩擦だけで慣性を実装していないので、足場から離れるとはたらいていた力は失われる。
    /// </summary>
    public class AdoptiveFriction{
        private GameObject athlete;
        private IGroundingDetector3D detector;
        private Transform defaultParent;


        public AdoptiveFriction(GameObject athleteObject,  IGroundingDetector3D gDetector) {
            this.athlete = athleteObject;
            this.detector = gDetector;
            this.defaultParent = null;      // transform.parent == null だと親を持たないオブジェクトになる?
        }


        public void ManualUpdate() {
            if (detector.IsGrounding == false) {
                ClearAdoption();
                return;
            }

            BeAdoptedByGround();
        }

        
        private void BeAdoptedByGround() {
            athlete.transform.parent = detector.DetectedGround.transform;
        }


        private void ClearAdoption() {
            athlete.transform.parent = defaultParent;
        }
    }
}