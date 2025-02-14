# Uni-AdvancedGameEngineProgramming-Structurefy
 A tool to setup your new Unity Project

19/01/2025
Plan: The plan for today is very simple. Figure out how to make unity create folders directly in the hierarchy. I assume this will be done using AssetDatabase but I will need to have a look in the unity docs.

•	Researched docs and found this: https://docs.unity3d.com/2022.3/Documentation/ScriptReference/AssetDatabase.CreateFolder.html
-	This will help me create folders inside the Assets folder
•	Instead of using AssetDatabase, I found I could use System.IO to create a folder directly in the assets folder (Application.dataPath)






•	Updated the function to loop through a new string array called folderNames to allow multiple folders to be created
•	Added a catch for if the folders exist so they will be ignored
 

•	Added a catch to check if the string includes any characters that will not work in a file name: https://www.mtu.edu/umc/services/websites/writing/characters-avoid/
•	Created an EditorMenus script to handle the Tools/Structurefy buttons. Right now, it will call and run the GenerateFolders function from the FolderGenerator script.











•	Demo video: https://www.youtube.com/watch?v=NwnakwdhLDs

Outcome: Today went better than I expected. It was very easy to use System.IO to help me generate folders. I was also able to create an easy menu to handle the button press which I was not planning to do for weeks. This will allow me to test the tool without opening the inventory.

20/01/2025
Plan: Today’s plan is to create the menus as I had designed in my deign diagrams in the Project Plan & Design document. I do not expect to get these working today but I do expect to get the formatting of the menus working. I do however, expect the buttons to be interactable. What I mean by that is they will debug.log to confirm they all work.

•	Updated EditorMenus.cs to include buttons for Generate Scene and Config Editor.
•	Created a new script called FolderConfigEditor.cs which will handle the window for the config editor.
•	Using this from the unity docs to help me get started: https://docs.unity3d.com/Manual/editor-EditorWindows.html
•	The /Tools/Structurefy/Config Editor button is now handled by FolderConfigEditor.cs
•	Created the folderNames as a list so they can be stored and viewed
•	Added a text field to add the folder names with a – button on the side of each one to remove them
•	Added a + button to add new input fields
•	Put the input fields into a for loop so I can dynamically add and remove them
•	Added a configName text field with a button to save the config.
•	Added a SaveConfig function which simply debugs the information for now and later it will be updated to add functionality with FolderGenerator.cs
•	Created a new script called FolderGeneratorEditor.cs which will handle the UI for the folder generator
•	Used this to help me create a dropdown: https://docs.unity3d.com/6000.0/Documentation/ScriptReference/EditorGUILayout.DropdownButton.html

23/01/2025
Plan: Today, I am going to do some research into xml comments and do some testing in the unity project to see how they work. I have opened the project and I have noticed that FolderGeneratorEditor.cs has not been complete. I am going to update it so it only has a working design without functionality.

•	Created a temporary fix to the UI for FolderGeneratorEditor.cs to remove an error, there are TEMP, BUG and TODO commands in that script.
 

 

•	Examples of XML comments: https://stefanrichings.com/2019/07/16/unity-tips-xml-comments/
•	It seems really limited on where I can put XML comments e.g. <summary>. I am only able to use them “Directly above a class, method, or property to provide a brief description of its functionality”.
	- Example (According to Google AI overview): 
Missing + Need to add:
-	31st January 2025 (According to video)
-	1st February 2025 (According to video)


13/02/2025 – Nate said “It os good for developers, Think about the wider use”

31st January – 13th February
•	Created a new script FolderConfigEditor. This script:
o	Creates a new window in Tools/Stucturefy/Create Config
o	Allows the user to create a list of files they would like to generate
o	Save it to a .sfconfig (structurefy configuration) file to Structurefy/Configs/Folders
•	Updated FolderGeneratorEditor. This script:
o	Finds all the files in Structurefy/Configs/Folders and allows the user to select one
o	Allows the user to choose which folder to import with a toggle
o	Generate the selected folders which will show up in the /Assets folder
•	Created a new script SceneGeneratorEditor. This script:
o	Finds all the scenes in Structurefy/Templates/Scenes and puts them in a dropdown
o	Allows the user to select a scene and generate it, it will copy the scene to Assets/Scenes
Demo of current progress: https://www.youtube.com/watch?v=GPk29Jao5FA

14-02-2025
Plan: Today, I am going to complete my scene generator. This will not include generating the scripts. I need to do a check for if the scene already exists, if it does prompt the user if they would like to override the existing scene.

Updates:
•	Added an if statement to check if the scene already exists. If it does, it will use DisplayDialogue to confirm if the user wants to overwrite the existing scene with the template.
•	I have updated FolderConfigEditor to do the same prompt if there is a config with the same name. I have decided not to do the same thing in FolderGeneratorEditor since this generates empty folders and overwriting an existing folder with an empty folder is useless.
•	I have tried to copy scripts along with the scenes but I have decided not to do that simply with how coplicated it is getting
   
https://www.youtube.com/watch?v=wZucgdf4dq8

•	I have added XML comments to all the scripts using /// <summary> to explain all the functions and classes
