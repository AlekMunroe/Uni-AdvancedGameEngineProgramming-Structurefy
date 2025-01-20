using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class FolderGenerator : MonoBehaviour
{
    public string[] folderNames;
    private char[] invalidChars = { '#', '%', '&', '{', '}', '\\', '<', '>', '*', '?', '/', '$', '!', '\'', '"', ':', '@', '+', '`', '|', '=', '~' };
    public void GenerateFolders()
    {
        foreach (string folderName in folderNames)
        {
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
