using UnityEngine;
using RootMotion.Dynamics;
using System.Collections.Generic;

namespace RootMotion.Demos
{
    /// <summary>
    /// Управляет сбросом состояния персонажа: восстанавливает позицию, поворот и оживляет.
    /// </summary>
    public class CharacterResetter : MonoBehaviour
    {
        [Tooltip("Reference to the PuppetMaster component.")]
        public PuppetMaster puppetMaster;

        [Tooltip("Reference to the Transform component of Character Controller.")]
        public CharacterMeleeDemo character;

        private Dictionary<Transform, TransformState> initialStates = new Dictionary<Transform, TransformState>();
        private TransformState _defaultPuppet;

        [System.Serializable]
        private struct TransformState
        {
            public Vector3 position;
            public Quaternion rotation;
        }

        void Start()
        {
            if (puppetMaster == null)
            {
                puppetMaster = GetComponent<PuppetMaster>(); // Автоматически находим PuppetMaster
            }

            // Сохраняем исходное состояние для всех объектов в иерархии
            SaveInitialStates(character.transform);
            SaveInitialStates(puppetMaster.transform);
            _defaultPuppet = new() { position = puppetMaster.transform.position, rotation = puppetMaster.transform.rotation };
        }

        /// <summary>
        /// Сохраняет исходное состояние позиции и поворота для всех объектов в иерархии.
        /// </summary>
        private void SaveInitialStates(Transform root)
        {
            foreach (Transform child in root)
            {
                SaveInitialStates(child);
            }

            initialStates[root] = new TransformState
            {
                position = root.localPosition,
                rotation = root.localRotation
            };
        }

        /// <summary>
        /// Сбрасывает позицию и состояние персонажа.
        /// </summary>
        public void ResetCharacter()
        {
            RestoreInitialStates(puppetMaster.transform);
            RestoreInitialStates(character.transform);
        }

        /// <summary>
        /// Восстанавливает позицию и поворот для всех объектов в иерархии.
        /// </summary>
        private void RestoreInitialStates(Transform root)
        {
            if (initialStates.TryGetValue(root, out TransformState state))
            {
                root.localPosition = state.position;
                root.localRotation = state.rotation;
            }

            foreach (Transform child in root)
            {
                RestoreInitialStates(child);
            }
        }

        /// <summary>
        /// Сбрасывает скорость у всех Rigidbody в иерархии.
        /// </summary>
        private void ResetRigidbodies(Transform root)
        {
            Rigidbody rb = root.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            foreach (Transform child in root)
            {
                ResetRigidbodies(child);
            }
        }
    }
}
