using System.Collections.Generic;
//using UnityEngine.UI;
//using UnityEditor;
using UnityEngine;
using System;
//using IntegratedInput;
using VirtualUI;


namespace Labo{
    public class WalkCalculator3D : IMovementCalculator3D {
        public Vector3 MovementPerFrame { get; private set; }
        private GameObject walkerObject;
        private float walkSpeed = 3f;
        private VirtualStick stickL;

        public WalkCalculator3D(GameObject walker, VirtualStick virtualStickL) {
            walkerObject = walker;
            this.stickL = virtualStickL;
        }


        public void ManualUpdate() {
            //Vector3 rightwardMovement = InputIntegrator.LeftStickInput.x * walkerObject.transform.right;
            //Vector3 forwardMovement = InputIntegrator.LeftStickInput.y * walkerObject.transform.forward;
            //MovementPerFrame = (rightwardMovement + forwardMovement) * walkSpeed;

            Vector3 rightwardMovement = stickL.StickInput.x * walkerObject.transform.right;
            Vector3 forwardMovement = stickL.StickInput.y * walkerObject.transform.forward;
            MovementPerFrame = (rightwardMovement + forwardMovement) * walkSpeed;
        }
    }
}