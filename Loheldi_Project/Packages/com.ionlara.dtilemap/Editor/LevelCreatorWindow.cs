using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System;

namespace TileMap3D
{
    /// <summary>
    /// The Level Creator Window Class is used to create the window for the tool, as well as for handling all the information
    /// used by the window. 
    /// It handles the creation of the Level Spawner objects, along with setting their values.
    /// It lets the user choose the Tile to use, as well as add new Tiles and customize them.
    /// From here the user can save their custom Tilesets and Load previously saved ones.
    /// It allows the user to enter and exit Edit Mode.
    /// And it also handles the baking and saving of meshes.
    /// </summary>
    public class LevelCreatorWindow : EditorWindow
    {
        #region Variables
        EditorLevelSpawner levelSpawner; //A reference to the spawned Level Spawner
        int numberOfFloors = 0; //How many floors will the level have
        int levelWidth = 0; //How many tiles wide will the level be
        int levelDepth = 0; //How many tiles in depth will the level have
        float floorHeight = 1; //How many Unity Units of height each floor has
        float gridSize = 1; //The size of each of the tiles in the grid
        int currentFloor = 1; //Currently selected floor
        bool set = false; //Is the Level Set for Editing?
        bool editing = false; //Is the Level being edited currently
        bool subscribed = false; //Subscribed to the scene and Initialized Mappings
        GameObject grid; // Editing Grid GameObject
        GameObject gridCollider; //The collider in the Grid Gameobject, used for raycasting
        int currentScene; //Scene Window is currently at
        int scene; //Scene Subscribed to
        int lvlValSpace = 0; //Extra space for the window to fit the level options if active
        int tileValSpace = 0; //Extra space for the window to fit the current tile values if active
        int tilesetSpace = 0; //Extra space for the window to fit the tileset if active
        int setSpace = 0; //Extra space for the window to fit after creating/finding a Level Spawner

        protected static bool showLvlVal = true; //Show the level values?
        protected static bool showBak = false; //Show the baking options?
        protected static bool showNewPrf = false; //Show the Add Prefab options
        protected static bool showTileVal = true; //Show the current Tile's values
        protected static bool showTileset = true; //Show the Tileset

        List<TileValues> tileSet = new List<TileValues>(); //The tiles
        TileValues selectedObject = null; //Selected tile
        FloorMesh floorMesh = null; //Currently selected settings for the Self tiling Floor
        FloorMesh setFloorMesh = null; //Lastly selected settings for the Self tiling Floor
        static string currentSettings = ""; //The place where the current Self Tiling Floor settings are stored
        
        string tileName = ""; //Tile name from the currently selected tile
        string setName = ""; //Check to see if the Tile name has been changed
        
        Texture2D tileIcon; //Tile image from te currently selected tile
        Texture2D setIcon = null; //Check to see if the Tile image has been changed

        Material tileMat = null; //Tile material from the currently selected tile
        Material setMat = null; //Check to see if the Tile material has been changed

        bool needMat = false; //Variable to check if the currently selected Tile needs a Material
        bool change = false; //Variable to check if after changing the current Tile's material, its image needs to be updated
        
        bool tileRotate; //Can the currently selected tile rotate
        bool setRotate; //Check to see if the "can rotate" value has changed
        
        bool tileIsFloor; //Is the current Tile considered a floor
        bool setIsFloor; //Check to see if the "is floor" value has changed
        
        bool tileNeedsFloor; //Does the current tile need a floor
        bool setNeedFloor; //Check to see if the "needs floor" value has changed

        bool updated = false; //Check to see if the values of the current tile have been updated on the window
        bool defaultTile = true; //Check to see if the current tile is one of the Default Tiles

        Vector2 scrollPosition; //The window scroll position
        
        GUIStyle bold = null; //GUI style for the level values when they differ from the current level's values
        GUIStyle boldFoldout = null; //GUI style for the Tileset foldout
        
        #endregion

        #region WindowCreation
        //This section sets up the creation of the window through the Tools Tab
        [MenuItem("/Tools/Level Creator")]
        public static void ShowWindow() 
        {
            GetWindow<LevelCreatorWindow>("Level Creator");
        }

        private void OnDestroy()
        {
            StopEditing(); //If the window is destroyed, then stop editing
        }
        #endregion
        
