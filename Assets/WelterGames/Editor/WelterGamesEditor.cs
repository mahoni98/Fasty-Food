
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

public class WelterGamesEditor : EditorWindow
{
 

    [MenuItem("Welter Games/Edit Settings")]
    public static void Settings()
    {
        var settings = Resources.Load<WelterGamesSettings>("WelterSettings");

        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<WelterGamesSettings>();

            string path = "Assets/WelterGames/Resources";

            if (!AssetDatabase.IsValidFolder(path))
            {
                AssetDatabase.CreateFolder("WelterGames", "Resources");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/WelterSettings.asset");

            AssetDatabase.CreateAsset(settings, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = settings;
    }
    
    
   
    
}

