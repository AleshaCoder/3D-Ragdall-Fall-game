using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootMotion.Demos
{
    public class RagdollGroundChecker : MonoBehaviour
    {
        [SerializeField] private Transform groundCheck; // ����� �� ������� ����� ����� ��� (��������, ������� ���)
        [SerializeField] private float groundDistance = 0.1f; // ��������� �� �����
        [SerializeField] private LayerMask groundMask; // ����� ��� �����������, ��� ����� "�����"

        /// <summary>
        /// ���������, ��������� �� ragdoll �� �����.
        /// </summary>
        public bool IsGrounded()
        {
            // ��������� ��� ���� � ��������� ������� � ������
            return Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
        }

        private void OnDrawGizmosSelected()
        {
            // ������ ��� ��� ������������
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundDistance);
        }
    }
}