        #region Updates
        private void Update()
        {
            if (subscribed == false)
            {
                //When the window initializes get the current scene and set it as the subscribed scene
                currentScene = EditorSceneManager.GetActiveScene().handle;
                scene = currentScene; //Check for last set scene
                subscribed = true; //Initialization has been completed
                InitializeTileset(); //Set up the basic Tiles for the Tileset
                //Check if there is a selection of the self tiling floor settings on the Editor Preferences
                currentSettings = EditorPrefs.GetString("floorSettings","Assets/com.ionlara.dtilemap/Runtime/FloorMesh/GrassFloor.asset");
                //Set the found selection or the default self tiling floor settings
                floorMesh = AssetDatabase.LoadAssetAtPath<FloorMesh>(currentSettings); 
                setFloorMesh = floorMesh; //Update the check
                updated = false; //Set to update the values of the chosen tile
                if (bold == null)
                { //Copy the values from the number field style for the bold style
                    bold = new GUIStyle(EditorStyles.numberField);    
                }
                bold.fontStyle = FontStyle.Bold; //Set the font style as bold
                if (boldFoldout == null)
                {
                    boldFoldout = new GUIStyle(EditorStyles.foldoutHeader);
                }
                boldFoldout.fontStyle = FontStyle.Bold;
            }
            currentScene = EditorSceneManager.GetActiveScene().handle;
            //Whenever the scene changes, the Window values will reset
            if(currentScene != scene)
            {
                ResetBuilder();
                scene = currentScene; //Update the subscribed scene
            }
            //If the Level Spawner is destroyed then stop Editing and require creation of a new one
            if (levelSpawner == null)
            {
                set = false;
                StopEditing();
            }
        }
        private void OnGUI()
        {
            GUI.skin.button.wordWrap = true; //Set buttons to have wordwrap
            //If the tileset is being shown, add extra space based on how many lines it has
            if (showTileset == true)
            {
                tilesetSpace = (Mathf.FloorToInt(tileSet.Count/3)+1)*92;
            }
            else
            {
                tilesetSpace = 0;
            }
            //If the Level Spawner has bene set then add space to fit the changes in the window
            if (set == true)
            {
                setSpace = 460;
            }
            else
            {
                setSpace = 0;
            }
            ////////////////////////////////////////////////////////////////////////////////////
            //Set up the window's work area///////////////////////
            Rect workArea = GUILayoutUtility.GetRect(10, 1000, 10, 10000);
            scrollPosition = GUI.BeginScrollView(workArea, scrollPosition, new Rect(0, 0, 280, 28 + lvlValSpace + tileValSpace + tilesetSpace + setSpace));
            GUILayout.BeginArea(new Rect(25, 25, 250, 28 + lvlValSpace + tileValSpace +tilesetSpace + setSpace));
            /////////////////////////////////////////////////////
            //Show the level values and allow user to change them//////////////////////
            #region Level Values
            showLvlVal = EditorGUILayout.Foldout(showLvlVal, "Level Values"); //Fold out for the Level values 
            if (showLvlVal == true)
            {
                lvlValSpace = 130; //Set extra space if the Level Values section is being shown
                EditorGUI.BeginDisabledGroup(editing); //Disable the level values editing while the user is editing
                //Field for the number of floors////////////////////////////////////////////////
                if (levelSpawner == null || numberOfFloors == levelSpawner.floors)
                { //If there is no reference to any Level spawner set, or the numer is the same as the set Level Spawner use the normal font
                    numberOfFloors = EditorGUILayout.IntField(new GUIContent("Floors","How many floors will the level have"), numberOfFloors);
                }
                else
                { //If the number is different than the set Level Spawner use a bold font
                    numberOfFloors = EditorGUILayout.IntField(new GUIContent("Floors","How many floors will the level have"), numberOfFloors, bold);
                }
                ////////////////////////////////////////////////////////////////////////////////////
                //Field for the level width/////////////////////////////////////////////////////////
                if (levelSpawner == null || levelWidth == levelSpawner.width)
                { //If there is no reference to any Level spawner set, or the numer is the same as the set Level Spawner use the normal font
                    levelWidth = EditorGUILayout.IntField(new GUIContent("Level Width","How many tiles will be on the X Axis"), levelWidth);
                }
                else
                { //If the number is different than the set Level Spawner use a bold font
                    levelWidth = EditorGUILayout.IntField(new GUIContent("Level Width","How many tiles will be on the X Axis"), levelWidth,bold);
                }
                /////////////////////////////////////////////////////////////////////////////////////
                //Field for the level depth//////////////////////////////////////////////////////////
                if (levelSpawner == null || levelDepth == levelSpawner.height)
                { //If there is no reference to any Level spawner set, or the numer is the same as the set Level Spawner use the normal font
                    levelDepth = EditorGUILayout.IntField(new GUIContent("Level Depth","How many tiles will be on the Z Axis"), levelDepth);
                }
                else
                { //If the number is different than the set Level Spawner use a bold font
                    levelDepth = EditorGUILayout.IntField(new GUIContent("Level Depth","How many tiles will be on the Z Axis"), levelDepth,bold);
                }
                //////////////////////////////////////////////////////////////////////////////////////
                GUILayout.Space(10); //Add some space between the basic settings and the advanced settings
                GUILayout.Label("Grid Values:", EditorStyles.boldLabel); //Title label for the grid values
                //Field for the floor height
                if (levelSpawner == null || floorHeight == levelSpawner.floorHeight)
                { //If there is no reference to any Level spawner set, or the numer is the same as the set Level Spawner use the normal font
                    floorHeight = EditorGUILayout.FloatField(new GUIContent("Floors Height", "The size of each floor, must be above 0.1"), floorHeight);
                }
                else
                { //If the number is different than the set Level Spawner use a bold font
                    floorHeight = EditorGUILayout.FloatField(new GUIContent("Floors Height", "The size of each floor, must be above 0.1"), floorHeight,bold);
                }
                //////////////////////////////////////////////////////////////////////////////////////////
                //Field for the grid size/////////////////////////////////////////////////////////////////
                if (levelSpawner == null || gridSize == levelSpawner.gridSize)
                { //If there is no reference to any Level spawner set, or the numer is the same as the set Level Spawner use the normal font
                    gridSize = EditorGUILayout.FloatField(new GUIContent("Grid Size","The size of each square in the grid, must be above 0.1"),gridSize);
                }
                else
                { //If the number is different than the set Level Spawner use a bold font
                    gridSize = EditorGUILayout.FloatField(new GUIContent("Grid Size","The size of each square in the grid, must be above 0.1"),gridSize,bold);
                }
                ////////////////////////////////////////////////////////////////////////////////////////////
                EditorGUI.EndDisabledGroup();
            }
            else
            {
                lvlValSpace = 0; //If the Level values section is not shown don't add extra space
            }
            #endregion
            GUILayout.Space(15);
            ///////////////////////////////////////////////////////////////////////////
            GUILayout.BeginHorizontal();
            if (editing == false)
            {
                SetUpSpawner(); //Create a new spawner and delete any that could be on the scene
            }
            FindSpawner(); //Find a spawner on the scene and set it as selected
            GUILayout.EndHorizontal();
            GetKeyboard();//Get the keyboard input for the Hotkeys
            if (set == true && editing == false)
            {
                if (GUILayout.Button("Start Editing"))
                {
                    StartEditing(); //Initialize and start Edit Mode
                }
            }
            if (set == true && editing == true)
            {
                currentFloor = levelSpawner.currentHeight+1; //Set the window current floor from the Level Spawner
                grid.transform.position = new Vector3(0, (currentFloor - 1)*floorHeight, 0); //Set the Grid to the selected height
                if (GUILayout.Button("Stop Editing"))
                {
                    StopEditing(); //Stop editing the scene and destroy the grid
                }
            }
            Repaint();
            //If the editor enters play mode then stop editing////////////
            if (EditorApplication.isPlayingOrWillChangePlaymode == true)
            {
                StopEditing();
            }
            //////////////////////////////////////////////////////////////
            if (set == true)
            {
                DisplayCurrentFloor(); //Show the current height and allow the user to change it
            }
            ///////////////////////////////////////////////////////////////
            //Allow the user to set up the meshes and Material that will be used by the Self Tiling Floor
            if (levelSpawner != null)
            {
                //CurrentTilePreview(); //Show the information about the currently selected tile
                //Field for the current self tiling floor tile settings
                floorMesh = (FloorMesh)EditorGUILayout.ObjectField(new GUIContent("Floor Settings:","The Self Tiling Floor Settings (Material & Meshes)"), floorMesh, typeof(FloorMesh), false);
                if (setFloorMesh != floorMesh) //If the settings are changed then update them
                {
                    levelSpawner.floorMesh = floorMesh; //Update the settings at the level spawner
                    setFloorMesh = floorMesh; //Update the check
                    currentSettings = AssetDatabase.GetAssetPath(floorMesh); //Save the path of the current settings
                    EditorPrefs.SetString("floorSettings", currentSettings); //Save the chosen settings on the Editor Preferences
                }
                ShowTiles();//Show the user's Tileset
                if (tileSet.Count > 2 && GUILayout.Button("Save Tileset"))
                {
                    SaveTileSet(); //Allow the user to save their custom tileset
                }
                if (GUILayout.Button("Load Tileset"))
                {
                    LoadTileSet(); //Allow the user to load a custom tileset
                }
                if (editing == false)
                {
                    if (GUILayout.Button("Reset Tileset"))
                    {
                        bool confirm = false;
                        confirm = EditorUtility.DisplayDialog("Reset Tileset", "Are you sure you want to reset your tileset to the default tiles? Your current tileset won't be saved.","Continue", "Cancel");
                        InitializeTileset(); //Reset the tileset to its default state
                    }
                    GUILayout.Space(15);
                    GUILayout.Label("Meshes Baking & Saving", EditorStyles.boldLabel);
                    BakeAndSave(); //Allow the user to bake the levels they have created
                }
            }
            GUILayout.EndArea();
            GUI.EndScrollView();
        }
        //Show the preview and the settings of the currently selected Tile
        
