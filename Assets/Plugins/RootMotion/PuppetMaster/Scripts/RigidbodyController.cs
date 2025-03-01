﻿using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using UnityEngine;

namespace RootMotion.Dynamics
{

    /// <summary>
    /// Adds force and torque to a Rigidbody to make it follow a target Transform.
    /// </summary>
    public class RigidbodyController : MonoBehaviour
    {

        public Transform target;
        [Range(0f, 1f)] public float forceWeight = 1f;
        [Range(0f, 1f)] public float torqueWeight = 1f;
        public bool useTargetVelocity = true;

        private Rigidbody r;
        private Vector3 lastTargetPos;
        private Quaternion lastTargetRot = Quaternion.identity;

        /// <summary>
        /// Call this after target has been teleported
        /// </summary>
        public void OnTargetTeleported()
        {
            lastTargetPos = target.position;
            lastTargetRot = target.rotation;
        }

        private void Start()
        {
            r = GetComponent<Rigidbody>();
            OnTargetTeleported();
        }

        private void FixedUpdate()
        {
            Vector3 targetVelocity = Vector3.zero;
            Vector3 targetAngularVelocity = Vector3.zero;

            // Calculate target velocity and angular velocity
            if (useTargetVelocity)
            {
                targetVelocity = (target.position - lastTargetPos) / TimeService.Delta;

                targetAngularVelocity = PhysXTools.GetAngularVelocity(lastTargetRot, target.rotation, TimeService.Delta);
            }

            lastTargetPos = target.position;
            lastTargetRot = target.rotation;

            // Force
            Vector3 force = PhysXTools.GetLinearAcceleration(r.position, target.position);
            force += targetVelocity;
            force -= r.velocity;
            if (r.useGravity) force -= Physics.gravity * TimeService.Delta;
            force *= forceWeight;
            r.AddForce(force, ForceMode.VelocityChange);

            // Torque
            Vector3 torque = PhysXTools.GetAngularAcceleration(r.rotation, target.rotation);
            torque += targetAngularVelocity;
            torque -= r.angularVelocity;
            torque *= torqueWeight;
            r.AddTorque(torque, ForceMode.VelocityChange);
        }
    }
}
