using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootMotion.Demos
{
    public class RagdollGroundChecker : MonoBehaviour
    {
        [SerializeField] private Transform groundCheck; // Точка от которой будет пущен луч (например, позиция ног)
        [SerializeField] private float groundDistance = 0.1f; // Дистанция до земли
        [SerializeField] private LayerMask groundMask; // Маска для определения, что такое "земля"

        /// <summary>
        /// Проверяет, находится ли ragdoll на земле.
        /// </summary>
        public bool IsGrounded()
        {
            // Выпускаем луч вниз и проверяем касание с землей
            return Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundMask);
        }

        private void OnDrawGizmosSelected()
        {
            // Рисуем луч для визуализации
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundDistance);
        }
    }
}
