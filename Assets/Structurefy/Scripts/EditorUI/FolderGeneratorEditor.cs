using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FolderGeneratorEditor : EditorWindow
{
    private string[] sfconfFiles = new string[0];
    private int configFilesIndex = 0;
    
    private List<string> folderNames = new List<string>();
    private bool[] selectedFolders;
    
    [MenuItem("Tools/Structurefy/Folder Generator Window")]
    public static void ShowWindow()
    {
        var window = GetWindow(typeof(FolderGeneratorEditor));
        
        window.titleContent = new GUIContent("Folder Generator");
    }

    private void OnGUI()
    {
        PopulateList();
        
        //Add a new dropdown and populate it
        if (sfconfFiles.Length > 0)
        {
            configFilesIndex = EditorGUILayout.Popup("Select a config file", configFilesIndex, sfconfFiles);
        }
        else
        {
            GUILayout.Label("No .sfconf.txt files found", EditorStyles.helpBox);
        }
        
        //Show the list of folder names and toggles
        for (int i = 0; i < folderNames.Count; i++)
        {
            GUILayout.BeginHorizontal();
            
            //Text fields
            GUILayout.Label("Folder: " + folderNames[i]);
            
            //Toggle
            //selectedFolders[i] = GUILayout.Toggle(true, "", GUILayout.Width(20));
            
            //TODO: Create a toggle that sets selectedFolders[i] to that toggle
            bool thisToggle = GUILayout.Toggle(selectedFolders[i], folderNames[i], GUILayout.Width(20));
            Debug.Log("Current toggle: " + thisToggle);
            
            
            GUILayout.EndHorizontal();
        }
    }

    void PopulateList()
    {
        //Find the config files
        string[] folderPaths = Directory.GetFiles(Application.dataPath + "/Structurefy/Configs/Folders", "*.sfconf.txt", SearchOption.AllDirectories);

        //Rename to show only the file names in the dropdown
        sfconfFiles = new string[folderPaths.Length];
        for (int i = 0; i < folderPaths.Length; i++)
        {
            sfconfFiles[i] = Path.GetFileName(folderPaths[i]);
        }

        //If no files were found
        if (sfconfFiles.Length == 0)
        {
            sfconfFiles = new string[] { "No files found" }; //Add this to the dropdown
            configFilesIndex = 0; //Reset the index to 0 which is the "No files found" option
        }
    }
}
