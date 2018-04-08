using System.Collections.Generic;
using UnityEngine;
using System;


namespace Labo{
    [RequireComponent(typeof(Rigidbody))]   // 注:物理挙動を使わない場合でも必要。Kinematicとして使う。
    public class AutomaticRotator : MonoBehaviour{
        [SerializeField] private Vector3 RotationPerSecond = new Vector3(0, 360, 0);
        
        private void Update() {
            Vector3 rotationPerFrame = RotationPerSecond * Time.deltaTime;
            this.transform.Rotate(rotationPerFrame);
        }

    }
}