        private void GetKeyboard() //Get the Keyboard Input for Hotkeys
        {
            Event current = Event.current; //Detect the current event
            if (current.type != EventType.KeyDown) //Return if the event is not a Key being pressed
            {
                return;
            }
            if (current.keyCode == KeyCode.LeftBracket && currentFloor > 1)//Lower current Floor
            {
                currentFloor --;
                levelSpawner.currentHeight = currentFloor-1; //Update Level Spawner current floor
            }
            if (current.keyCode == KeyCode.RightBracket && currentFloor < numberOfFloors)//Increase current Floor
            {
                currentFloor ++;
                levelSpawner.currentHeight = currentFloor-1; //Update Level Spawner current floor
            }
        }
        #endregion
        
        #region Set Up
        private void SetUpSpawner() //Create and set up a Level spawner object in the level
        {
            if (GUILayout.Button("Set Up New Level Spawner",GUILayout.Width(120))) //Create a button
            {
                bool confirmation = true;
                //If there is already a Level spawner assigned, send a warning to accidental avoid progress loss 
                if (levelSpawner != null)
                {
                    confirmation = EditorUtility.DisplayDialog("Set Up New Level Spawner","If you create a new Level Spawner the existing one will be destroyed. Any created tiles will be destroyed. Continue?", "Continue","Cancel");
                }
                if (confirmation == true && numberOfFloors > 0 && levelWidth > 0 && levelDepth > 0 && floorHeight >= 0.1 && gridSize >= 0.1)
                {
                    //Find any Level spawners on the scene and destroy them to replace them
                    EditorLevelSpawner[] spawners = (EditorLevelSpawner[])Resources.FindObjectsOfTypeAll<EditorLevelSpawner>();
                    if (spawners.Length > 0)
                    {
                        for (int i = 0; i < spawners.Length; i++)
                        {
                            UnityEngine.Object.DestroyImmediate(spawners[i].gameObject);
                        }
                    }
                    //Create a new Level Spawner with the settings chosen by the user
                    GameObject temp = new GameObject(); //Create a new Empty Game Object
                    temp.name = "LevelSpawner"; //Set the object name
                    temp.AddComponent<EditorLevelSpawner>(); //Add the Level Spawner component
                    ParentFloors(temp); //Parent the floors based on how many floors the level will have
                    levelSpawner = temp.GetComponent<EditorLevelSpawner>();  //Get the reference to the Level Spawner
                    Debug.Log("Created Level Spawner"); //Send a log
                    set = true; //The level has been set
                    levelSpawner.UpdateTileset(tileSet); //Update the Tileset in the Level Spawner
                    levelSpawner.floorHeight = floorHeight; //Set the floor height in the Level Spawner
                    levelSpawner.gridSize = gridSize; //Set the grid size in the Level Spawner
                    levelSpawner.InitializeMatrix(levelWidth,levelDepth, numberOfFloors); //Initialize the matrix for the tile placement
                }
                else //If any of the level's values are not defined or they are negative values send a Log
                {
                    if (numberOfFloors == 0) {Debug.Log("Specify number of floors");}
                    if (numberOfFloors < 0) {Debug.Log("Number of floors can't be a negative number");}

                    if (levelWidth == 0) {Debug.Log("Specify Level Width");}
                    if (levelWidth < 0) {Debug.Log("The level width can't be a negative number");}

                    if (levelDepth == 0) {Debug.Log("Specify Level Depth");}
                    if (levelDepth < 0) {Debug.Log("The level depth can't be a negative number");}

                    if (floorHeight < 0.1) {Debug.Log("Floor Height cannot be smaller than 0.1");}
                    
                    if (gridSize < 0.1) {Debug.Log("Grid Size cannot be smaller than 0.1");}
                }
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene()); //Mark the scene as dirty to avoid progress loss
            }
        }
        private void FindSpawner() //Find an existing Level spawner object in the level
        {
            if (set == false) //If a Level Spawner object has not been set
            {
                if (GUILayout.Button("Find Level Spawner", GUILayout.Width(120))) //Create a button
                {
                    //Get all the objects in the scene with a Level Spawner component
                    EditorLevelSpawner[] gameObjects = (EditorLevelSpawner[])Resources.FindObjectsOfTypeAll<EditorLevelSpawner>();
                    if (gameObjects.Length > 0)
                    {
                        //Find all the spawners in the scene and select the first one
                        //Delete all the remaining Spawners after setting the first one up
                        levelSpawner = (EditorLevelSpawner)gameObjects[0];
                        if (gameObjects.Length > 1)
                        {
                            for (int i = 1; i < gameObjects.Length; i++)
                            { //Destroy every found object except the first one
                                UnityEngine.Object.Destroy(gameObjects[i].gameObject);
                            }
                        }
                        //Set the number of floors as the value set in the found Level Spawner
                        numberOfFloors = levelSpawner.floors;
                        //Set the level width as the value set in the found Level Spawner
                        levelWidth = levelSpawner.width;
                        //Set the level depth as the value set in the found Level Spawner
                        levelDepth = levelSpawner.height;
                        //Set the floors height as the value set in the found Level Spawner
                        floorHeight = levelSpawner.floorHeight;
                        //Set the grid size as the value set in the found Level Spawner
                        gridSize = levelSpawner.gridSize;
                        Debug.Log("Found Level Spawner"); //Send a log
                        levelSpawner.UpdateTileset(tileSet); //Update the Tileset on the Level Spawner
                        set = true; //A Level Spawner has been set
                        //Mark scene as dirty to avoid progress loss
                        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                    }
                    else
                    {
                        //Send log if there are no Level Spawners in the Scene
                        Debug.Log("No Level Spawner Found");
                    }
                    
                }
            }
        }
        private void ParentFloors(GameObject parent) //Create containers for every floor and parent them to the level spawner
        {
            //Get the Prefab Material for the Combiners
            Material mat = AssetDatabase.LoadAssetAtPath<Material>("Assets/com.ionlara.dtilemap/Resources/Materials/MAT_Floor.mat");
            GameObject[] floors = new GameObject[numberOfFloors]; //Create new array to hold every floor
            //Create a Child Object for each of the floors///////////////
            //Add a Combiner Component to each of the floor Objects//////
            for (int i = 0; i < floors.Length; i++)
            {
                floors[i] = new GameObject(); //Create new empty game object and give its reference to the array
                //Set the object's position to its corresponding height
                floors[i].transform.position = new Vector3(0,i*floorHeight,0); 
                floors[i].name = "Floor " + (i + 1); //Set the object's name
                floors[i].transform.parent = parent.transform; //Parent the object to the Level Spawner object
            }
            ///////////////////////////////////////////////////////////////
            //Set the floors as the Level Spawner Areas
            parent.gameObject.GetComponent<EditorLevelSpawner>().SetAreas(levelWidth, levelDepth, numberOfFloors, floors, floorMesh);
            currentFloor = 1; //Initialize current floor
            ////////////////////////////////////////////////////////////
        }
        private void InitializeTileset() //Initialize the Tileset to the Default Tiles
        {
            tileSet.Clear(); //Clear the Tileset
            TileValues floor = new TileValues();
            #region Floor Tile
            //Set the default Floor Tile
            //Load the self tiling floor prefab from the resources
            GameObject temp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/com.ionlara.dtilemap/Resources/Prefabs/ModularFloor.prefab");
            floor = new TileValues(); //Create TileValues for the values
            floor.prefab = temp; // Set the tile prefab
            floor.prefabName = "Floor"; //Set the tile name
            floor.canRotate = false; //Restrict rotation for the tile
            //Get the floor icon from the resources
            floor.image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/com.ionlara.dtilemap/Resources/Icons/ICO_Floor.png");
            floor.isFloor = true; //Set the tile to behave as a floor
            floor.isTriangle = false; //Set the tile to not behave as a triangle
            floor.eraser = false; //Set the tile to not behave as an eraser
            tileSet.Add(floor); //Add the new tile to the tileset
            selectedObject = tileSet[0]; //Set the Floor as the current Tile
            updated = false; //Set updated as false to update the shown values in the window
            defaultTile = true;
            #endregion
            #region Diagonal Floor
            //Set the Diagonal Floor Tile
            //Load the self tiling diagonal floor from the resources
            temp = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/com.ionlara.dtilemap/Resources/Prefabs/ModularFloor-Diagonal.prefab");
            floor = new TileValues(); //Create a new TileValues for the values
            floor.prefab = temp; //Set the tile prefab
            floor.prefabName = "Diagonal Floor"; //Set the Tile name
            floor.canRotate = true; //Allow the tile to be rotated
            //Load the Diagonal Floor icon from the resources
            floor.image = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/com.ionlara.dtilemap/Resources/Icons/ICO_TriangleFloor.png");
            floor.isFloor = true; //Set the tile to behave as a floor
            floor.isTriangle = true; //Set the tile to behave as a triangle
            floor.eraser = false; //Set the tile to not behave as am eraser
            tileSet.Add(floor); //Add the tile to the tileset
            #endregion
            if (levelSpawner != null)
            { //Update the Tileset in the level spawner if it is not null
                levelSpawner.UpdateTileset(tileSet);
            }
        }
        private void ResetBuilder() //Reset the Values for a new level
        {
            StopEditing(); //Get out of Edit Mode
            //Reset all the values for a new Level
            numberOfFloors = 0;
            levelWidth = 0;
            levelDepth = 0;
            set = false;
            currentFloor = 1;
        }
        private GameObject CreateGrid() //Create the grid for the Edit Mode
        {
            GameObject parent = new GameObject();
            parent.name = "Grid"; //Create the Grid Parent Object
            //Create the Lines along the X Axis////////////////////////////////////
            float size = levelSpawner.gridSize;
            for (int i = 0; i <= levelWidth; i++)
            {
                GameObject temp = new GameObject();
                temp.hideFlags = HideFlags.HideInHierarchy;//Hide in the Hierarchy
                temp.transform.parent = parent.transform;//Parent to the Grid
                temp.name = "X-" + i; //Set the name
                LineRenderer line = temp.AddComponent<LineRenderer>();//Spawn the Line
                line.useWorldSpace = false;//Set so that it will move with the parent object
                line.material = new Material(Shader.Find("Unlit/Color"));//Add Material
                Vector3[] positions = new Vector3[2]{new Vector3(i*size,0,0), new Vector3(i*size,0,levelDepth*size)};//Set positions
                line.SetPositions(positions);
                line.startWidth = 0.05f; //Set Line Width
                line.endWidth = line.startWidth;

            }
            ///////////////////////////////////////////////////////////////////////
            //Create the Lines along the Z Axis////////////////////////////////////
            for (int i = 0; i <= levelDepth; i++)
            {
                GameObject temp = new GameObject();
                temp.hideFlags = HideFlags.HideInHierarchy; //Hide in Hierarchy
                temp.transform.parent = parent.transform; //Parent to the Grid
                temp.name = "Z-" + i;
                LineRenderer line = temp.AddComponent<LineRenderer>(); //Spawn the line
                line.useWorldSpace = false; //Set so that it will move with the parent object
                line.material = new Material(Shader.Find("Unlit/Color")); //Add Material
                Vector3[] positions = new Vector3[2]{new Vector3(0,0,i*size), new Vector3(levelWidth*size,0,i*size)}; //Set positions
                line.SetPositions(positions);
                line.startWidth = 0.05f; //Set Line Width
                line.endWidth = line.startWidth;
                
            }
            ///////////////////////////////////////////////////////////////////////
            return parent; //Return the Grid Parent Object         
        }
        #endregion
    
