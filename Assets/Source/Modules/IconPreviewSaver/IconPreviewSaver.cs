using UnityEngine;
using UnityEditor;
using System.IO;

namespace Assets.Source.Modules.IconPreviewSaver
{
    public class IconPreviewSaver
    {
        [MenuItem("Assets/Save Preview Icon as PNG Fast", true)]
        private static bool ValidateSaveIconAsPNG()
        {
            // Validate if there's an active selection and it has a preview
            return Selection.activeObject != null && AssetPreview.GetAssetPreview(Selection.activeObject) != null;
        }

        [MenuItem("Assets/Save Preview Icon as PNG Fast", false, 2001)]
        private static void SaveIconAsPNG()
        {
            // Get selected asset
            Object selectedAsset = Selection.activeObject;

            if (selectedAsset == null)
            {
                Debug.LogError("No asset selected.");
                return;
            }

            // Get the asset path
            string assetPath = AssetDatabase.GetAssetPath(selectedAsset);

            // Get the directory and file name
            string directory = Path.GetDirectoryName(assetPath);
            string fileName = Path.GetFileNameWithoutExtension(assetPath);

            // Get the preview texture
            Texture2D previewTexture = AssetPreview.GetAssetPreview(selectedAsset);

            if (previewTexture == null)
            {
                Debug.LogError("No preview available for the selected asset.");
                return;
            }

            // Convert Texture2D to PNG
            byte[] pngData = previewTexture.EncodeToPNG();

            if (pngData == null)
            {
                Debug.LogError("Failed to encode texture to PNG.");
                return;
            }

            // Save the PNG file in the same directory as the asset
            string savePath = Path.Combine(directory, fileName + "_icon.png");
            File.WriteAllBytes(savePath, pngData);

            // Refresh the AssetDatabase to show the new file in the Project Window
            AssetDatabase.Refresh();

            TextureImporter textureImporter = AssetImporter.GetAtPath(savePath) as TextureImporter;

            if (textureImporter != null)
            {
                // Меняем тип текстуры на Sprite (2D and UI)
                textureImporter.textureType = TextureImporterType.Sprite;

                // Применяем изменения
                AssetDatabase.ImportAsset(savePath, ImportAssetOptions.ForceUpdate);

                Debug.Log($"Texture type changed to Sprite for UI: {savePath}");
            }
            else
            {
                Debug.LogError("Failed to change texture type: TextureImporter not found.");
            }

            Debug.Log($"Icon saved as PNG at: {savePath}");
        }
    }
}