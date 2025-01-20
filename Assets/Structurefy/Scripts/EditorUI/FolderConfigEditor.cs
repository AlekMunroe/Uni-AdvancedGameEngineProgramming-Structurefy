using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        string[] foldersArray = folderNames.ToArray();
        Debug.Log("Not implemented: Saved config \n " +
                  "Folders: " + string.Join(", ", foldersArray) + "\n " +
                  "File name: " + configName);
    }
}
