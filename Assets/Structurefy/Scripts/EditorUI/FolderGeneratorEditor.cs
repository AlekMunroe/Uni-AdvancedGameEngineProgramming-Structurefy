using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class FolderGeneratorEditor : EditorWindow
{
    private string[] sfconfFiles = new string[0];
    private int configFilesIndex = 0;
    
    private List<string> folderNames = new List<string>();
    private bool[] selectedFolders;
    
    private char[] invalidChars = { '#', '%', '&', '{', '}', '\\', '<', '>', '*', '?', '/', '$', '!', '\'', '"', ':', '@', '+', '`', '|', '=', '~' };
    
    [MenuItem("Tools/Structurefy/Folder Generator Window")]
    public static void ShowWindow()
    {
        var window = GetWindow(typeof(FolderGeneratorEditor));
        
        window.titleContent = new GUIContent("Folder Generator");
    }

    private void OnEnable()
    {
        
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
            GUILayout.Label("No .sfconf files found", EditorStyles.helpBox);
        }
        
        //Show the list of folder names and toggles
        GUILayout.BeginHorizontal();

        ReadAndSaveFolderNames();
        
        //TODO: Read folder names from file and add via folderNames.add(nameInFile[i])
        
        //TODO: For each item in the list, create a GUILayout.Label(folderName[i])
        
        
        
        //TODO: Show a toggle next to each label

        GUILayout.EndHorizontal();
        
        if (selectedFolders == null || selectedFolders.Length != folderNames.Count)
        {
            selectedFolders = new bool[folderNames.Count];
            
            //Set the toggles to true by default
            for (int i = 0; i < selectedFolders.Length; i++)
            {
                selectedFolders[i] = true;
            }
        }

        for (int i = 0; i < selectedFolders.Length; i++)
        {
            GUILayout.BeginHorizontal();
            
            //Show the folder names
            GUILayout.Label("Folder: " + folderNames[i], GUILayout.Width(200));
            
            //Add a toggle for selection

            selectedFolders[i] = GUILayout.Toggle(selectedFolders[i], "Import", GUILayout.Width(80));
            
            GUILayout.EndHorizontal();
        }
        
        //The generate folders button
        if (GUILayout.Button("Generate Folders", GUILayout.Width(120)))
        {
            GenerateFolders();
        }
    }

    void PopulateList()
    {
        //BUG: This might loop due to it being called from within OnGUI()
            //Update it by calling this function on awake/refresh
            
        //Find the config files in /Structurefy/Configs/Folders
        string[] folderPaths = Directory.GetFiles(Application.dataPath + "/Structurefy/Configs/Folders", "*.sfconf", SearchOption.AllDirectories);

        //Rename to show only the file names in the dropdown
        sfconfFiles = new string[folderPaths.Length];
        for (int i = 0; i < folderPaths.Length; i++)
        {
            sfconfFiles[i] = Path.GetFileName(folderPaths[i]);
        }

        //If no files were found
        if (sfconfFiles.Length == 0)
        {
            sfconfFiles = new string[] { "No files found" };
            configFilesIndex = 0; //Reset the index to 0 which is the "No files found" option
        }
    }

    void ReadAndSaveFolderNames()
    {
        if (sfconfFiles.Length == 0 || sfconfFiles[0] == "No files found")
        {
            Debug.LogWarning("No .sfconf files found/are valid.");
            return;
        }
        
        string configFolderPath = Application.dataPath + "/Structurefy/Configs/Folders";
        string selectedConfigFile = sfconfFiles[configFilesIndex];
        string filePath = Path.Combine(configFolderPath, selectedConfigFile);
        
        //Check if the file exists
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("No .sfconf file found: " + filePath);
            return;
        }
        
        //Read the file and add the folder names
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            folderNames.Clear(); //Clear the old file names before creating the new ones

            foreach (string line in lines)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    folderNames.Add(line.Trim());
                }

                Debug.Log("Loaded folder names from: " + filePath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error reading config file: " + ex.Message);
        }
    }

    void GenerateFolders()
    {
        for (int i = 0; i < folderNames.Count; i++)
        {
            string folderName = folderNames[i];
            
            if (!selectedFolders[i])
            {
                Debug.Log("Skipping folder: " + folderName + " As it's not selected.");
                continue;
            }

            Debug.Log("Processing folder: " + folderName);
            //----------Invalid character check----------
            bool hasBadChar = false;
            
            //Check for any characters that cannot be in a folder name
            foreach (char c in folderName)
            {
                Debug.Log("Looping through characters");
                if (invalidChars.Contains(c) || char.IsSurrogate(c) || char.IsSymbol(c))
                {
                    Debug.LogWarning("Folder " + folderName + "has invalid characters");
                    hasBadChar = true;
                    break;
                }
            }

            //After finding a bad character, it will skip folder generation
            if (hasBadChar)
            {
                Debug.Log("Contining to the next loop");
                continue; //Skip to the next loop
            }

            //----------Folder generation----------
            //Format the path correctly to start in /Assets
            string folderPath = Application.dataPath + "/" + folderName;
            
            //Check if the folder exists so it can be ignored
            if (System.IO.Directory.Exists(folderPath))
            {
                Debug.LogWarning("Folder " + folderName + " already exists");
                
            }
            else
            {
                System.IO.Directory.CreateDirectory(folderPath);
                Debug.Log("Generated folder at " + folderPath);
            }
        }

        //Refresh the assets folder to immediately show the changes
        AssetDatabase.Refresh();
    }
}
