using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace TileMap3D
{
    /// <summary>
    /// The Level Spawner Editor Class is the one in charge of handling the user's input while on Edit Mode. It gets the mouse position
    /// in order to get the positions to spawn the new tiles at. It also handles the Tiles previews and the keyboard input for the hotkeys.
    /// </summary>
    [CustomEditor(typeof(EditorLevelSpawner))]
    public class LevelSpawnerEditor : Editor
    {
        #region Variables
        Vector3 gridPos = new Vector3(0,0,0); //The user's current position on the grid
        Vector3 tilePos = new Vector3(0,0,0); //The user's current position tilewise
        Vector3 previewRot = new Vector3(0,0,0); //The selected tile preview rotation
        GameObject prefab; //The prefab selected to preview & create
        GameObject preview = null; //The preview on the scene
        bool previewing = false; //Is the user currently previewing a tile on Edit Mode?
        EditorLevelSpawner spawner; //The scene's Level Spawner
        bool altDwn = false; //Is any of the Alt Keys beign pressed?
        bool ctrDwn = false; //Is any of the Ctrl or Command Keys being pressed?

        bool subscribed = false; //Subscribed to the selection change event?
        #endregion

        private void OnSceneGUI()
        {
            #region Subscriptions
            spawner = (EditorLevelSpawner)target; //Get a reference to the Level Spawner
            prefab = spawner.selectedTile.prefab; //Get the currently selected tile prefab
            if (subscribed == false) //Subscribe to the Selection Changed Event
            {
                Selection.selectionChanged+= StopPreview;
                subscribed = true;
            }
            if (preview != null && preview != prefab) //If the selection changes at any point, stop previewing
            {
                StopPreview();
            }
            #endregion
            Repaint();
            if (spawner.editing == true)
            {
                //Set the scene selection to the level Spawner to allow user to click on the scene
                Selection.objects = new Object[1]{spawner.gameObject};
                #region Hotkeys Detection
                if (Event.current.type == EventType.KeyDown) //Get the user's keyboard input
                {
                    //Decrease the currently edited floor if posible
                    if (Event.current.keyCode == KeyCode.LeftBracket && spawner.currentHeight > 0)
                    {
                        spawner.currentHeight --;
                        return;
                    }
                    //Increase the currently edited floor if posible
                    if (Event.current.keyCode == KeyCode.RightBracket && spawner.currentHeight < spawner.floors-1)
                    {
                        spawner.currentHeight ++;
                        return;
                    }
                }
                #endregion
                //Cast a Ray from the Scene Camera at the mouse position
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hit = new RaycastHit(); //Initialize the RaycastHit variable
                //Cast the Ray and check if it collided with the editing grid 
                if (Physics.Raycast(ray, out hit, 10000f) && hit.transform.gameObject == spawner.grid)
                {
                    Vector3 point = hit.point;
                    if (point.x > 0 && point.z > 0 && point.x < (spawner.width*spawner.gridSize) && point.z < (spawner.height*spawner.gridSize))
                    {
                        //Set the tile position based on the raycast hit position
                        tilePos.x = Mathf.Ceil(hit.point.x/spawner.gridSize);
                        tilePos.y = spawner.currentHeight;
                        tilePos.z = Mathf.Ceil(hit.point.z/spawner.gridSize);
                        //Get the coordinates based on the tile position
                        gridPos.x = ((tilePos.x*spawner.gridSize) - (spawner.gridSize*0.5f));
                        gridPos.y = spawner.currentHeight*spawner.floorHeight; //Set current height(floor) as the position on y
                        gridPos.z = ((tilePos.z*spawner.gridSize) - (spawner.gridSize*0.5f));
                        if (previewing == false)
                        { //Check if a preview has already been created
                            float size = 0.3f;
                            #region Prefab and Handles Creation
                            if (prefab != null && ctrDwn == false)
                            {
                                Handles.color = Color.grey; //Set the handle color for the preview of a tile
                                //Create a Cube Handle above the tile preview
                                Handles.CubeHandleCap(0,(gridPos + (Vector3.up/2)),Quaternion.identity, size, EventType.Repaint);

                                preview = Instantiate(prefab,gridPos,Quaternion.identity); //Instantiate the preview
                                preview.name = "Preview"; //Change the name of the preview Game Object
                                previewing = true; //Enter the previewing state
                                if (preview.TryGetComponent<Floor>(out Floor floor) == true)
                                {
                                    //If the tile has a Floor Class Component then use its default look
                                    floor.DefaultLook(spawner.floorMesh);
                                }
                            }
                            else
                            {
                                Handles.color = Color.red; //Set the handle color for erasing tiles
                                //Create a Cube Handle floating to show which tile will be erased
                                Handles.CubeHandleCap(0,(gridPos + (Vector3.up/2)),Quaternion.identity, size, EventType.Repaint);
                                preview = new GameObject(); //Create an empty object as the preview 
                                preview.name = "Preview"; //Change the object's name
                                previewing = true; //Enter the previewing state
                            }
                            #endregion
                        }
                        #region Creation & Rotation
                        if (previewing == true && preview != null)
                        {
                            SceneView.FocusWindowIfItsOpen(typeof(SceneView)); //Keep the focus on the Scene View Window
                            //Set the preview's position as the grid position taken from the Raycast Hit
                            preview.transform.position = gridPos; 
                            //Set the preview rotation on the Y axis to the one set by the user
                            previewRot.y = 90 * spawner.selectedTile.turns;
                            preview.transform.rotation = Quaternion.Euler(previewRot);
                            ///////////////////////////////////////////////////////////////////
                            Event current = Event.current; //Get the user's Input from the mouse and the keyboard
                            if (current.type == EventType.KeyDown) //Get when keys are being pressed down
                            {
                                //Rotate the preview by 90 degrees when the space bar is pressed
                                if (current.keyCode == KeyCode.Space && spawner.selectedTile.canRotate == true)
                                {
                                    if (spawner.selectedTile.turns < 3)
                                    { //If the tile has turned less than 270 degrees rotate it
                                        spawner.selectedTile.turns++;
                                    }
                                    else
                                    { //If the tile has turned 270 degrees reset it to 0
                                        spawner.selectedTile.turns = 0;
                                    }
                                    return;
                                } 
                                if (current.keyCode == KeyCode.AltGr || current.keyCode == KeyCode.LeftAlt || current.keyCode == KeyCode.RightAlt)
                                { //Check if any of the Alt keys has been pressed
                                    altDwn = true;
                                }
                                if (current.keyCode == KeyCode.LeftControl || current.keyCode == KeyCode.RightControl || current.keyCode == KeyCode.LeftCommand || current.keyCode == KeyCode.RightCommand)
                                { //Check if anu of the Ctrl or Command Keys have been pressed
                                    ctrDwn = true;
                                }
                            }
                            if (current.type == EventType.KeyUp) //Check the keys that stop being pressed
                            { 
                                if (current.keyCode == KeyCode.AltGr || current.keyCode == KeyCode.LeftAlt || current.keyCode == KeyCode.RightAlt)
                                { //Check if the any of the Alt keys have stopped being pressed
                                    altDwn = false;
                                }
                                if (current.keyCode == KeyCode.LeftControl || current.keyCode == KeyCode.RightControl || current.keyCode == KeyCode.LeftCommand || current.keyCode == KeyCode.RightCommand)
                                { //Check if any of the Ctrl or Command keys have stopped being pressed
                                    ctrDwn = false;
                                }
                                
                            }
                            if (current.type == EventType.MouseDown) //Check if the mouse has been pressed
                            {
                                if (altDwn == false) //If there is no Alt key being pressed continue
                                {
                                    if (ctrDwn == false)
                                    { //If there is no Ctrl or Command key being pressed then create the selected tile
                                        spawner.Create(gridPos, tilePos); //Spawn a tile when clicking with the mouse
                                        EditorUtility.SetDirty(spawner.gameObject); //Set the spawner object as dirty
                                        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene()); //Set scene as dirty
                                    }
                                    else
                                    { //If there is a Ctrl or Command key being pressed then delet the tile in the selected point
                                        spawner.DeleteTile(tilePos); //Delete the tile
                                        EditorUtility.SetDirty(spawner.gameObject); //Set the Spawner object as dirty
                                        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene()); //Set scene as dirty
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
            }
            //Reset previewing state if the user stops edit mode
            else if(preview != null)
            {
                DestroyImmediate(preview);
                previewing = false;
            }
            else
            {
                previewing = false;
            }
        }
        private void StopPreview()
        {
            if (preview != null)
            { //If there is a preview active Destroy the preview object and exit previewing state
                DestroyImmediate(preview);
                previewing = false;
            }
        }
    }
}
