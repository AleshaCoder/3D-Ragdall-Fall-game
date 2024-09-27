using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Transform))]
public class CenterPivotEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // Add a button for centering pivot and resetting scale/rotation
        if (GUILayout.Button("Center Pivot and Reset Transform"))
        {
            CenterPivotAndReset();
        }
    }

    private void CenterPivotAndReset()
    {
        Transform targetTransform = ((Transform)target);

        // Ensure the GameObject has a MeshRenderer
        MeshRenderer meshRenderer = targetTransform.GetComponent<MeshRenderer>();
        if (meshRenderer == null)
        {
            Debug.LogWarning("MeshRenderer not found! Cannot center pivot.");
            return;
        }

        // Step 1: Get the original scale and rotation
        Vector3 originalScale = targetTransform.localScale;
        Quaternion originalRotation = targetTransform.rotation;

        // Step 2: Get the center of the mesh's bounds in world coordinates
        Vector3 boundsCenter = meshRenderer.bounds.center;

        // Step 3: Calculate local offset from the Transform position
        Vector3 localCenterOffset = targetTransform.InverseTransformPoint(boundsCenter);

        // Step 4: Move the Transform to the new position (adjusted by the bounds center)
        targetTransform.position += targetTransform.TransformDirection(localCenterOffset);

        // Step 5: Adjust all vertices by the inverse of the local center offset
        MeshFilter meshFilter = targetTransform.GetComponent<MeshFilter>();
        if (meshFilter != null)
        {
            // Clone the original mesh to avoid modifying the original asset
            Mesh originalMesh = meshFilter.sharedMesh;
            Mesh newMesh = Instantiate(originalMesh);
            newMesh.name = originalMesh.name + "_Modified";

            Vector3[] vertices = newMesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] -= localCenterOffset;
            }

            newMesh.vertices = vertices;
            newMesh.RecalculateBounds();

            // Step 6: Reset rotation to Quaternion.identity and adjust mesh orientation
            targetTransform.rotation = Quaternion.identity;

            // Step 7: Reset scale to Vector3.one and adjust mesh size
            targetTransform.localScale = Vector3.one;

            // Step 8: Adjust vertices to maintain original scale and rotation
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = Vector3.Scale(vertices[i], originalScale);
                vertices[i] = originalRotation * vertices[i];
            }

            newMesh.vertices = vertices;
            newMesh.RecalculateBounds();

            // Assign the new mesh
            meshFilter.sharedMesh = newMesh;

            // Get the asset path of the original mesh
            string originalMeshPath = AssetDatabase.GetAssetPath(originalMesh);

            if (!string.IsNullOrEmpty(originalMeshPath))
            {
                // Extract the directory from the original mesh path
                string directory = System.IO.Path.GetDirectoryName(originalMeshPath);

                // Create the path for the new mesh
                string newMeshPath = System.IO.Path.Combine(directory, newMesh.name + ".asset");

                // Save the new mesh asset to the same directory as the original
                AssetDatabase.CreateAsset(newMesh, newMeshPath);
                AssetDatabase.SaveAssets();

                Debug.Log("Mesh saved at: " + newMeshPath);
            }
            else
            {
                Debug.LogWarning("Original mesh path not found. Mesh not saved.");
            }
        }
    }
}