        #region Editing
        private void StartEditing() //Start Edit Mode
        {
            //If the settings for the self tiling floor are empty then set them as the default
            if (floorMesh == null)
            {
                //Set the current settings path as the default path
                currentSettings = "Assets/com.ionlara.dtilemap/Runtime/FloorMesh/GrassFloor.asset";
                //Load the default settings from the resources
                floorMesh = AssetDatabase.LoadAssetAtPath<FloorMesh>(currentSettings);
                setFloorMesh = floorMesh; //Update the check
                EditorPrefs.SetString("floorSettings", currentSettings); //Update the Editor preferences
                levelSpawner.floorMesh = floorMesh;
            }
            editing = true;
            //Set the Level Spawner as the Selected Object so that user can start adding tiles
            Selection.objects = new GameObject[1]{levelSpawner.gameObject}; //Set the selection as the Level Spawner object
            levelSpawner.editing = true; //Set the state as editing
            grid = CreateGrid(); //Create the Grid on the Scene
            grid.gameObject.hideFlags = HideFlags.HideInHierarchy; //Hide the Grid object to avoid slecting it
            gridCollider = new GameObject(); //Set a new object for the Collider on the grid
            gridCollider.name = "Grid Collider"; //Set the name of the Grid Collider
            gridCollider.hideFlags = HideFlags.HideInHierarchy; //Hide Collider Object to avoid selecting it
            gridCollider.transform.parent = grid.transform; //Parent Collider on the Grid
            gridCollider.AddComponent<BoxCollider>().isTrigger = true; //Add Box collider for Raycasting
            //Set the Collider object position and scale so that it matches the grid
            gridCollider.transform.position = (new Vector3((levelWidth*gridSize*0.5f),-0.5f,(levelDepth*gridSize*0.5f)));
            gridCollider.transform.localScale = (new Vector3(levelWidth*gridSize,1f ,levelDepth*gridSize));
            levelSpawner.grid = gridCollider; //Set the Grid Variable on the Level Spawner as the spawned Grid
            levelSpawner.selectedTile = selectedObject; //Set selected Tile in the Level Spawner
            levelSpawner.DisableColliders(); //Disable the colliders of the created tiles to allow raycasting
        }
        private void StopEditing() //Stop Edit Mode
        {
            //If the Grid still exists destroy it 
            if (grid != null)
            {
                DestroyImmediate(grid);
            }
            //If the reference to the Level Spawner still exists, exit edit mode in it and Enable tiles colliders
            if (levelSpawner != null)
            {
                levelSpawner.editing = false;
                levelSpawner.EnableColliders();
            }
            editing = false; //Stop the Editing state
        }
        private void DisplayCurrentFloor() //Show the current floor and allow the user to change it
        {
            GUILayout.Label("Current Floor : " + currentFloor, EditorStyles.boldLabel); //Show a label with the current floor
            GUILayout.BeginHorizontal(); //Start a line
            //Create a button to decrease the current floor, it will only decrease it if the current floor is bigger than 1
            if (GUILayout.Button("<") && currentFloor > 1)
            {
                levelSpawner.currentHeight--; //Decrease by one the current floor
            }
            //Create a button to increase the current floor, it will only increase it if the current floor is smaller than the total of floors
            if (GUILayout.Button(">") && currentFloor < numberOfFloors)
            {
                levelSpawner.currentHeight++; //Increase by one the current floor
            }
            GUILayout.EndHorizontal(); //End the line
            //If the current floor is above 0, there is a reference to the level spawner and the user is not on edit mode 
            if (currentFloor > 0 && levelSpawner != null && editing == false)
            {
                levelSpawner.currentHeight = currentFloor - 1; //Update the Level Spawner current floor
            }
        }
        #endregion    

