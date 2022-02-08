using System.Collections.Generic;
using UnityEngine;

namespace TileMap3D
{
    /// <summary>
    /// The Editor Level Spawner Class is the one in charge of creating and deleting the different tiles, as well as keeping track of 
    /// all the created tiles.
    /// </summary>
    public class EditorLevelSpawner : MonoBehaviour
    {
        #region Variables
        public TileValues[] tileValues; //The current tileset
        public TileValues selectedTile; //Currently selected Tile
        public bool currentIsFloor; //If the currently selected Tile is set as a floor
        public bool editing = false; //If user is currently editing the level
        public GameObject grid; //The grid spawned on edit mode
        public float gridSize; //The size of each tile on the grid

        public int width; //Current Level Width
        public int height; //Current level depth
        public int floors{get; private set;} = 5; //Current level floors(levels)
        public float floorHeight; //The height of each floor(level)
        public int currentHeight; //The current floor being edited

        public Matrices matrices; 

        public int[] tilesCount = new int[5]; //How many tiles have been spawned in each floor
        public GameObject[] areas = new GameObject[5]; //The game objects containing each floor
        public bool[] bakedFloors; //Floors that have already been baked
        private List<Collider> colliderList = new List<Collider>(); //The colliders on every spawned tile

        public FloorMesh floorMesh;

        private string excep = "Floor Settings are missing ";
        #endregion

        #region Initializing
        //Set the children objects that will contain the spawned tiles
        public void SetAreas(int _width, int _depth, int _height,GameObject[] items, FloorMesh options)
        {
            width = _width; //Set the width of the level
            height = _depth; //Set the depth of the level
            floors = _height; //Set the height(floors) of the level
            areas = new GameObject[items.Length]; //Initialize the object array
            floorMesh = options;
            tilesCount = new int[floors]; //Initialize the int array
            //Get the reference to the containers
            for (int i = 0; i < items.Length; i++) 
            {
                areas[i] = items[i];
                tilesCount[i] = 0;
            }
            // InitializeMatrix(_width,_depth,_height); //Initialize Matrices with level metrics
            //Initialize and set to false the Array with the height(floors) of the level
            bakedFloors = new bool[items.Length]; 
            for (int i = 0; i < bakedFloors.Length; i++)
            {
                bakedFloors[i] = false;
            }
            ////////////////////////////////////////////////////////////////////////////
            colliderList = new List<Collider>(); //Initialize the Colliders list
            colliderList.Clear();
        }
        //Initialize the Matrices with the level values
        public void InitializeMatrix(int _width, int _depth, int height)
        {
            matrices = new Matrices();
            matrices.Initialize(_width, _depth, height);

            Vector3 scale = new Vector3(gridSize, floorHeight, gridSize);
            tileValues[0].prefab.transform.localScale = scale;
            tileValues[1].prefab.transform.localScale = scale;
        }
        #endregion

