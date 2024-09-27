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
        /// ����� ��� ����������� ragdoll � ��������� �����������.
        /// </summary>
        /// <param name="direction">����������� �����������.</param>
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

            // ��������� ���� � ������� ����� ���� � ��������� �����������.
            Vector3 force = direction.normalized * forceMultiplier * TimeSystem.TimeService.Scale;
            mainBody.AddForce(force, ForceMode.Acceleration);
        }
    }
}
