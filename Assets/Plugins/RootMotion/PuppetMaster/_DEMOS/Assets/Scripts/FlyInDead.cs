using RootMotion.Dynamics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootMotion.Demos
{
    public class FlyInDead : MonoBehaviour
    {
        [SerializeField] private Rigidbody mainBody;
        [SerializeField] private float forceMultiplier = 10f;
        [SerializeField] private PuppetMaster puppetMaster;
        [SerializeField] private RagdollGroundChecker ragdollGroundChecker;

        /// <summary>
        /// ћетод дл€ перемещени€ ragdoll в указанном направлении.
        /// </summary>
        /// <param name="direction">Ќаправление перемещени€.</param>
        public void Move(Vector3 direction)
        {
            if (mainBody == null)
            {
                Debug.LogError("Main body is not assigned!");
                return;
            }

            if (puppetMaster.state != PuppetMaster.State.Dead)
                return;

            if (ragdollGroundChecker.IsGrounded())
                return;

            // ѕримен€ем силу к главной части тела в указанном направлении.
            Vector3 force = direction.normalized * forceMultiplier * TimeSystem.TimeService.Scale;
            mainBody.AddForce(force, ForceMode.Acceleration);
        }
    }
}
