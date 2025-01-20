using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorMenus : EditorWindow
{
    [MenuItem("Tools/Structurefy/Generate Folders")]
    public static void GenerateFolders()
    {
        //Create a temporary gameobject to hold the script
        GameObject tempGameObject = new GameObject("TempFolderGenerator");
        FolderGenerator folderGenerator = tempGameObject.AddComponent<FolderGenerator>();
        
        //Update the array
        folderGenerator.folderNames = new string[] { "Folder1", "Folder Two", "Folder 3", "Folder Four" };
        
        folderGenerator.GenerateFolders();
        
        //Destroy the GameObject after it is used
        GameObject.DestroyImmediate(tempGameObject);
    }

    [MenuItem("Tools/Structurefy/Generate Scene")]
    public static void GenerateScene()
    {
        Debug.Log("Not implemented: Generating scene");
    }
}