        #region Colliders
        private void DisableCollider(GameObject spawned) //Disable the collider of a single spawned object
        {
            if (spawned.TryGetComponent<Collider>(out Collider collider) == true)
            {
                //If the object has a collider add it to the colliders list and disable it
                colliderList.Add(collider);
                collider.enabled = false;
            }
            for (int i = 0; i < spawned.transform.childCount; i++)
            {
                //Disable the colliders on all the child objects
                DisableCollider(spawned.transform.GetChild(i).gameObject);
            }
        }
        public void DisableColliders() //Disable all the colliders saved on the colliders list
        {
            for (int i = 0; i < colliderList.Count; i++)
            {
                if (colliderList[i] != null)
                {
                    colliderList[i].enabled = false;
                }
            }
        }
        public void EnableColliders() //Enable all the colliders saved on the colliders list
        {
            for (int i = 0; i < colliderList.Count; i++)
            {
                if (colliderList[i] != null)
                {
                    if (colliderList[i].gameObject.TryGetComponent<Floor>(out Floor floor) == true)
                    { //If the tile has a Floor component, check if the tile is blocked completely
                        if (floor.blocked == false)
                        {
                            colliderList[i].enabled = true;
                        }
                    }
                    else
                    {
                        colliderList[i].enabled = true;
                    }
                }
            }
        }
        #endregion
        //Handles the creation of the selected tile in the selected grid square
        public void Create(Vector3 spawnPoint, Vector3 tileIndex)
        {
            ExceptionCatching(); //Check every mesh in the Floor Settings to look for null entries
            if (bakedFloors[currentHeight] == false) //Check if the currently editing height(floor) has been baked
            {
                if (selectedTile.prefab != null)//Create a new tile
                {
                    //Instantiate the currently selected tile on the current grid position and save a reference
                    GameObject temp = Instantiate(selectedTile.prefab, new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z), Quaternion.identity, transform.GetChild(currentHeight));
                    DisableCollider(temp); //Disable the collider of spawned tile
                    int x = Mathf.FloorToInt(tileIndex.x - 1); //Save the grid position on x
                    int z = Mathf.FloorToInt(tileIndex.z - 1); //Save the grid position on z
                    #region Spawned Floor
                    if (selectedTile.isFloor == true)
                    {
                        matrices.SetTile(x, z, currentHeight, true); //Update matrix value
                        // matrices.boolMatrix[x,z,currentHeight] = true;
                        if (matrices.GetTileObject(x, z, currentHeight) != null)
                        {
                            // Destroy any previously existing tile on that position
                            DeleteTile(tileIndex);
                        }
                        matrices.SetTileObject(x, z, currentHeight, temp);
                        //Check if Tile has Floor Class Component
                        if (temp.TryGetComponent<Floor>(out Floor floor) == true)
                        {
                            //Set the information for auto tiling on the Floor Class Component
                            floor.SetInfo(this, new Vector3(x, z, currentHeight), areas.Length, areas[currentHeight], selectedTile.turns, floorMesh);
                            floor.GetNeighbours(this, false); //Set floor meshes
                        }
                        else
                        {
                            //If the tile is not a self tiling floor rotate it to its set rotation
                            temp.transform.rotation = Quaternion.Euler(0, 90 * selectedTile.turns, 0);
                        }
                        tilesCount[currentHeight]++; //Increase by 1 the count of tiles for this height(floor)
                    }
                    #endregion
                    #region Spawned Object
                    else
                    {
                        //Set the tile to the chosen rotation
                        temp.transform.rotation = Quaternion.Euler(0, 90 * selectedTile.turns, 0);
                        if (matrices.GetObject(x, z, currentHeight) != null)
                        {
                            //Destroy any object previously on that location
                            DestroyImmediate(matrices.GetObject(x, z, currentHeight));
                            tilesCount[currentHeight]++; //Increase by 1 the count of tiles for this height(floor)
                        }
                        matrices.SetObject(x, z, currentHeight, temp);
                        // objectMatrix[x,z,currentHeight] = temp; //Set object reference in matrix
                        // matrices.objectMatrix[x,z,currentHeight] = temp;
                        //Check if objects needs floor tile below it and if there is already a floor underneath
                        if (selectedTile.needsFloor == true && matrices.GetTileObject(x, z, currentHeight) == null)
                        {
                            matrices.SetTile(x, z, currentHeight, true);
                            // matrix[x,z,currentHeight] = true; //Update matrix value
                            // matrices.boolMatrix[x,z,currentHeight] = true;
                            //Instantiate a floor tile under the object
                            temp = Instantiate(tileValues[0].prefab, new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z), Quaternion.identity, transform.GetChild(currentHeight));
                            DisableCollider(temp); //Disable the collider on the spawned floor tile
                            matrices.SetTileObject(x, z, currentHeight, temp);
                            // floorMatrix[x,z,currentHeight] = temp; //Update the Floor tile matrix
                            // matrices.tileMatrix[x,z,currentHeight] = temp;
                            Floor floor = temp.GetComponent<Floor>(); //Get the Floor Class Component
                            //Set the floor auto tiling info////////////////////////////////////////////////////////
                            floor.SetInfo(this, new Vector3(x, z, currentHeight), areas.Length, areas[currentHeight], 0, floorMesh);
                            floor.GetNeighbours(this, false);
                            ////////////////////////////////////////////////////////////////////////////////////////
                            tilesCount[currentHeight]++; //Increase by 1 the count of tiles for this height(floor)
                        }
                    }
                    #endregion
                }
            }
            else
            {
                //If the height(floor) has been baked send error message
                Debug.Log("Can't edit baked floor");
            }
        }
        //Destroy a previously existing tile
        public void DeleteTile(Vector3 tileIndex)
        {
            if (bakedFloors[currentHeight] == false)
            {
                //Get the user's position on the grid
                int x = Mathf.FloorToInt(tileIndex.x-1);
                int z = Mathf.FloorToInt(tileIndex.z-1);
                /////////////////////////////////////
                if (matrices.GetTileObject(x,z,currentHeight) != null)
                {
                    //If there is a floor tile on the position destroy it
                    GameObject temp = matrices.GetTileObject(x,z,currentHeight); //Get the floor tile object reference
                    Floor floor = temp.GetComponent<Floor>(); //Get the floor class component reference
                    //Update the matrices values//////////
                    matrices.SetTileObject(x,z,currentHeight,null);
                    matrices.SetTile(x,z,currentHeight,false);
                    // floorMatrix[x,z,currentHeight] = null;
                    // matrix[x,z,currentHeight] = false;
                    //////////////////////////////////////
                    GameObject[] neighs = new GameObject[10]; //Initialize an array for the tile's neighbours
                    if (floor != null) 
                    {
                        //If the destroyed tile has a floor class component... 
                        //get and update all it's registered neighbours
                        neighs = floor.neighbourObjects;
                        GameObject[] upNeighs = floor.upNeighObj;
                        GameObject[] dwnNeighs = floor.dwnNeighObj;
                        for (int i = 0; i < upNeighs.Length; i++)
                        {
                            if (upNeighs[i]!=null)
                            {
                                upNeighs[i].GetComponent<Floor>().DownNeighbourDestroyed(temp);
                            }
                            if (dwnNeighs[i]!=null)
                            {
                                dwnNeighs[i].GetComponent<Floor>().UpNeighbourDestroyed(temp);
                            }
                        }
                        for (int i = 0; i < neighs.Length; i++)
                        {
                            if (neighs[i] != null)
                            {
                                neighs[i].GetComponent<Floor>().NeighbourDestroyed(temp);
                            }
                        }
                    }
                    DestroyImmediate(temp); //Destroy the floor tile
                    tilesCount[currentHeight] --; //Decrease the tile count by 1    
                }
                if (matrices.GetObject(x,z,currentHeight) != null)
                {
                    //If there is an object on the user's position then destroy it
                    DestroyImmediate(matrices.GetObject(x,z,currentHeight));
                    matrices.SetObject(x,z,currentHeight,null);
                    // objectMatrix[x,z,currentHeight] = null; //Update the object matrix

                    tilesCount[currentHeight] --; //Decrease the tile count by 1    
                }
            }
            else
            {
                Debug.Log("Can't edit baked floor");
            }
        }
        //This method searches for any null meshes in the Floor Settings, and throws an exception if it finds any
        private void ExceptionCatching()
        {
            //Check the Square Lid Mesh
            if (floorMesh.squareLid == null) { throw new System.Exception(excep + "the Square Lid Mesh!"); }
            //Check the Square Bottom Mesh
            if (floorMesh.squareBottom == null) { throw new System.Exception(excep + "the Square Bottom Mesh!"); }
            //Check the Corner Wall Meshes
            for (int i = 0; i < floorMesh.cornerWalls.Length; i++)
            {
                if (floorMesh.cornerWalls[i] == null) { throw new System.Exception(excep + "a Corner Wall Mesh!"); }
            }
            //Check the Corner Lid Mesh
            if (floorMesh.cornerLid == null) { throw new System.Exception(excep + "the Corner Lid Mesh!"); }
            //Check the Corner Bottom Mesh
            if (floorMesh.cornerBottom == null) { throw new System.Exception(excep + "the Corner Bottom Mesh!"); }
            //Check the Straight Wall Meshes
            for (int i = 0; i < floorMesh.straightWalls.Length; i++)
            {
                if (floorMesh.straightWalls[i] == null) { throw new System.Exception(excep + "a Straight Wall Mesh!"); }
            }
            //Check the Straight Bottom Mesh
            if (floorMesh.straightBottom == null) { throw new System.Exception(excep + "the Straight Bottom Mesh!"); }
            //Check the Curved Wall Meshes
            for (int i = 0; i < floorMesh.curvedWalls.Length; i++)
            {
                if (floorMesh.curvedWalls[i] == null) { throw new System.Exception(excep + "a Curved Wall Mesh!"); }
            }
            //Check the Curved Lid Meshes
            for (int i = 0; i < floorMesh.curveLids.Length; i++)
            {
                if (floorMesh.curveLids[i] == null) { throw new System.Exception(excep + "a Curved Lid Mesh!"); }
            }
            //Check the Curved Bottom Meshes
            for (int i = 0; i < floorMesh.curveBottoms.Length; i++)
            {
                if (floorMesh.curveBottoms[i] == null) { throw new System.Exception(excep + "a Curved Bottom Mesh!"); }
            }
            //Check the Half Bottom Meshes
            for (int i = 0; i < floorMesh.halfBottoms.Length; i++)
            {
                if (floorMesh.halfBottoms[i] == null) { throw new System.Exception(excep + "a Half Bottom Mesh!"); }
            }
            //Check the Diagonal Wall Meshes
            for (int i = 0; i < floorMesh.diagonalWalls.Length; i++)
            {
                if (floorMesh.diagonalWalls[i] == null) { throw new System.Exception(excep + "a Diagonal Wall Mesh!"); }
            }
            //Check the Diagonal Lid Meshes
            for (int i = 0; i < floorMesh.diagonalLids.Length; i++)
            {
                if (floorMesh.diagonalLids[i] == null) { throw new System.Exception(excep + "a Diagonal Lid Mesh!"); }
            }
            //Check the Diagonal Bottom Meshes
            for (int i = 0; i < floorMesh.diagonalBottoms.Length; i++)
            {
                if (floorMesh.diagonalBottoms[i] == null) { throw new System.Exception(excep + "a Diagonal Bottom Mesh!"); }
            }
        }
        //Update the current tileset to match the one chosen by the user
        public void UpdateTileset(List<TileValues> list)
        {
            //Re-Initialize the tileset array to match the length of the new tilset
            tileValues = new TileValues[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                tileValues[i] = list[i]; //Set the new tiles
            }
        }
    }
}
