using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;



public class CreateGameObjects : EditorWindow
{
    [MenuItem("GameObject/Welter Games/Canvas")]
    
    public static void Create()
    {
        
        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WelterGames/Prefabs/GameObjects/Canvas.prefab", typeof(GameObject));
        GameObject go = PrefabUtility.InstantiatePrefab(prefab as GameObject) as GameObject;
        go.name = "Canvas";
        Selection.activeObject = go;
  
        Undo.RegisterCreatedObjectUndo(go, "Created Canvas Object");
        
    }

    [MenuItem("GameObject/Welter Games/Loading Slider")]

    public static void CreateSlider()
    {

        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WelterGames/Prefabs/GameObjects/LoadingSlider.prefab", typeof(GameObject));
        GameObject go = PrefabUtility.InstantiatePrefab(prefab as GameObject) as GameObject;
        go.name = "Loading Slider";
        Selection.activeObject = go;

        Undo.RegisterCreatedObjectUndo(go, "Created Loading Slider Object");

    }
}
