using UnityEngine;
using UnityEditor;

public class TextureTypeChanger
{
    [MenuItem("Assets/Set Texture Type as UI (Sprite)", true)]
    private static bool ValidateSetTextureTypeAsUI()
    {
        return Selection.activeObject != null && Selection.activeObject is Texture2D;
    }

    [MenuItem("Assets/Set Texture Type as UI (Sprite)", false, 2000)]
    private static void SetTextureTypeAsUI()
    {
        string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);

        TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;

        if (textureImporter != null)
        {
            textureImporter.textureType = TextureImporterType.Sprite;

            AssetDatabase.ImportAsset(assetPath, ImportAssetOptions.ForceUpdate);

            Debug.Log($"Texture type changed to Sprite for UI: {assetPath}");
        }
        else
        {
            Debug.LogError("Failed to change texture type: TextureImporter not found.");
        }
    }
}
