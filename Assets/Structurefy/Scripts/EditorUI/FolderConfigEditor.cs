using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;

public class FolderConfigEditor : EditorWindow
{
    private List<string> folderNames = new List<string>();
    private string configName;
    
    [MenuItem("Tools/Structurefy/Create Config")]
    public static void ShowWindow()
    {
        //Create the window
        var window = GetWindow(typeof(FolderConfigEditor));
        
        //Rename the window's name
        window.titleContent = new GUIContent("Create Config");
    }

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

            //Create the file if it does not exist
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, string.Empty); //Creates an empty .sfconf file
                Debug.Log("Generated file: " + filePath);
            }
            else
            {
                Debug.LogWarning("Folder exists, you cannot overwrite existing files.");
                
                //End this loop
                return;
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
