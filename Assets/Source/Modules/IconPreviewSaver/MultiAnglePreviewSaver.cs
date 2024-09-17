using UnityEngine;
using UnityEditor;
using System.IO;

public class MultiAnglePreviewSaver
{
    private static readonly Vector3[] Angles = new Vector3[]
    {
        new Vector3(30, 0, 0),   // Ракурс 1
        new Vector3(0, 45, 0),   // Ракурс 2
        new Vector3(-30, 90, 0), // Ракурс 3
        new Vector3(0, 135, 0),  // Ракурс 4
        new Vector3(30, 180, 0)  // Ракурс 5
    };

    [MenuItem("Assets/Save Multi-Angle Preview Icons as PNG", false, 2001)]
    private static void SaveMultiAngleIconsAsPNG()
    {
        // Получаем выбранный объект
        GameObject selectedObject = Selection.activeObject as GameObject;

        if (selectedObject == null)
        {
            Debug.LogError("Выбранный объект не является GameObject.");
            return;
        }

        // Получаем путь к объекту
        string assetPath = AssetDatabase.GetAssetPath(selectedObject);
        string directory = Path.GetDirectoryName(assetPath);
        string fileName = Path.GetFileNameWithoutExtension(assetPath);

        // Создаем скрытую камеру
        Camera camera = new GameObject("Preview Camera").AddComponent<Camera>();
        camera.backgroundColor = Color.clear;
        camera.clearFlags = CameraClearFlags.SolidColor;

        // Создаем RenderTexture для рендера предпросмотров
        RenderTexture renderTexture = new RenderTexture(512, 512, 16);
        camera.targetTexture = renderTexture;

        // Создаем GameObject для рендера
        GameObject instance = PrefabUtility.InstantiatePrefab(selectedObject) as GameObject;

        // Устанавливаем начальные настройки
        instance.transform.position = Vector3.zero;
        instance.transform.rotation = Quaternion.identity;

        // Позиционируем и рендерим с разных углов
        for (int i = 0; i < Angles.Length; i++)
        {
            // Устанавливаем ракурс камеры
            camera.transform.position = instance.transform.position + Quaternion.Euler(Angles[i]) * Vector3.back * 5;
            camera.transform.LookAt(instance.transform.position);

            // Рендерим изображение
            camera.Render();

            // Создаем Texture2D из RenderTexture
            RenderTexture.active = renderTexture;
            Texture2D previewTexture = new Texture2D(512, 512, TextureFormat.RGBA32, false);
            previewTexture.ReadPixels(new Rect(0, 0, 512, 512), 0, 0);
            previewTexture.Apply();

            // Сохраняем PNG
            byte[] pngData = previewTexture.EncodeToPNG();
            string savePath = Path.Combine(directory, $"{fileName}_icon_{i + 1}.png");
            File.WriteAllBytes(savePath, pngData);

            Debug.Log($"Сохранено изображение с ракурса {i + 1} по пути: {savePath}");
        }

        // Очищаем ресурсы
        RenderTexture.active = null;
        Object.DestroyImmediate(camera.gameObject);
        Object.DestroyImmediate(instance);
        AssetDatabase.Refresh();
    }
}
