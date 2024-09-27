using System.Collections.Generic;
using UnityEngine;


namespace TimeSystem
{
    public class RigidbodyTimeScaler : MonoBehaviour
    {
        private List<Rigidbody> rigidbodies = new List<Rigidbody>();
        private float lastScale = 1f;

        private void OnValidate()
        {
            GatherRigidbodies();
        }

        private void GatherRigidbodies()
        {
            rigidbodies.Clear();
            rigidbodies.AddRange(GetComponentsInChildren<Rigidbody>());
        }

        private void FixedUpdate()
        {
            float currentScale = TimeService.Scale;

            if (Mathf.Approximately(currentScale, lastScale)) return;

            foreach (var rb in rigidbodies)
            {
                rb.velocity = rb.velocity * currentScale / lastScale;
                rb.angularVelocity = rb.angularVelocity * currentScale / lastScale;
            }

            lastScale = currentScale;
        }
    }
}
