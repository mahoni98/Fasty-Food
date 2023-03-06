
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateWelterPrefab : EditorWindow
{
    [MenuItem("Welter Games/Create Welter Prefab")]
  
    static void CreatePrefab()
    {
        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/WelterGames/Prefabs/WelterGames.prefab", typeof(GameObject));
        GameObject go = PrefabUtility.InstantiatePrefab(prefab as GameObject) as GameObject;
        go.name = "WelterGames";
        Selection.activeObject = go;
        Undo.RegisterCreatedObjectUndo(go, "Created Welter Games Object");
    }

  
}

