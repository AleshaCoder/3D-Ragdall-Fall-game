using UnityEngine;
using UnityEditor;
using System.IO;

public class InteractiveIconPreviewSaver : EditorWindow
{
    private GameObject selectedObject;
    private Color backgroundColor = Color.gray;
    private Vector2 size = new(512, 512);
    private Vector2 previewRotation;
    private float cameraDistance = 5f; // Для работы с приближением/отдалением
    private Vector3 cameraOffset = Vector3.zero; // Для управления камерой через WASD
    private float cameraMoveSpeed = 0.1f; // Скорость движения камеры
    private Texture _preview;

    private PreviewRenderUtility previewRenderUtility;

    [MenuItem("Assets/Save Interactive Preview Icon as PNG", false, 2000)]
    public static void ShowWindow()
    {
        InteractiveIconPreviewSaver window = GetWindow<InteractiveIconPreviewSaver>("Icon Preview");
        window.Show();
    }

    [MenuItem("Assets/Save Interactive Preview Icon as PNG", true)]
    private static bool ValidateShowWindow()
    {
        return Selection.activeObject != null && Selection.activeObject is GameObject;
    }

    private void OnEnable()
    {
        previewRenderUtility = new PreviewRenderUtility();
        previewRenderUtility.cameraFieldOfView = 30f;
    }

    private void OnDisable()
    {
        previewRenderUtility.Cleanup();
    }

    private void OnGUI()
    {
        selectedObject = Selection.activeObject as GameObject;

        if (selectedObject == null)
        {
            EditorGUILayout.HelpBox("Please select a GameObject to preview.", MessageType.Warning);
            return;
        }

        backgroundColor = EditorGUILayout.ColorField("Background Color", backgroundColor);
        size = EditorGUILayout.Vector2Field("Icon Size", size);

        GUILayout.Label("Interactive Preview", EditorStyles.boldLabel);
        Rect previewRect = GUILayoutUtility.GetRect(size.x, size.y, GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false));

        HandleMouseEvents(previewRect);

        HandleKeyboardInput();

        RenderInteractivePreview(previewRect, previewRotation, cameraDistance, cameraOffset);

        if (GUILayout.Button("Save Icon as PNG"))
        {
            SaveIconAsPNG();
        }
    }

    private void HandleMouseEvents(Rect previewRect)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDrag && previewRect.Contains(e.mousePosition))
        {
            previewRotation.x += e.delta.x;
            previewRotation.y += e.delta.y;
            Repaint();
        }

        if (e.type == EventType.ScrollWheel && previewRect.Contains(e.mousePosition))
        {
            cameraDistance += e.delta.y * 0.1f;
            cameraDistance = Mathf.Clamp(cameraDistance, 2f, 15f); // Ограничиваем дистанцию камеры
            Repaint();
        }
    }

    private void HandleKeyboardInput()
    {
        Event e = Event.current;

        if (e.type == EventType.KeyDown)
        {
            switch (e.keyCode)
            {
                case KeyCode.W:
                    cameraOffset.y += cameraMoveSpeed;
                    break;
                case KeyCode.S:
                    cameraOffset.y -= cameraMoveSpeed;
                    break;
                case KeyCode.A:
                    cameraOffset.x -= cameraMoveSpeed;
                    break;
                case KeyCode.D:
                    cameraOffset.x += cameraMoveSpeed;
                    break;
                default:
                    return;
            }

            Repaint();
        }
    }

    // Рендер интерактивного предпросмотра
    private void RenderInteractivePreview(Rect previewRect, Vector2 rotation, float distance, Vector3 offset)
    {
        if (previewRenderUtility == null || selectedObject == null)
            return;

        previewRenderUtility.BeginPreview(previewRect, GUIStyle.none);

        // Устанавливаем фон
        previewRenderUtility.camera.backgroundColor = backgroundColor;
        previewRenderUtility.camera.clearFlags = CameraClearFlags.Color;

        // Позиционируем камеру и объект
        Mesh mesh = null;
        Material material = null;

        // Проверяем наличие компонентов MeshFilter или SkinnedMeshRenderer
        MeshFilter meshFilter = selectedObject.GetComponentInChildren<MeshFilter>();
        SkinnedMeshRenderer skinnedMeshRenderer = selectedObject.GetComponentInChildren<SkinnedMeshRenderer>();

        if (meshFilter != null)
        {
            mesh = meshFilter.sharedMesh;
            material = selectedObject.GetComponentInChildren<MeshRenderer>().sharedMaterial;
        }
        else if (skinnedMeshRenderer != null)
        {
            mesh = skinnedMeshRenderer.sharedMesh;
            material = skinnedMeshRenderer.sharedMaterial;

            // Применение анимационной позы, если есть Animator
            Animator animator = selectedObject.GetComponentInChildren<Animator>();
            if (animator != null)
            {
                animator.Update(0); // Обновляем анимацию до её текущего состояния
            }
        }

        if (mesh != null && material != null)
        {
            previewRenderUtility.DrawMesh(mesh, Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(rotation.y, rotation.x, 0), Vector3.one), material, 0);
        }

        previewRenderUtility.camera.transform.position = new Vector3(offset.x, offset.y, -distance);

        previewRenderUtility.Render();

        _preview = previewRenderUtility.EndPreview();
        GUI.DrawTexture(previewRect, _preview, ScaleMode.StretchToFill, false);
    }

    // Метод для сохранения иконки как PNG
    private void SaveIconAsPNG()
    {
        if (selectedObject == null)
        {
            Debug.LogError("Nothing to save. Please make sure an object is selected.");
            return;
        }

        Texture2D texture = ToTexture2D();

        // Конвертируем в PNG и сохраняем
        byte[] pngData = texture.EncodeToPNG();
        if (pngData != null)
        {
            string assetPath = AssetDatabase.GetAssetPath(selectedObject);
            string directory = Path.GetDirectoryName(assetPath);
            string fileName = Path.GetFileNameWithoutExtension(assetPath);
            string savePath = Path.Combine(directory, fileName + "_interactive_icon.png");
            File.WriteAllBytes(savePath, pngData);
            AssetDatabase.Refresh();

            // Меняем тип текстуры на Sprite
            TextureImporter textureImporter = AssetImporter.GetAtPath(savePath) as TextureImporter;
            if (textureImporter != null)
            {
                textureImporter.textureType = TextureImporterType.Sprite;
                AssetDatabase.ImportAsset(savePath, ImportAssetOptions.ForceUpdate);
                Debug.Log($"Texture type changed to Sprite for UI: {savePath}");
            }
        }
        else
        {
            Debug.LogError("Failed to save preview as PNG.");
        }
    }

    private Texture2D ToTexture2D()
    {
        Texture2D texture = new Texture2D(_preview.width, _preview.height, TextureFormat.RGBA32, false);
        RenderTexture currentRT = RenderTexture.active;

        RenderTexture renderTexture = new RenderTexture(_preview.width, _preview.height, 32);
        Graphics.Blit(_preview, renderTexture);

        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        RenderTexture.active = currentRT;
        return texture;
    }
}