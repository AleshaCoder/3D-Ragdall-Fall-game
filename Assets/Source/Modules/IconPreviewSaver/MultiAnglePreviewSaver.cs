using UnityEngine;
using UnityEditor;
using System.IO;

public class MultiAnglePreviewSaver
{
    private static readonly Vector3[] Angles = new Vector3[]
    {
        new Vector3(30, 0, 0),   // ������ 1
        new Vector3(0, 45, 0),   // ������ 2
        new Vector3(-30, 90, 0), // ������ 3
        new Vector3(0, 135, 0),  // ������ 4
        new Vector3(30, 180, 0)  // ������ 5
    };

    [MenuItem("Assets/Save Multi-Angle Preview Icons as PNG", false, 2001)]
    private static void SaveMultiAngleIconsAsPNG()
    {
        // �������� ��������� ������
        GameObject selectedObject = Selection.activeObject as GameObject;

        if (selectedObject == null)
        {
            Debug.LogError("��������� ������ �� �������� GameObject.");
            return;
        }

        // �������� ���� � �������
        string assetPath = AssetDatabase.GetAssetPath(selectedObject);
        string directory = Path.GetDirectoryName(assetPath);
        string fileName = Path.GetFileNameWithoutExtension(assetPath);

        // ������� ������� ������
        Camera camera = new GameObject("Preview Camera").AddComponent<Camera>();
        camera.backgroundColor = Color.clear;
        camera.clearFlags = CameraClearFlags.SolidColor;

        // ������� RenderTexture ��� ������� ��������������
        RenderTexture renderTexture = new RenderTexture(512, 512, 16);
        camera.targetTexture = renderTexture;

        // ������� GameObject ��� �������
        GameObject instance = PrefabUtility.InstantiatePrefab(selectedObject) as GameObject;

        // ������������� ��������� ���������
        instance.transform.position = Vector3.zero;
        instance.transform.rotation = Quaternion.identity;

        // ������������� � �������� � ������ �����
        for (int i = 0; i < Angles.Length; i++)
        {
            // ������������� ������ ������
            camera.transform.position = instance.transform.position + Quaternion.Euler(Angles[i]) * Vector3.back * 5;
            camera.transform.LookAt(instance.transform.position);

            // �������� �����������
            camera.Render();

            // ������� Texture2D �� RenderTexture
            RenderTexture.active = renderTexture;
            Texture2D previewTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
            previewTexture.ReadPixels(new Rect(0, 0, 512, 512), 0, 0);
            previewTexture.Apply();

            // ��������� PNG
            byte[] pngData = previewTexture.EncodeToPNG();
            string savePath = Path.Combine(directory, $"{fileName}_icon_{i + 1}.png");
            File.WriteAllBytes(savePath, pngData);

            Debug.Log($"��������� ����������� � ������� {i + 1} �� ����: {savePath}");
        }

        // ������� �������
        RenderTexture.active = null;
        Object.DestroyImmediate(camera.gameObject);
        Object.DestroyImmediate(instance);
        AssetDatabase.Refresh();
    }
}
