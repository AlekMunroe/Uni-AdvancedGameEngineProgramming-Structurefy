using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

/// <summary>
/// An editor window to generate scene templates
/// </summary>
public class SceneGeneratorEditor : EditorWindow
{
    private string[] sceneNames = new string[0];
    int selectedSceneIndex = 0;
    
    string finalScenePath;
    
    /// <summary>
    /// Shows the SceneGeneratorEditor winow in Unity
    /// </summary>
    [MenuItem("Tools/Structurefy/Scene Generator Window")]
    public static void ShowWindow()
    {
        var window = GetWindow(typeof(SceneGeneratorEditor));
        
        window.titleContent = new GUIContent("Scene Generator");
    }

    /// <summary>
    /// Used for rendering all the UI elements for the window
    /// </summary>
    void OnGUI()
    {
        //Populate the list of scene name
        PopulateList();

        //Dropdown of scene files
        if (sceneNames.Length > 0)
        {
            //Populate the dropdown
            selectedSceneIndex = EditorGUILayout.Popup("Select a scene", selectedSceneIndex, sceneNames);
        }
        else
        {
            GUILayout.Label("No scenes available", EditorStyles.helpBox);
        }
        
        //Generate button
        if(GUILayout.Button("Generate Scenes", GUILayout.Width(120)))
        {
            GenerateScene(sceneNames[selectedSceneIndex]);
        }
    }

    /// <summary>
    /// This populates the list of scenes in /Stucturefy/Templates/Scenes
    /// </summary>
    void PopulateList()
    {
        string[] sceneNamesInDir = Directory.GetFiles(Application.dataPath + "/Structurefy/Templates/Scenes/", "*.unity", SearchOption.AllDirectories);
        
        //Rename to show only the file names
        sceneNames = new string[sceneNamesInDir.Length];
        for (int i = 0; i < sceneNamesInDir.Length; i++)
        {
            sceneNames[i] = Path.GetFileName(sceneNamesInDir[i]);
        }

        if (sceneNamesInDir.Length == 0)
        {
            sceneNames = new string[] { "No scenes found" };
            selectedSceneIndex = 0;
        }
    }
    
    /// <summary>
    /// This generates a new scene by copying the selected scene from /Structurefy/Templates/Scenes to /Scenes
    /// </summary>
    /// <param name="sceneSelection">The name of the scene e.g. LevelOne.unity</param>
    void GenerateScene(string sceneSelection)
    {
        //Find the scene file (.unity) in /Structurefy/Templates/Scenes based on the selection
        string sceneToCopy = Application.dataPath + "/Structurefy/Templates/Scenes/" + sceneSelection;

        finalScenePath = Application.dataPath + "/Scenes/";
        
        //Copy the file into /Scenes using System.IO
        Debug.Log("Copying scene: " + sceneToCopy + " to " + finalScenePath);
        
        //Check if the /Scenes folder exists
        if (!Directory.Exists(finalScenePath))
        {
            Directory.CreateDirectory(finalScenePath);
        }
        
        //----------Copy Scene----------
        bool userCopyConfirmation = false; //Used to check if the scene is allowed to be copied
        
        if (System.IO.File.Exists(finalScenePath + sceneSelection))
        {
            //The scene exists, prompt for confirmation
            userCopyConfirmation = EditorUtility.DisplayDialog("Scene Exists", "The scene already exists. Do you want to overwrite it?", "Yes", "No");
        }
        else
        {
            //The scene does not already exist, automatically copy
            userCopyConfirmation = true;
        }
        
        //Copy the scene if the user allows copying
        if (userCopyConfirmation)
        {
            System.IO.File.Copy(sceneToCopy, finalScenePath, true);
            AssetDatabase.Refresh();
        }
    }
}
