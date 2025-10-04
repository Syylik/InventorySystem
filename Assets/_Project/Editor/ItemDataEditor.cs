#if UNITY_EDITOR
using Game.Inventory;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    [InitializeOnLoadMethod]
    private static void SetIcons()
    {
        EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
    }

    private static void OnProjectWindowItemGUI(string guid, Rect rect)
    {
        string path = AssetDatabase.GUIDToAssetPath(guid);
        ItemData itemData = AssetDatabase.LoadAssetAtPath<ItemData>(path);

        if (itemData != null && itemData.icon != null)
        {
            Sprite icon = itemData.icon;
            Texture2D tex = icon.texture;

            Rect texCoords = icon.textureRect;
            texCoords.x /= tex.width;
            texCoords.y /= tex.height;
            texCoords.width /= tex.width;
            texCoords.height /= tex.height;

            Rect iconRect = new Rect(rect.x + 2, rect.y + 2, rect.height - 4, rect.height - 4);
            GUI.DrawTextureWithTexCoords(iconRect, tex, texCoords);
        }
    }
}
#endif