        #region Tiles
        private void ShowTiles() //Show the user's current tileset on the window
        {
            if (tileSet.Count > 0 && levelSpawner != null) //If there is at least one tile
            {
                //Create a foldout for the tileset
                showTileset = EditorGUILayout.Foldout(showTileset,"Tiles(" + tileSet.Count + "):", boldFoldout);
                if (showTileset == true)
                {
                    int count = 0; //Counter for keeping only 3 Tiles on each line
                    GUILayout.BeginHorizontal(); //Start a line
                    for (int i = 0; i < tileSet.Count; i++)
                    {
                        GUILayout.BeginVertical(); //In a vertical space include the tile name and its icon as a button
                        if (GUILayout.Button(tileSet[i].image, GUILayout.Width(70), GUILayout.Height(70))) //Create the icon button
                        {
                            //If the button is pressed////////////////////
                            selectedObject = tileSet[i]; //Set tile as selected
                            levelSpawner.selectedTile = tileSet[i]; //Update Level Spawner current tile
                            levelSpawner.currentIsFloor = tileSet[i].isFloor; //Set if the current tile is a floor
                            updated = false; //Set to update the shown values in the window
                            //////////////////////////////////////////////
                        }
                        GUILayout.Label(tileSet[i].prefabName); //Create label with the tile name
                        GUILayout.EndVertical();//End the vertical Space
                        count++; //Count one more tile on this line
                        if (count == 3) //In case that 3 tiles already exist in this line
                        {
                            //End current line and start a new one underneath
                            GUILayout.EndHorizontal();
                            GUILayout.BeginHorizontal();
                            count = 0; //Reset the tiles count
                        }
                    }
                    AddNewTile(); //Show the area to add a new tile
                    GUILayout.EndHorizontal();//End the last line created
                }
            }
        }
        private void CurrentTilePreview() //Show the properties of the currently selected tile
        {
            if (updated == false)
            { //Check if the shown information is updated to the currently selected Tile
                tileName = selectedObject.prefabName; // Set the Tile name
                setName = tileName; //Variable to check changes in the name
                tileIcon = selectedObject.image; //Set the Tile image
                setIcon = tileIcon; //Variable to check for changes in the image
                tileRotate = selectedObject.canRotate; //Set if the Tile can be rotated
                setRotate = tileRotate; //Variable to check for changes in the "can be rotated" bool
                tileIsFloor = selectedObject.isFloor; //Set if the Tile is a floor
                setIsFloor = tileIsFloor; //Variable to check for changes in the "is floor" bool
                tileNeedsFloor = selectedObject.needsFloor; //Set if the Tile needs a floor
                setNeedFloor = tileNeedsFloor; //Variable to check for changes in the "needs floor" bool
                if(selectedObject.prefab.TryGetComponent<MeshRenderer>(out MeshRenderer mr) == true)
                { //If the selected Tile has a Mesh Renderer, get its material
                    tileMat = mr.sharedMaterial;
                    setMat = tileMat;
                    needMat = true; //Set so that the material can be changed
                }
                else
                { //If the Tile doesn't have a Mesh Renderer set the references as null
                    tileMat = null;
                    setMat = null;
                    needMat = false; //Set so that there is no way to change the material
                }
                //Check if the currently selected tile is any of the Default Tiles/////////////
                if (selectedObject == tileSet[0] || selectedObject == tileSet[1])
                { 
                    defaultTile = true;
                }
                else
                {
                    defaultTile = false;
                }
                ////////////////////////////////////////////////////////////////////////////////
                updated = true; //Set updated as true so it won't keep updating
            }
            GUILayout.Label("Selected Tile:", EditorStyles.boldLabel); //Title Label
            GUILayout.BeginVertical(); //Start vertical area for the Image and name of the tile
            GUILayout.Box(selectedObject.prefabName); //Show the selecetd Tile name
            GUILayout.Box(selectedObject.image, GUILayout.Width(85), GUILayout.Height(85)); //Show a box with the Tile image
            GUILayout.EndVertical(); //End the area for the Tile's image and name

            showTileVal = EditorGUILayout.Foldout(showTileVal, "Tile Settings"); 
            if (showTileVal == true) //Show a Dropdown with the currently selected Tile Settings
            {
                tileValSpace = 145; //If the current tile values are being show, add extra space on the window
                //Disable all the settings if the selected tile is a default tile, so that the user doesn't edit the values
                EditorGUI.BeginDisabledGroup(defaultTile);
                GUILayout.BeginVertical(); //Begin the vertical area with the settings
                tileName = EditorGUILayout.TextField("Tile Name", tileName); //Field to change the name
                if (setName != tileName) //Check for changes in the field
                {
                    selectedObject.prefabName = tileName; //Updated the Tile name in the Tileset
                    setName = tileName; //Update the check
                }
                tileRotate = EditorGUILayout.Toggle("Can be rotated", tileRotate); //Field to change if the Tile can rotate
                if (setRotate != tileRotate) //Check for changes in the field
                {
                    selectedObject.canRotate = tileRotate; //Update the Tile variable in the Tileset
                    setRotate = tileRotate; //Update the check
                }
                tileIsFloor = EditorGUILayout.Toggle("Is a Floor Tile", tileIsFloor); //Field to change if the Tiles is a floor
                if (setIsFloor != tileIsFloor)//Check for changes in the field
                {
                    selectedObject.isFloor = tileIsFloor; //Update the Tile variable in the Tileset
                    setIsFloor = tileIsFloor; //Update the check
                }
                EditorGUI.BeginDisabledGroup(tileIsFloor); //Disable the "Needs to be on floor" field if "Is a Floor Tile" is true
                tileNeedsFloor = EditorGUILayout.Toggle("Needs to be on floor", tileNeedsFloor); //Field to change if the Tile needs to be on a floor
                if (setNeedFloor != tileNeedsFloor) //Check for changes on the field
                {
                    selectedObject.needsFloor = tileNeedsFloor; //Update the Tile variable in the Tileset
                    setNeedFloor = tileNeedsFloor; //Update the check
                }
                EditorGUI.EndDisabledGroup();//////////////////////////////////////////////////////////////////////////////////////////
                if (needMat == true) //Only display the options to change the material if the Tile needs a material
                {
                    //Field to change the Tile's material
                    tileMat = (Material)EditorGUILayout.ObjectField("Tile Material", tileMat,typeof(Material),false);
                    if (setMat != tileMat) //Check for changes in the material
                    {
                        //Check if the image field will need to be changed, this will only be needed if the tile's image is the Asset Preview
                        if (tileIcon == AssetPreview.GetAssetPreview(selectedObject.prefab)) {change = true;}
                        else{change = false;}
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////
                        //Set the Tile's materia as the new chosen material
                        selectedObject.prefab.GetComponent<MeshRenderer>().sharedMaterial = tileMat;
                        setMat = tileMat; //Update the check
                        Selection.objects = new GameObject[1]{selectedObject.prefab}; //Select the tile's prefab on the inspector
                        string path = AssetDatabase.GetAssetPath(selectedObject.prefab); //Get the prefab's path
                        //Get a direct reference to the prefab in the project folders
                        GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(path,typeof(GameObject));
                    }
                }
                //Field to change the Tile icon
                tileIcon = (Texture2D)EditorGUILayout.ObjectField("Tile Icon", tileIcon, typeof(Texture2D), false);
                if (setIcon != tileIcon) //Check if the field changed
                { 
                    selectedObject.image = tileIcon; //Update the Tile image in the Tileset
                    setIcon = tileIcon; //Update the check
                }
                EditorGUI.EndDisabledGroup();
                GUILayout.EndVertical(); //End the vertical area with the Tile's settings
                //Check if the Tile's image needs to be changed and if the current asset preview has changed
                if (change == true && tileIcon != AssetPreview.GetAssetPreview(selectedObject.prefab))
                { //When the prefab starts to load the new asset preview, start checking for it to finish
                    if (AssetPreview.IsLoadingAssetPreview(selectedObject.prefab.GetInstanceID())==true) 
                    { 
                        tileIcon = null;
                    }
                    if (tileIcon == null && AssetPreview.IsLoadingAssetPreview(selectedObject.prefab.GetInstanceID())==false) 
                    { //When the new asset preview has been loaded, then update the image and update the check
                        tileIcon = AssetPreview.GetAssetPreview(selectedObject.prefab);
                        change = false;
                        if (editing == true)
                        { //If the user is in edit mode, select the Level Spawner to keep editing
                            Selection.objects = new GameObject[1]{levelSpawner.gameObject}; //Select 
                        }
                    }
                }
                if (defaultTile == false) //If the currently selected tile is not from the default tiles, then allow the user to delete it
                { 
                    if (GUILayout.Button("Set Asset Preview as Icon")) //Show button to set Asset Preview as icon
                    {
                        tileIcon = AssetPreview.GetAssetPreview(selectedObject.prefab); //Set the asset preview as the Tile icon
                        selectedObject.image = tileIcon;
                        setIcon = tileIcon; //Update the check
                    }
                    if (GUILayout.Button("Delete Tile")) //Show the Delete Tile Button
                    {
                        bool confirm = false; //Bool to confirm if the tile should be deleted
                        //Show a pop up window to confirm with the user if the tile should be deleted
                        confirm = EditorUtility.DisplayDialog("Delete Tile?","Do you want to delete this Tile?","Continue","Cancel");
                        if (confirm == true)
                        { //If the user chooses to continue then delete the tile
                            for (int i = 0; i < tileSet.Count; i++)
                            {
                                if (tileSet[i] == selectedObject) //Find the currently selected tile index
                                {
                                    selectedObject = tileSet[i-1]; //Select the tile below the deleted one in the tileset
                                    tileSet.Remove(tileSet[i]); //Remove the tile
                                    updated = false; //Set to update the values of the current tile
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                tileValSpace = 0; //If the current tile values are not being shown, then don't add extra space
            }
        }
        private void AddNewTile() //Create a box for the user to drag and drop a new tile
        {
            //Square to add a new Tile
            Rect myRect = GUILayoutUtility.GetRect(70, 70, GUILayout.ExpandWidth(false)); //Create the area to drop the new Tile
            GUI.Box(myRect, "Drag new Tile Object", GUI.skin.box); //Create the box with text
            if (myRect.Contains(Event.current.mousePosition)) //Check if the mouse is currently inside the created box
            {
                if (Event.current.type == EventType.DragUpdated) //Check if mouse is draging an object
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    Event.current.Use();
                }
                else if (Event.current.type == EventType.DragPerform) //Check if the mouse dragged and dropped something inside the box
                {
                    //Get every asset dragged and droped on the box
                    for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
                    {
                        Type type = DragAndDrop.objectReferences[i].GetType(); //Get the dropped asset type
                        GameObject obj = null;
                        if (type == typeof(GameObject)) //If the asset is a GameObject
                        {
                            obj = (GameObject)DragAndDrop.objectReferences[i]; //Get the reference to the selected assets

                            if (obj.scene.IsValid())
                            { //If the object is found inside of the scene then make a prefab from it to avoid issues if the...
                              //object in the scene is destroyed
                                GameObject scene = obj; //Save a reference to the object
                                string path = "Assets/" + scene.name + ".prefab"; //Create the path for the prefab
                                string unique = AssetDatabase.GenerateUniqueAssetPath(path); //Create unique path
                                obj = PrefabUtility.SaveAsPrefabAsset(scene, unique); //Save as a prefab on the Assets folder
                                Debug.Log("Object came from scene, added prefab in Assets Folder"); //Send a Log
                            }
                        }
                        else if (type == typeof(Mesh)) //If the asset is a Mesh
                        { //Create and save a prefab with the mesh
                            GameObject temp = new GameObject(); //Create new Game Object
                            Mesh mesh = (Mesh)DragAndDrop.objectReferences[i]; //Get the mesh reference
                            string path = AssetDatabase.GetAssetPath(mesh); //Get the mesh path 
                            string fileName = Path.GetFileName(path); //Get the name of the asset inside the project
                            path = path.Replace(fileName, mesh.name + ".prefab"); //Change the name to be able to create a prefab
                            string unique = AssetDatabase.GenerateUniqueAssetPath(path); //Create unique path
                            //Add a Mesh Filter to the object and set the mesh in it
                            temp.AddComponent<MeshFilter>().mesh = (Mesh)DragAndDrop.objectReferences[i];
                            //Add a Mesh Renderer to the object and save a reference to it 
                            MeshRenderer mr = temp.AddComponent<MeshRenderer>();
                            temp.name = mesh.name; //Set the tile name to the mesh name
                            if (mr.sharedMaterial == null)
                            { //If the mesh doesn't have a material, add the default material by creating a primitive...
                              //and getting its material. This will avoid having issues when using different rendering pipelines
                                GameObject prim = GameObject.CreatePrimitive(PrimitiveType.Quad); //Create primitive
                                prim.SetActive(false); //Set it as inactive
                                Material defMat = prim.GetComponent<MeshRenderer>().sharedMaterial; //Get the primitive's material
                                mr.sharedMaterial = defMat; //Set the material from the primitive to the Mesh Renderer
                                UnityEngine.Object.DestroyImmediate(prim); //Destroy the primitive
                            }
                            obj = PrefabUtility.SaveAsPrefabAsset(temp, path); //Save the prefab
                            UnityEngine.Object.DestroyImmediate(temp); //Destroy the created Game Object from the scene
                        }
                        else
                        { //If the dropped asset is neither a Game Object or a Mesh, then it can't be added as a tile
                            Debug.Log("You can only add new Tiles from GameObjects and Meshes!"); //Send Log
                        }
                        if (obj != null) //If there is an object to add as a Tile add it to the Tileset
                        {
                            TileValues temp = new TileValues(); //Create new Tile
                            temp.prefab = obj; //Set the Tile's object
                            temp.prefabName = obj.name; //Set the Tile's name
                            while (temp.image == null)
                            { //Get the asset's preview to use as the Tile's icon. 
                              //Use a while loop to make sure that the preview is found, since it might look for it before it is created
                                temp.image = AssetPreview.GetAssetPreview(temp.prefab);
                            }
                            tileSet.Add(temp); //Add the Tile to the Tileset
                            levelSpawner.UpdateTileset(tileSet); //Update the tilest on the Level Spawner
                        }
                    }

                    Event.current.Use();
                }
            }
        }
        private void SaveTileSet() //Save the current tileset as a ScriptableObject
        {
            //Spawn a save window for the user to select where to save the tileset
            string path = EditorUtility.SaveFilePanelInProject("Save Tileset", "NewTileset", "asset", "Enter a name");
            if (path.Length != 0) //If a path was selected save the tilest
            {
                string unique = AssetDatabase.GenerateUniqueAssetPath(path); //Create an unique path to avoid overlapping names
                Tileset temp = ScriptableObject.CreateInstance<Tileset>(); //Create a new instance of the scriptable object
                temp.tileset = new List<TileValues>(); //Create a new list for the instance
                for (int i = 0; i < tileSet.Count; i++) //For every tile in the tileset
                {
                    TileValues tile = CopyTileValues(i,tileSet); //Copy the value of the tileset
                    temp.tileset.Add(tile); //Add the copied value to the scriptable object
                }
                AssetDatabase.CreateAsset(temp,unique); //Save the scriptable object inside the project assets on the chosen path
            }
        }
        private void LoadTileSet() //Load a previously saved tileset
        {
            //Show an "Open File" window to select the tileset to load
            string path = EditorUtility.OpenFilePanel("Load Tileset", "Assets/","asset");
            //Make sure that the path is set inside of project to avoid errors////
            string editor = Application.dataPath; //Get the project path
            editor = editor.Replace("Assets", ""); //Clean up the project path
            if (path.Contains(editor)) //If the chosen is not inside the project
            {
                path = path.Replace(editor,""); //Clean up the path 
            }
            //////////////////////////////////////////////////////////////////////
            {
                Tileset temp = AssetDatabase.LoadAssetAtPath<Tileset>(path); //Get a reference to the chosen tileset
                if (temp != null) //If the asset does exist and is a Tileset
                {
                    tileSet.Clear(); //Clear the current Tileset
                    for (int i = 0; i < temp.tileset.Count; i++) //For every tile in the loaded tileset
                    {
                        TileValues tile = CopyTileValues(i,temp.tileset); //Copy the loaded tileset tile
                        tileSet.Add(tile); //Add the value to the current tileset
                    }
                    levelSpawner.UpdateTileset(tileSet); //Update the tileset on the Level Spawner
                }
                else //If the asset is not a tileset send an error message
                {
                    Debug.Log("File is not a Tileset");
                }
            }
        }
        private TileValues CopyTileValues(int i, List<TileValues> from) //Copy all the values from a tile and return a new tile with the values
        {
            TileValues tile = new TileValues(); //Create a new tile
            tile.prefab = from[i].prefab; //Copy the prefab
            tile.prefabName = from[i].prefabName; //Copy the name
            tile.image = from[i].image; //Copy the icon
            tile.isFloor = from[i].isFloor; //Copy if it's a floor
            tile.isTriangle = from[i].isFloor; //Copy if it's a triangle
            tile.needsFloor = from[i].needsFloor; //Copy if it needs a floor
            tile.turns = from[i].turns; //Copy how many turns it has
            tile.eraser = from[i].eraser; //Copy if it's an eraser
            tile.canRotate = from[i].canRotate; //Copy if it can rotate
            return tile; //Return the new tile with the copied values
        }
        #endregion

        #region Bake & Save
        private void BakeAndSave() //Show the options to bake and save meshes
        {
            if (GUILayout.Button("Bake Selected")) //Bake meshes from all the selected Game Objects and their children
            {
                if (Selection.gameObjects.Length > 0)
                {
                    GameObject[] selection = Selection.gameObjects; //Get the current selection of objects
                    GameObject container = new GameObject(); //Create an empty Game Object to hold the mesh that will be created
                    container.name = "Baked Mesh"; //Set the name of the new Game Object
                    Combiner combiner = container.AddComponent<Combiner>(); //Add a Combiner component to the Game Object
                    combiner.GetHierarchy(selection); //Start baking the meshes of all selected GameObjects
                    Selection.objects = new GameObject[1]{container}; //Select the GameObject with the new mesh
                }
                else
                {
                    Debug.Log("No Game Object selected"); //Send Log
                }
            }

            if (GUILayout.Button("Save Selected Baked Mesh")) //Save any baked meshes
            {
                if (Selection.gameObjects.Length > 0) //Check that at least 1 GameObject is selected
                { 
                    GameObject selected = Selection.gameObjects[0]; //Only get the first selected object
                    if (selected.TryGetComponent<Combiner>(out Combiner combiner) == true)
                    { //Check if the selected object has a Combiner
                        if (selected.TryGetComponent<MeshFilter>(out MeshFilter mf) == true)
                        { //Check if the selected object has a Mesh Filter
                            if (mf.sharedMesh != null)
                            { //Check that the Mesh Filter has a Mesh assigned
                                SaveMesh(mf.sharedMesh); //Save the mesh present in the object Mesh Filter
                            }
                            else
                            {
                                Debug.Log("No mesh present in object!"); //Send error log
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("You can only save baked meshes!"); //Send error log
                    }
                }
                else
                {
                    Debug.Log("No Game Object Selected!"); //Send error log
                }
            }
        }
        
        private void SaveMesh(Mesh mesh) //Save a Mesh inside of the project
        {
            //Show a Save File Window to choose the Asset Save Path
            string path = EditorUtility.SaveFilePanelInProject("Save Mesh", mesh.name,"asset", "Enter a file name");
            if (path.Length > 0) //If the user chose a path
            {
                //Make sure that the path is not repeated
                string unique = AssetDatabase.GenerateUniqueAssetPath(path);
                if (AssetDatabase.Contains(mesh) == false)
                {
                    //If the project does not have a mesh saved with the same name Save it
                    AssetDatabase.CreateAsset(mesh, unique);
                }
                else //If the project already has a mesh saved with the same name
                {
                    //Copy the pre-existing asset and paste it into the chosen path
                    AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(mesh), unique);
                    //Get a reference to the copy
                    Mesh copy = AssetDatabase.LoadAssetAtPath<Mesh>(unique);
                    //Change the mesh of the copy for the new mesh
                    copy = mesh;
                    //Update the copy name
                    copy.name = Path.GetFileName(unique);
                }
            }
        }
        #endregion
    }

}
