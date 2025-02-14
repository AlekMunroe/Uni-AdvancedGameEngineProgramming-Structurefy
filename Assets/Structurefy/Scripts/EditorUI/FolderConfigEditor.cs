using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;

/// <summary>
/// An editor window to create and generate .sfconfig files
/// </summary>
public class FolderConfigEditor : EditorWindow
{
    private List<string> folderNames = new List<string>();
    private string configName;
    
    /// <summary>
    /// Shows the FolderConfigEditor window in Unity
    /// </summary>
    [MenuItem("Tools/Structurefy/Create Config")]
    public static void ShowWindow()
    {
        //Create the window
        var window = GetWindow(typeof(FolderConfigEditor));
        
        //Rename the window's name
        window.titleContent = new GUIContent("Create Config");
    }

    /// <summary>
    /// Used for rendering all the UI elements for the window
    /// </summary>
    private void OnGUI()
    {
        GUILayout.Label("Add Folders", EditorStyles.boldLabel);
        
        //Show the list of folder names
        for (int i = 0; i < folderNames.Count; i++)
        {
            GUILayout.BeginHorizontal();
            
            //Text fields
            folderNames[i] = EditorGUILayout.TextField("Folder" + (i + 1), folderNames[i]);
            
            //Remove button
            if (GUILayout.Button("-", GUILayout.Width(20)))
            {
                folderNames.RemoveAt(i);
            }
            
            GUILayout.EndHorizontal();
        }
        
        //Add button
        if (GUILayout.Button("+", GUILayout.Width(20)))
        {
            folderNames.Add(string.Empty);
        }

        GUILayout.Space(10);

        //Config name
        configName = EditorGUILayout.TextField("Config Name", configName);
        
        GUILayout.Space(10);
        
        //Save button
        if (GUILayout.Button("Save Config", GUILayout.Width(100)))
        {
            SaveConfig();
        }
    }

    /// <summary>
    /// Saves the list of folder names to a .sfconfig file
    /// </summary>
    private void SaveConfig()
    {
        //The list of folders
        string[] foldersArray = folderNames.ToArray();

        //The path to all the "Folders" config files
        string configPath = Application.dataPath + "/Structurefy/Configs/Folders/";
        
        //Check if the folder exists
        if (System.IO.Directory.Exists(configPath))
        {
            //The file path and name
            string filePath = configPath + configName + ".sfconf";

            bool userOverwriteConfirmation = false;
            //Create the file if it does not exist
            if (File.Exists(filePath))
            {
                //The file exists, prompt for confirmation
                userOverwriteConfirmation = EditorUtility.DisplayDialog("Config Exists", "A .sfconfig file with the same name exists. Do you want to overwrite it?", "Yes", "No");
            }
            else
            {
                //The file does not exist, automatically generate the file
                userOverwriteConfirmation = true;
            }

            if (userOverwriteConfirmation)
            {
                File.WriteAllText(filePath, string.Empty); //Creates an empty .sfconf file
                Debug.Log("Generated file: " + filePath);
            }

            //Refresh the assets folder to immediately show the changes
            AssetDatabase.Refresh();

            //Open the file in "Append mode" and write to the next line
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                //Loop through all the folder names and write them
                for (int i = 0; i < foldersArray.Length; i++)
                {
                    writer.WriteLine(foldersArray[i]);
                }
            }
        }
    }
}
