using UnityEngine;
using RootMotion.Dynamics;
using TimeSystem;
using System.Linq;

namespace RootMotion.Demos
{
    /// <summary>
    /// ����������� �������� � ������������� �������� ���������, ���� ��� �� ��������.
    /// </summary>
    public class AutoResurrection : MonoBehaviour
    {
        [Tooltip("Reference to the PuppetMaster component.")]
        public PuppetMaster puppetMaster;

        [Tooltip("����������� ��������, ���� ������� �������� ��������� �����������.")]
        public float minMovementThreshold = 0.1f;

        [Tooltip("����� � ��������, �� ��������� �������� �������� �������, ���� �� ���������.")]
        public float resurrectionTime = 3f;

        private float idleTime = 0f;
        private Vector3 lastPosition;
        private Muscle baseMuscle;

        void Start()
        {
            if (puppetMaster == null)
            {
                puppetMaster = GetComponent<PuppetMaster>();
            }

            baseMuscle = puppetMaster.muscles.FirstOrDefault(m => m.props.group == Muscle.Group.Spine);
            lastPosition = baseMuscle.transform.position;
        }

        void Update()
        {
            if (puppetMaster.state == PuppetMaster.State.Dead)
            {
                CheckMovement();
            }
        }

        private void CheckMovement()
        {
            float movement = (baseMuscle.transform.position - lastPosition).magnitude;

            if (movement < minMovementThreshold)
            {
                idleTime += TimeService.Delta;

                if (idleTime >= resurrectionTime && puppetMaster.state == PuppetMaster.State.Dead)
                {
                    Resurrect();
                }
            }
            else
            {
                idleTime = 0f;
            }

            lastPosition = baseMuscle.transform.position;
        }

        private void Resurrect()
        {
            puppetMaster.Resurrect();
            idleTime = 0f;
        }
    }
}
