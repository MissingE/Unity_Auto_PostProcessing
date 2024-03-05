using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class AutoAddLayer
{
    static AutoAddLayer()
    {
        AddLayer("PostProcessing");
    }

    private static void AddLayer(string layerName)
    {
        if (string.IsNullOrEmpty(layerName)) return;

        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
        SerializedProperty layers = tagManager.FindProperty("layers");

        bool layerExists = false;
        for (int i = 0; i < layers.arraySize; i++)
        {
            SerializedProperty layerSP = layers.GetArrayElementAtIndex(i);
            if (layerSP.stringValue == layerName)
            {
                layerExists = true;
                break;
            }
        }

        if (!layerExists)
        {
            for (int i = 8; i < layers.arraySize; i++)
            {
                SerializedProperty layerSP = layers.GetArrayElementAtIndex(i);
                if (string.IsNullOrEmpty(layerSP.stringValue))
                {
                    layerSP.stringValue = layerName;
                    tagManager.ApplyModifiedProperties();
                    Debug.Log($"Layer '{layerName}' 추가됨.");
                    break;
                }
            }
        }
    }
}
