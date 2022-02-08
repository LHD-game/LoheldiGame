using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TileMap3D
{
    /// <summary>
    /// The Floor Class is the one in charge of all the functions related to the "Self Tiling Floor" Behaviour. It gets the grid information
    /// from the Editor Level Spawner class, and uses it to define the appereance that the tile should have.
    /// </summary>
    public class Floor : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Transform[] _pieces = new Transform[4]; //The different parts of the floor  where to spawn the meshes
        //A bool array from the direct neighbours from this floor instance, getting direct contact and diagonals
        // 0 = North / 1 = West / 2 = East / 3 = South
        // 4 = North-West / 5 = North-East / 6 = South-West / 7 = South-East
        private bool[] _neighbours = new bool[8];
        //A bool array from the neighbours on the floor above from this floor instance, getting direct contact and diagonals
        // 0 = North / 1 = West / 2 = East / 3 = South
        // 4 = North-West / 5 = North-East / 6 = South-West / 7 = South-East
        private bool[] _upNeigh = new bool[8];
        //A bool array from the neighbours on the floor below from this floor instance, getting direct contact and diagonals
        // 0 = North / 1 = West / 2 = East / 3 = South
        // 4 = North-West / 5 = North-East / 6 = South-West / 7 = South-East
        private bool[] _dwnNeigh = new bool[8];
        /////////////////////////////////////////////
        public bool above{get;private set;} = false; //Is there a floor directly above this one?
        public bool below{get;private set;} = false; //Is there a floor directly below this one?
        public GameObject[] neighbourObjects = new GameObject[10]; //The Game Objects from the neighbours + above & below
        public GameObject[] upNeighObj = new GameObject[8]; //The tile above neighbours
        public GameObject[] dwnNeighObj = new GameObject[8]; //The tile below neighbours
        private bool _upBorder = false; //Is this floor at the north border of the level designated area?
        private bool _downBorder = false; //Is this floor at the south border of the level designated area?
        private bool _leftBorder = false; //Is this floor at the west edge of the level designated area?
        private bool _rightBorder = false; //Is this floor at the east border of the level designated area?
        public bool blocked = false; //Does the tile have all four adjacent neighbours as well as below and above?
        
        [SerializeField]
        public Vector3 index;//The floor(tile) position on the level's grid (x,z,y)
        [SerializeField]
        private int _xIndex; //The index x value to help serializing
        [SerializeField]
        private int _zIndex; //The index y value to help serializing
        [SerializeField]
        private int _yIndex; //The index z value to help serializing
        [SerializeField]
        private FloorMesh _parts; //The floor's designated meshes and material
        public bool isTriangle = false; //Is this floor a triangle?
        public int rotation = 0; //How many 90 degrees turns was the tile spawned with?

        [SerializeField]
        private EditorLevelSpawner _editorSpawner; //The reference to the level's Level Spawner
        [SerializeField]
        private GameObject _areaContainer; //The Game Object that contains this floor's Game Object

        private bool[,,] _levelMatrix; //The level's Matrix with value for every tile position, giving wich ones contain a floor
        [SerializeField]
        private int _mapWidth; //The level's designated width
        [SerializeField]
        private int _mapDepth; //The level's designated depth
        [SerializeField]
        private int _curFloor; //The level(floor) being occupied
        [SerializeField]
        private int _maxFloors; //The total amount of levels(floors) deignated for the level

        public enum BlockDirections //An Enumerator containing the values from the previous array to facilitate the use of the values
        {
            UpBlock = 4,
            LeftBlock = 2,
            RightBlock = -2,
            DownBlock = -1
        }
        #endregion
        
        #region Set Up
        //Get all the main information from the level 
        public void SetInfo(EditorLevelSpawner spawner, Vector3 cord, int levelFloors, GameObject area, int floorRotation, FloorMesh options)
        {
            _editorSpawner = spawner; //The level's Level Spawner
            _parts = options; //The chosen meshes and material for the self tiling floor
            rotation = floorRotation; //The user's chosen rotation
            index = cord; //The position where the Game Object was spawned
            _xIndex = (int)index.x;
            _yIndex = (int)index.y;
            _zIndex = (int)index.z;
            _mapWidth = _editorSpawner.width; //The map's Width
            _mapDepth = _editorSpawner.height; //The map's Depth
            _maxFloors = levelFloors; //The map's amount of floors(height)
            _areaContainer = area; //The Game Object that will contain this Game Object
        }
        //Get all the neighbours from this tile
        public void GetNeighbours(EditorLevelSpawner spawner, bool fromFloor)
        {
            index.x = _xIndex;
            index.y = _yIndex;
            index.z = _zIndex;
            // _levelMatrix = spawner.matrix; //Copy the Matrix values from the Level Spawner's Matrix
            _curFloor = (int)index.z; //Get the current floor from the index coordinates
            GetBorders(); //Check if the Floor Tile is placed at any of the edges of the map
            
            if (_upBorder == false) //Check for the North Neighbour
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x, (int)index.y + 1, _curFloor);
                if (temp != null && temp.TryGetComponent<Floor>(out var floor) == true)
                { //If there is a neighbour and it has a Floor component, add it to the Objects Array
                    neighbourObjects[0] = temp;
                    CheckForTriangles(temp, 0, 0); //Check if the tile has adjacent walls to this tile
                }
                //Check if there is a North neighbour above and below
                GetNeighbours(_upNeigh, upNeighObj, 0,(int)index.x,(int)index.y+1,_curFloor+1);
                GetNeighbours(dwnNeighObj, 0,(int)index.x,(int)index.y+1,_curFloor-1);
            }
            if (_leftBorder == false) //Check for the West Neighbour
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x-1, (int)index.y, _curFloor);
                if (temp != null && temp.TryGetComponent<Floor>(out var floor) == true)
                { //If there is a neighbour and it has a floor Component, add it to the Objects Array
                    neighbourObjects[1] = temp;
                    CheckForTriangles(temp,1,3); //Check if the tile has adjacent walls to this tile
                }
                //Check if there is a West neighbour above and below
                GetNeighbours(_upNeigh, upNeighObj, 1,(int)index.x-1,(int)index.y,_curFloor+1);
                GetNeighbours(dwnNeighObj, 1,(int)index.x-1,(int)index.y,_curFloor-1);
            }
            if (_rightBorder == false) //Check for the East Neighbour
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x+1, (int)index.y, _curFloor);
                if (temp != null && temp.TryGetComponent<Floor>(out var floor) == true)
                { //If there is a neighbour and it has a floor Component, add it to the Objects Array
                    neighbourObjects[2] = temp;
                    CheckForTriangles(temp,2,1); //Check if the tile has adjacent walls to this one
                }
                //Check if there is an East neighbour above and below
                GetNeighbours(_upNeigh, upNeighObj, 2,(int)index.x+1,(int)index.y,_curFloor+1);
                GetNeighbours(dwnNeighObj, 2,(int)index.x+1,(int)index.y,_curFloor-1);
            }
            if (_downBorder == false) //Check for the South Neighbour
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x, (int)index.y-1, _curFloor);
                if (temp != null && temp.TryGetComponent<Floor>(out var floor) == true)
                { //If there is a neighbour and it has a floor Component, add it to the Objects Array
                    neighbourObjects[3] = temp;
                    CheckForTriangles(temp,3,2); //Check if the tile has adjacent walls to this one
                }
                //Check if there is a South neighbour above and below
                GetNeighbours(_upNeigh, upNeighObj, 3,(int)index.x,(int)index.y-1,_curFloor+1);
                GetNeighbours(dwnNeighObj, 3,(int)index.x,(int)index.y-1,_curFloor-1);
            }
            if (_leftBorder == false && _upBorder == false) //Check for the NorthWest Neighbour
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x - 1, (int)index.y + 1,_curFloor);
                if (temp != null && temp.TryGetComponent<Floor>(out var floor) == true)
                { //If there is a neighbour and it has a floor Component, add it to the Objects Array
                    if (isTriangle == true) //If the tile is a triangle, then immediately set the neighbour as true
                    {
                        _neighbours[4] = true;
                    }
                    else
                    { //Only set the neighbour as true if it is not a triangle tile
                        if (floor.isTriangle == true)
                        {
                            _neighbours[4] = false;
                        }
                        else
                        {
                            _neighbours[4] = true;
                        }
                    }
                    neighbourObjects[4] = temp;
                }
                //Check if there is a NorthWest neighbour above and below
                GetNeighbours(_upNeigh, upNeighObj, 4,(int)index.x-1,(int)index.y+1,_curFloor+1);
                GetNeighbours(dwnNeighObj, 4,(int)index.x-1,(int)index.y+1,_curFloor-1);
            }
            if (_upBorder == false && _rightBorder == false) //Check for the North-East Neighbour
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x + 1, (int)index.y +1, _curFloor);
                if (temp != null && temp.TryGetComponent<Floor>(out var floor) == true)
                { //If there is a neighbour and it has a floor Component, add it to the Objects Array
                    if (isTriangle == true) //If the tile is a triangle, then immediately set the neighbour as true
                    {
                        _neighbours[5] = true;
                    }
                    else
                    { //Only set the neighbour as true if it is not a triangle tile
                        if (floor.isTriangle == true)
                        {
                            _neighbours[5] = false;
                        }
                        else
                        {
                            _neighbours[5] = true;
                        }
                    }
                    neighbourObjects[5] = temp;
                }
                //Check if there is a NorthEast neighbour above and below
                GetNeighbours(_upNeigh, upNeighObj, 5,(int)index.x+1,(int)index.y+1,_curFloor+1);
                GetNeighbours(dwnNeighObj, 5,(int)index.x+1,(int)index.y+1,_curFloor-1);
            }
            if (_leftBorder == false && _downBorder == false) //Check for the South-West Neighbour
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x - 1, (int)index.y - 1, _curFloor);
                if (temp != null && temp.TryGetComponent<Floor>(out var floor) == true)
                { //If there is a neighbour and it has a floor Component, add it to the Objects Array
                    if (isTriangle == true) //If the tile is a triangle, then immediately set the neighbour as true
                    {
                        _neighbours[6] = true;
                    }
                    else
                    { //Only set the neighbour as true if it is not a triangle tile
                        if (floor.isTriangle == true)
                        {
                            _neighbours[6] = false;
                        }
                        else
                        {
                            _neighbours[6] = true;
                        }
                    }
                    neighbourObjects[6] = temp;
                }
                //Check if there is a SouthWest neighbour above and below
                GetNeighbours(_upNeigh, upNeighObj, 6,(int)index.x-1,(int)index.y-1,_curFloor+1);
                GetNeighbours(dwnNeighObj, 6,(int)index.x-1,(int)index.y-1,_curFloor-1);
            }
            if (_downBorder == false && _rightBorder == false) //Check for the South-East Neighbour
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x + 1, (int)index.y -1, _curFloor);
                if (temp!= null && temp.TryGetComponent<Floor>(out var floor) == true)
                { //If there is a neighbour and it has a Floor Component, add it to the Objects Array
                    if (isTriangle == true) //If the tile is a triangle, then immediately set the neighbour as true
                    {
                        _neighbours[7] = true;
                    }
                    else
                    { //Only set the neighbour as true if it is not a triangle tile
                        if (floor.isTriangle == true)
                        {
                            _neighbours[7] = false;
                        }
                        else
                        {
                            _neighbours[7] = true;
                        }
                    }
                    neighbourObjects[7] = temp;
                }
                //Check if there is a SouthEast neighbour above and below
                GetNeighbours(_upNeigh, upNeighObj, 7,(int)index.x+1,(int)index.y-1,_curFloor+1);
                GetNeighbours(dwnNeighObj, 7,(int)index.x+1,(int)index.y-1,_curFloor-1);
            }
            if (_curFloor < (_maxFloors - 1)) //Check for neighbours above
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x, (int)index.y,(int)index.z+1);
                // GameObject temp = spawner.floorMatrix[(int)_index.x, (int)_index.y,(int)_index.z+1];
                if (temp != null && temp.TryGetComponent<Floor>(out var floor) == true)
                { //If there is a neighbour and it has a floor Component, add it to the Objects Array
                    neighbourObjects[8] = temp;
                    if (floor.isTriangle == false)
                    {
                        above = true; //Set the corresponding neighbour bool as true
                    }
                }
            }
            if (_curFloor > 0) //Check for neighbours below
            { //Try to get the neighbour's Game Object
                GameObject temp = spawner.matrices.GetTileObject((int)index.x,(int)index.y,(int)index.z-1);
                // GameObject temp = spawner.floorMatrix[(int)_index.x,(int)_index.y,(int)_index.z-1];
                if (temp != null && temp.TryGetComponent<Floor>(out var floor) == true && floor.isTriangle == false)
                { //If there is a neighbour and it has a floor Component, add it to the Objects Array
                    neighbourObjects[9] = temp;
                    if (floor.isTriangle == false)
                    {
                        below = true; //Set the corresponding neighbour bool as true
                    }
                } 
            }
            //Check if the function when the tile was spawned or from a neighbouring tile
            if (fromFloor == false)
            {
                for (int i = 0; i < neighbourObjects.Length; i++)
                {
                    if (neighbourObjects[i] != null && neighbourObjects[i].GetComponent<Floor>() != null)
                    {//Update all neighbouring tiles
                        neighbourObjects[i].GetComponent<Floor>().GetNeighbours(spawner,true);
                    }
                }
                for (int i = 0; i < 8; i++)
                { //Update all the neighbouring tiles from 1 level above and below
                    if (upNeighObj[i] != null)
                    {
                        upNeighObj[i].GetComponent<Floor>().GetNeighbours(spawner,true);
                    }
                    if (dwnNeighObj[i] != null)
                    {
                        dwnNeighObj[i].GetComponent<Floor>().GetNeighbours(spawner,true);
                    }
                }
                UpdateFloor(); //After setting all the neighbours, set the floor's appereance
            }
            else
            {
                CleanMesh(); //If the function was called by another floor, destroy the spawned meshes and update the tile's appereance
            }
        }
        //Check if there is a neighbour on the marked section, used for the neighbours on the level above
        private void GetNeighbours(bool[] boolSection, GameObject[] objSection, int index, int x, int y, int z)
        {
            if (_curFloor < (_maxFloors-1))
            {
                GameObject temp = _editorSpawner.matrices.GetTileObject(x,y,z);
                // GameObject temp = _editorSpawner.floorMatrix[x,y,z];
                if (temp != null && temp.TryGetComponent<Floor>(out Floor floor) == true)
                {
                    objSection[index] = temp;
                    if (floor.isTriangle == false)
                    {
                        boolSection[index] = true;
                    }
                }
            }
        }
        //Check if there is a neighbour on the marked section, used for the neighbours on the level below
        private void GetNeighbours(GameObject[] objSection, int index, int x, int y, int z)
        {
            if (_curFloor > 0)
            {
                GameObject temp = _editorSpawner.matrices.GetTileObject(x,y,z);
                // GameObject temp = _editorSpawner.floorMatrix[x,y,z];
                if (temp != null && temp.TryGetComponent<Floor>(out Floor floor) == true)
                {
                    objSection[index] = temp;
                    if (floor.isTriangle == false)
                    {
                        _dwnNeigh[index] = true; 
                    }
                }
            }
        }
        //Check if the tile is at any of the edges of the Map
        private void GetBorders()
        {
            if (index.y == _mapDepth - 1)
            {
                _upBorder = true; //Is the tile at the last row on the Z axis?
            }
            if (index.y < 1)
            {
                _downBorder = true; //Is the tile at the first row on the Z axis?
            }
            if (index.x < 1)
            {
                _leftBorder = true; //Is the tile at the first row on the X axis?
            }
            if (index.x == _mapWidth - 1)
            {
                _rightBorder = true; //Is the tile at the last row on the X axis?
            }
        }
        //Set the floor's appereance based on its neighbours
        private void UpdateFloor()
        {
            if (isTriangle == true)
            { //If the tile is a triangle spawn it and rotate it accordingly
                SetTriangle();
            }
            else
            {
                int besides = 0; //How many adjacent neighbours the tile has
                for (int i = 0; i < 4; i++)
                {
                    if (_neighbours[i] == true)
                    {
                        besides ++; //Check how many adjacent neighbours (first four in array) the tile has
                    }
                }
                switch (besides)
                { //Depending on the number of adjacent neighbours call function for needed type of appereance
                    case 0:
                    IsLoner(); //If there are no adjacent neighbours
                        break;
                    case 1:
                    IsOneSpace(); //Only one adjacent neighbour
                        break;
                    case 2:
                    TwoEdges(); //Two adjacent neighbours
                        break;
                    case 3:
                    IsOneEdge(); //Three adjacent neighbours
                        break;
                    case 4:
                    IsCentre(); //Four adjacent neighbours
                        break;
                    default:
                        break;
                }
                if (below == true && above == true)
                {
                    besides +=2; //Check if the tile has neighbours both below and above
                }
                if (besides == 6)
                { //If the tile has neighbours on every direction then turn off its collider
                    blocked = true; 
                }
                else
                {
                    blocked = false; //Set for its collider to be turned on 
                }
            }
        }
        //Check if the tile has two specified diagonal neighbours and return a bool array with the results
        private bool[] CheckDiagonal(int a, int b)
        {
            bool[] array = new bool[2]; //Initialize bool array
            array[0] = _neighbours[a]; //Get the first specified neighbour from the _neighbours array
            array[1] = _neighbours[b]; //Get the second specified neighbour from the _neighbours array
            return array;
        }
        //Check if the specified tile is a triangle and if it should be considered as a neighbour
        private void CheckForTriangles(GameObject neighbour, int index)
        {
            if (neighbour != null)
            {
                Floor floor = neighbour.GetComponent<Floor>();
                if (floor.isTriangle == true)
                { //If the neighbour is set as a triangle
                    switch (index)
                    {
                        case 4: case 6: //If its the North-West or South-West tile
                        if (floor.rotation == 2 || floor.rotation == 1)
                        { //If the triangle is connected to this tile's neighbour set the neighbour as true
                            _neighbours[index] = true; 
                        }
                        else
                        {
                            _neighbours[index] = false;
                        }
                            break;
                        case 5: case 7: //If its the North-East or South-East tile
                        if (floor.rotation == 3 || floor.rotation == 0)
                        { //If the triangle is connected to this tile's neighbour set the neighbour as true
                            _neighbours[index] = true;
                        }
                        else
                        {
                            _neighbours[index] = false;
                        }
                            break;
                        
                        default:
                            break;
                    }
                }
                else
                { //If the tile is not a triangle set its index as true
                    _neighbours[index] = true;
                }
            }
        }
        //Check if the neighbour is a triangle, and if it is, if they have adjacent walls to this tile
        private void CheckForTriangles(GameObject neighbour, int index, int turns)
        {
            //Initialize Queue with the rotation options of the triangle floor that would count as a neighbour for this tile
            Queue<int> myQueue = new Queue<int>(new[]{2,3,0,1}); 
            if (neighbour != null)
            {
                Floor floor = neighbour.GetComponent<Floor>(); 
                if (floor.isTriangle == true)
                {   //In case the neighbour is set as a triangle check its rotation
                    for (int i = 0; i < turns; i++)
                    { //Depending on the direction the other tile is at, set which 2 of the rotation options are needed from the...
                    //triangle tile in order to count as a neighbour  
                        int temp = myQueue.Dequeue(); //Take out first option..
                        myQueue.Enqueue(temp); //and place it as the last
                    }
                    int[] options = new int[]{myQueue.Dequeue(),myQueue.Dequeue()}; //Create an array with the 2 first options from the Queue
                    if (neighbour.GetComponent<Floor>().rotation == options[0] || neighbour.GetComponent<Floor>().rotation == options[1])
                    { //If the triangle is directly connected to the mesh set its neighbour index as true
                        _neighbours[index] = true;
                    }
                    else
                    {
                        _neighbours[index] = false;
                    }
                }
                else
                { //If the neighbour is not set as a triangle, then immediately set it as true
                    _neighbours[index] = true;
                }
            }
        }
        //Destroy any previously spawned meshes of this tile
        private void CleanMesh()
        { 
            transform.rotation = Quaternion.identity; //Reset the rotation of the tile 
            for (int i = 0; i < _pieces.Length; i++)
            {
                int temp = _pieces[i].childCount;
                for (int j = 0; j < temp; j++)
                { //Destroy all child objects, which will be the meshes
                    DestroyImmediate(_pieces[i].GetChild(0).gameObject);
                }
            }
            UpdateFloor(); //Set the floor's appereance 
        }
        //Called when a neighbouring tile is erased in edit mode
        public void NeighbourDestroyed(GameObject neighbour)
        {
            //Check which of its neighbours was destroyed and set its reference as false///
            for (int i = 0; i < 8; i++)
            {
                if (neighbourObjects[i] == neighbour)
                {
                    neighbourObjects[i] = null;
                    _neighbours[i] = false;
                }
            }
            if (neighbourObjects[8] == neighbour)
            {
                neighbourObjects[8] = null;
                above = false;
            }
            if (neighbourObjects[9] == neighbour)
            {
                neighbourObjects[9] = null;
                below = false;
            }
            /////////////////////////////////////////////////////////////////////////////
            CleanMesh();
        }
        //Called when a neihgbouring tile on the level above is erased in edit mode
        public void UpNeighbourDestroyed(GameObject neighbour)
        { //Find which of the neighbours above was destroyed and update the lists
            for (int i = 0; i < 8; i++)
            {
                if (upNeighObj[i] == neighbour)
                {
                    upNeighObj[i] = null;
                    _upNeigh[i] = false;
                }
            }
            CleanMesh(); //Update the look of the tile
        }
        //Called when a neihgbouring tile on the level below is erased in edit mode
        public void DownNeighbourDestroyed(GameObject neighbour)
        { //Find which of the neighbours below was destroyed and update the list
            for (int i = 0; i < 8; i++)
            {
                if (dwnNeighObj[i] == neighbour)
                {
                    dwnNeighObj[i] = null;
                }
            }
            CleanMesh(); //Update the look of the tile
        }
        #endregion
        
        #region Floor Details
        //Instantiate a Tile with a set rotation  and set its material
        private void Spawn(GameObject part,Transform piece,int rotation)
        {
            GameObject mesh;
            mesh = Instantiate(part,piece.position,Quaternion.Euler(0,rotation,0), piece);
            mesh.GetComponent<MeshRenderer>().material = _parts.material;
        }
        //Instantiate a Tile with no rotation and set its material
        private GameObject Spawn(GameObject part,Transform piece)
        {
            GameObject mesh;
            mesh = Instantiate(part, piece);
            mesh.GetComponent<MeshRenderer>().material = _parts.material;
            return mesh;
        }
        //Get if there is neighbours above on the 2 specified points
        private bool[] GetAbove(int a, int b)
        {
            bool[] array = new bool[2];
            array[0] = _upNeigh[a];
            array[1] = _upNeigh[b];
            return array;
        }
        //Get if there is neighbours above on the 3 specified points
        private bool[] GetAbove(int a, int b, int c)
        {
            bool[] array = new bool[3];
            array[0] = _upNeigh[a];
            array[1] = _upNeigh[b];
            array[2] = _upNeigh[c];
            return array;
        }
        //Get if there is neighbours above on the 4 specified points
        private bool[] GetAbove(int a, int b, int c, int d)
        {
            bool[] array = new bool[4];
            array[0] = _upNeigh[a];
            array[1] = _upNeigh[b];
            array[2] = _upNeigh[c];
            array[3] = _upNeigh[d];
            return array;
        }
        //Get if there is neighbours below on the 2 specified points
        private bool[] GetBelow(int a, int b)
        {
            bool[] array = new bool[2];
            array[0] = _dwnNeigh[a];
            array[1] = _dwnNeigh[b];
            return array;
        }
        //Get if there is neighbours below on the 4 specified points
        private bool[] GetBelow(int a, int b, int c, int d)
        {
            bool[] array = new bool[4];
            array[0] = _dwnNeigh[a];
            array[1] = _dwnNeigh[b];
            array[2] = _dwnNeigh[c];
            array[3] = _dwnNeigh[d];
            return array;
        }
        private bool[] CheckTriangleIndexes(int a, int b)
        {
            int minus = 0;
            int plus = 0;
            if (rotation == 0) {minus = 3;}
            else {minus = rotation - 1;}
            if (rotation == 3) {plus = 0;}
            else {plus = rotation + 1;}
            bool[] array = new bool[2];
            GameObject[] temps = new GameObject[2]{neighbourObjects[a],neighbourObjects[b]};
            Floor floor = null;
            if (temps[0] != null && temps[0].TryGetComponent<Floor>(out floor) == true)
            {
                if (floor.isTriangle == false)
                {
                    array[0] = true;
                }
                else
                {
                    if (floor.rotation == rotation || floor.rotation == minus)
                    {
                        array[0] = true;
                    }
                    else
                    {
                        array[0] = false;
                    }
                }
            }
            else {array[0] = false;}

            if (temps[1] != null && temps[1].TryGetComponent<Floor>(out floor) == true)
            {
                if (floor.isTriangle == false)
                {
                    array[1] = true;
                }
                else
                {
                    if (floor.rotation == rotation || floor.rotation == plus)
                    {
                        array[1] = true;
                    }
                    else
                    {
                        array[1] = false;
                    }
                }
            }
            else {array[1] = false;}
            return array;
        }
        //Set the name of the tile using its position on the grid and its type
        private void SetName(string type)
        {
            gameObject.name = index.x + ", " + index.y + " - ("+type+")";
        }
        #endregion

        #region Types of Floors
        public void IsLoner() //No adjacent neighbours
        {
            if (above == false)
            { //If there are no tiles above, instantiate the upper parts of the mesh and the normal walls
                for (int i = 0; i < 4; i++)
                {
                    Spawn(_parts.cornerLid,_pieces[i],i*90);
                    Spawn(_parts.cornerWalls[0],_pieces[i],i*90);
                }
            }
            else
            { //If there is a tile above, instantiate the connection walls
                bool[] neighAbove = new bool[4];
                neighAbove = GetAbove(1,0,2,3);
                Queue<bool> queue = new Queue<bool>(neighAbove);
                bool temp = false;
                for (int i = 0; i < 4; i++)
                {
                    neighAbove = queue.ToArray();
                    int count = 0;
                    for (int j = 0; j < 2; j++)
                    {
                        if (neighAbove[j] == true)
                        {
                            count++;
                        }
                    }
                    if (count == 1)
                    {
                        Spawn(_parts.cornerWalls[2],_pieces[i],i*90);
                    }
                    else
                    {
                        Spawn(_parts.cornerWalls[1],_pieces[i],i*90);
                    }
                    temp = queue.Dequeue();
                    queue.Enqueue(temp);
                }
            }
            if (below == false)
            { //If there are no tiles below, instantiate the bottom part of the mesh
                for (int i = 0; i < 4; i++)
                {
                    Spawn(_parts.cornerBottom,_pieces[i],i*90);
                }
            }
        }
        public void IsOneSpace() //Only one adjacent neighbour
        {
            SetName("OneSpace");
            int neigh = 0; //Index of adjacent neighbour
            bool neighAbove = false;
            bool[] diagonals = new bool[2]; 
            bool[] diagAbove = new bool[2];
            bool[] wallCheck = new bool[3];
            bool[] diagBelow = new bool[2];
            bool floorCheck = false;
            for (int i = 0; i < 4; i++)
            { //Get the index of the adjacent neighbour
                if (_neighbours[i] == true)
                {
                    neigh = i;
                    neighAbove = _upNeigh[i];
                    break;
                }
            }

            switch (neigh)
            { //Depending on the position of the adjacent neighbour check the corresponding diagonal neighbours,... 
              // as well as if they have tiles above
                case 0:
                diagonals = CheckDiagonal(4,5);
                diagAbove = GetAbove(4,5);
                wallCheck = GetAbove(2,3,1);
                diagBelow = GetBelow(4,5);
                    break;
                case 1:
                diagonals = CheckDiagonal(6,4);
                diagAbove = GetAbove(6,4);
                wallCheck = GetAbove(0,2,3);
                diagBelow = GetBelow(6,4);
                    break;
                case 2:
                diagonals = CheckDiagonal(5,7);
                diagAbove = GetAbove(5,7);
                wallCheck = GetAbove(3,1,0);
                diagBelow = GetBelow(5,7);
                    break;
                case 3:
                diagonals = CheckDiagonal(7,6);
                diagAbove = GetAbove(7,6);
                wallCheck = GetAbove(1,0,2);
                diagBelow = GetBelow(7,6);
                    break;
                default:
                    break;
            }
            floorCheck = _dwnNeigh[neigh];
            if (above == false)
            {
                Spawn(_parts.cornerWalls[0],_pieces[0]); //Spawn the normal corner walls
                Spawn(_parts.cornerWalls[0],_pieces[1],90);
                Spawn(_parts.cornerLid,_pieces[0]); //Spawn the corner lids
                Spawn(_parts.cornerLid,_pieces[1],90);
            }
            else
            { //Check if the empty spaces around have a tile above to decide which wall to spawn on the corners
                for (int i = 0; i < 2; i++)
                {
                    int count = 0;
                    for (int j = 0; j < 2; j++)
                    {
                        if (wallCheck[j+i] == true)
                        { //Check the 0 & 1 on the first round and then 1 & 2
                            count++;
                        }
                    }
                    if (count == 1)
                    { //If there is only one tile above the empty neighbours, then spawn the special connection wall
                        Spawn(_parts.cornerWalls[2],_pieces[i],90*i);
                    }
                    else
                    {
                        Spawn(_parts.cornerWalls[1],_pieces[i],90*i); //Spawn the normal connection corner walls
                    }
                }

            }
            if (below == false)
            {
                Spawn(_parts.cornerBottom,_pieces[0]); //Spawn the corner bottoms
                Spawn(_parts.cornerBottom,_pieces[1],90);
            }
            #region Curve or Straight Connection
            if (diagonals[0] == false)
            { //If there is no diagonal neighbour next to the adjacent neighbour then instantiate straight edges and lids
                if (above == false)
                { //If there is no tile above then spawn the normal walls and lid
                    Spawn(_parts.straightWalls[0],_pieces[2],180);
                    Spawn(_parts.squareLid,_pieces[2]);
                }
                else
                { //If there is a tile above check if the adjacent neighbour has one above too
                    if (neighAbove == true )
                    { //Spawn the normal connection wall
                        Spawn(_parts.straightWalls[1],_pieces[2],180);
                    }
                    else
                    { //Spawn the special connection wall
                        Spawn(_parts.straightWalls[3],_pieces[2]);
                    }
                }
                if (below == false)
                { //If there is no tile above then spawn the straight bottom
                    Spawn(_parts.straightBottom,_pieces[2],180);
                }
            }
            else
            { //If there is a diagonal neighbour next to the adjacent neighbour then instantiate curved edges and lids
                if (above == false)
                { //If there is no tile above then spawn the normal wall and the lid
                    Spawn(_parts.curvedWalls[0],_pieces[2],180);
                    Spawn(_parts.curveLids[0],_pieces[2],180);
                }
                else
                { //Check if the diagonal neighbour has a tile above
                    if (diagAbove[0] == true && neighAbove == true)
                    { //Spawn the normal connection wall
                        Spawn(_parts.curvedWalls[2],_pieces[2],180);   
                    }
                    else
                    { //Spawn the special connection wall
                        Spawn(_parts.curvedWalls[4],_pieces[2],180);
                    }
                }
                if (below == false)
                { //If there is no tile below then spawn the curved bottom
                    Spawn(_parts.curveBottoms[0],_pieces[2],180);
                }
                else
                {
                    if (floorCheck == false || diagBelow[0] == false)
                    {
                        Spawn(_parts.halfBottoms[0],_pieces[2],180);
                    }
                }
            }
            if (diagonals[1] == false)
            { //If there is no diagonal neighbour next to the adjacent neighbour then instantiate straight edges and lids
                if (above == false)
                { //If there is no tile above then spawn the normal wall and lid
                    Spawn(_parts.straightWalls[0],_pieces[3]);
                    Spawn(_parts.squareLid,_pieces[3]);
                }
                else
                { //Check to see if the adjacent neighbour has a tile above
                    if (neighAbove == true)
                    { //Spawn the normal connection wall
                        Spawn(_parts.straightWalls[1],_pieces[3]);
                    }
                    else
                    { //Spawn the special connection wall
                        Spawn(_parts.straightWalls[2],_pieces[3]);
                    }
                }
                if (below == false)
                { //If there is no tile below then spawn the straight bottom
                    Spawn(_parts.straightBottom,_pieces[3]);
                }
            }
            else
            { //If there is a diagonal neighbour next to the adjacent neighbour then instantiate curved edges and lids
                if (above == false)
                { //If there is no tile above then spawn the normal wall and lid
                    Spawn(_parts.curvedWalls[1],_pieces[3],180);
                    Spawn(_parts.curveLids[1],_pieces[3],180);
                }
                else
                { //Check if the diagonal neighbour has a tile above
                    if (diagAbove[1] == true && neighAbove == true)
                    { //Spawn the normal connection wall
                        Spawn(_parts.curvedWalls[3],_pieces[3],180);
                    }
                    else
                    { //Spawn the special connection wall
                        Spawn(_parts.curvedWalls[5],_pieces[3],180);
                    }
                }
                if (below == false)
                { //If there is no tile below then spawn the curved bottom
                    Spawn(_parts.curveBottoms[1],_pieces[3],180);
                }
                else
                {
                    if (floorCheck == false || diagBelow[1] == false)
                    {
                        Spawn(_parts.halfBottoms[1],_pieces[3],180);
                    }
                }
            }
            #endregion
            switch (neigh)
            { //Depending on the index of the adjacent neighbour, rotate the tile to match correctly
                case 0:
                transform.rotation = Quaternion.Euler(0,180,0);
                    break;
                case 1:
                transform.rotation = Quaternion.Euler(0,90,0);
                    break;
                case 2:
                transform.rotation = Quaternion.Euler(0,270,0);
                    break;
                case 3:
                transform.rotation = Quaternion.Euler(0,0,0);
                    break;
                default:
                    break;
            }
        }
        public void IsOneEdge() //Three adjacent neighbours
        {
            SetName("OneEdge");
            int noNeigh = 0; //Index of the only adjacent neighbour missing
            int direction = 0; //The rotation that the tile needs
            Queue myQueue = new Queue();
            bool[] neighAbove = new bool[2];
            bool[] diagAbove = new bool[2];
            bool[] neighBelow = new bool[2];
            bool[] diagBelow = new bool[2];
            for (int i = 0; i < 4; i++)
            { //Get the index of the missing adjacent neighbour
                if (_neighbours[i] == false)
                {
                    noNeigh = i;
                    break;
                }
            }
            switch (noNeigh)
            { //Get the direction to rotate the tile in order to match with its neighbours
              //Get if the diagonal neighbours have tiles above
                case 0:
                direction = 0;
                diagAbove = GetAbove(4,5); 
                neighAbove = GetAbove(1,2);
                diagBelow = GetBelow(4,5);
                neighBelow = GetBelow(1,2);
                    break;
                case 1:
                direction = 270;
                diagAbove = GetAbove(6,4);
                neighAbove = GetAbove(3,0);
                diagBelow = GetBelow(6,4);
                neighBelow = GetBelow(3,0);
                    break;
                case 2:
                direction = 90;
                diagAbove = GetAbove(5,7);
                neighAbove = GetAbove(0,3);
                diagBelow = GetBelow(5,7);
                neighBelow = GetBelow(0,3);
                    break;
                case 3:
                direction = 180;
                diagAbove = GetAbove(7,6);
                neighAbove = GetAbove(2,1);
                diagBelow = GetBelow(7,6);
                neighBelow = GetBelow(2,1);
                    break;
                default:
                    break;
            }
            myQueue = SetQueue(direction); //Get the 2 diagonal neighbours that need to be checked 
            for (int i = 0; i < 2; i++)
            {
                bool temp = (bool)myQueue.Dequeue(); //Get if there is a diagonal neighbour
                if (temp == false)//If there is no diagonal neighbor
                { //Instantiate a straight part
                    if (above == false)
                    { //If there is no tile above then spawn the normal wall and lid
                        Spawn(_parts.straightWalls[0],_pieces[i],90);
                        Spawn(_parts.squareLid,_pieces[i]);
                    }
                    else
                    { //Check if the adjacent neighbour has a tile above
                        if (neighAbove[i] == true)
                        { //Spawn the normal connection wall
                            Spawn(_parts.straightWalls[1],_pieces[i],90);
                        }
                        else
                        { //Spawn the special connection wall
                            Spawn(_parts.straightWalls[2+i],_pieces[i],90+(180*i));
                        }
                    }
                    if (below == false)
                    { //If there is no tile below spawn the straight bottom
                        Spawn(_parts.straightBottom,_pieces[i],90);
                    }
                }
                else//If there is a diagonal neighbor
                { //Instantiate a curved part
                    if (above == false)
                    { //If there is no tile above then spawn a normal wall and lid
                        Spawn(_parts.curvedWalls[1-i],_pieces[i],270-(180*i));
                        Spawn(_parts.curveLids[1-i],_pieces[i],270-(180*i));
                    }
                    else
                    { //Check if the diagonal neighbours have tiles above
                        if (diagAbove[i] == true && neighAbove[i] == true)
                        { //Spawn a normal connection wall
                            Spawn(_parts.curvedWalls[3-i],_pieces[i],270-(180*i));
                        }
                        else
                        { //Spawn a special connection wall
                            Spawn(_parts.curvedWalls[5-i],_pieces[i],270-(180*i));
                        }
                    }
                    if (below == false)
                    { //If there is no tile below then spawn a curved bottom
                        Spawn(_parts.curveBottoms[1-i],_pieces[i],270-(180*i));
                    }
                    else
                    {
                        if (diagBelow[i] == false || neighBelow[i] == false)
                        {
                            Spawn(_parts.halfBottoms[1-i],_pieces[i],270-(180*i));
                        }   
                    }
                }
                //Instantiate the lids and bottoms of the parts adjacent to neighbours
                if (above == false)
                {
                    Spawn(_parts.squareLid,_pieces[2+i]);
                }
                if (below == false)
                {
                    Spawn(_parts.squareBottom,_pieces[2+i]);
                }
            }
            transform.rotation = Quaternion.Euler(0,direction,0); //Set the tile's rotation to match its neighbours 
        }
        public void IsCentre() //Four adjacent neighbours
        {
            SetName("CentrePiece");
            if (above == false ||below == false)
            { //Check if any mesh needs to be instantiated
                GameObject[] lids = new GameObject[4];
                for (int i = 0; i < _pieces.Length; i++)
                {
                    if (above == false)
                    { //Instantiate the top mesh if there are no neighbours above
                        Spawn(_parts.squareLid,_pieces[i]);
                    }
                    if (below == false)
                    { //Instantiate the bottom mesh if there are no neighbours bellow
                        Spawn(_parts.squareBottom,_pieces[i]);
                    }
                }
            }
        }
        private void SetTriangle() //If the tile is a triangle
        {
            SetName("Triangle");
            int yes = 0; //How many walls connect with the walls on the tile
            bool[] touching = QueueVariant(); //Get which neighbours to check based on the tile's rotation
            for (int i = 0; i < 2; i++)
            {
                if (touching[i] == true)
                { //If the side is connected to another tile, instantiate the open triangle parts of the tile
                    yes++; //Increase the counter of connected sides
                    bool[] array = new bool[2];
                    switch (rotation)
                    {
                        case 0:
                        array = CheckTriangleIndexes(6,5);
                            break;
                        case 1:
                        array = CheckTriangleIndexes(4,7);
                            break;
                        case 2:
                        array = CheckTriangleIndexes(5,6);
                            break;
                        case 3:
                        array = CheckTriangleIndexes(7,4);
                            break;
                        default:
                            break;
                    } 

                    if (array[i] == false)
                    {
                        Spawn(_parts.diagonalWalls[1-i],_pieces[2-i]);
                        if (above == false)
                        {
                            Spawn(_parts.diagonalLids[1-i],_pieces[2-i]);
                        }
                        Spawn(_parts.diagonalBottoms[1-i],_pieces[2-i]);
                    }
                    else
                    {
                        Spawn(_parts.diagonalWalls[4],_pieces[2-i]);
                        if (above == false)
                        {
                            Spawn(_parts.diagonalLids[4],_pieces[2-i]);
                        }
                        Spawn(_parts.diagonalBottoms[4],_pieces[2-i]);
                    }
                }
                else
                { //If the side is not connected to another tile, Instantiate the closed triangle parts of the tile
                    Spawn(_parts.diagonalWalls[3-i],_pieces[2-i]);
                    if (above == false)
                    { //If there are no tiles above instantiate the top part
                        Spawn(_parts.diagonalLids[3-i],_pieces[2-i]);
                    }
                    Spawn(_parts.diagonalBottoms[3-i],_pieces[2-i]);
                }
            }
            switch (yes)
            { //Instantiate the rest of the mesh based on how many sides are connected to another tile
                case 0: //If the tile is not connected to any other tile Instantiate a fully closed triangle
                    if (above == false)
                    { //If there are no tiles above instantiate a normal wall and the lid
                        Spawn(_parts.cornerWalls[0],_pieces[0]);
                        Spawn(_parts.cornerLid,_pieces[0]);
                    }
                    else
                    { //If there is a tile above then instantiate a connection wall
                        Spawn(_parts.cornerWalls[1],_pieces[0]);
                    }
                    Spawn(_parts.cornerBottom,_pieces[0]);
                    break;
                case 1: //If the tile is connected to only 1 other tile Instantiate a straight wall
                    GameObject[] meshes = new GameObject[2];
                    if (above == false)
                    { //If there are no tiles above instantiate a normal wall and the lid
                        meshes[0] = Spawn(_parts.straightWalls[0],_pieces[0]);
                        Spawn(_parts.squareLid, _pieces[0]);
                    }
                    else
                    { //Instantiate a connection wall
                        meshes[0] = Spawn(_parts.straightWalls[1],_pieces[0]);
                    }
                    meshes[1] = Spawn(_parts.straightBottom, _pieces[0]);
                    if (touching[0] == true)
                    { //Rotate the meshes to the proper position to match its neghbours
                        for (int i = 0; i < 2; i++)
                        {
                            if (meshes[i] != null)
                            {
                                meshes[i].transform.rotation = Quaternion.Euler(0, 90, 0);
                            }
                        }
                    }
                    break;
                case 2: //If the tile is connected to 2 other tiles set an open square lid and bottom
                    if (above == false)
                    {
                        Spawn(_parts.squareLid,_pieces[0]);
                    }
                    Spawn(_parts.squareBottom,_pieces[0]);
                    break;
                default:
                    break;
            }
            gameObject.transform.rotation = Quaternion.Euler(0, 90 * rotation, 0); //Rotate the tile to match its neighbours
        }
            #region Two Edges Variants
        public void TwoEdges() //Two adjacent neighbours
        {
            List<int> values = new List<int>();
            //Set a specific value for the two neighbours based on their positions from the tile 
            if (_neighbours[0] == true)
            {
                values.Add((int)BlockDirections.UpBlock);
            }
            if (_neighbours[1] == true)
            {
                values.Add((int)BlockDirections.LeftBlock);
            }
            if (_neighbours[2] == true)
            {
                values.Add((int)BlockDirections.RightBlock);
            }
            if (_neighbours[3] == true)
            {
                values.Add((int)BlockDirections.DownBlock);
            }
            //Get a final ID number based on the sum of the values given to each of the neighbouring tiles and...
            //use this ID number to define which type of tile it is and its rotation
            int result = values[0] + values[1];
            switch (result)
            {
                case -3:
                SetCorner(0); //Its a corner with no rotation
                    break;
                case 0:
                SetHall(90); //Its a hall with 90 degrees rotation
                    break;
                case 1:
                SetCorner(90); //Its a corner with 90 degrees rotation
                    break;
                case 2:
                SetCorner(270); //Its a corner with 170 degrees rotation
                    break;
                case 3:
                SetHall(0); //Its a hall with no rotation
                    break;
                case 6:
                SetCorner(180); //Its a corner with 180 degrees rotation
                    break;
                default:
                    break;
            }
        }
        public void SetCorner(int direction) //The adjacent neighbours are not in oposite sides
        {
            SetName("Corner");
            int j = 1; //Counter to help with the placement of the meshes
            bool[] diagonals = new bool[2];
            bool[] diagAbove = new bool[2];
            bool[] neighAbove = new bool[2];
            bool[] temp = new bool[2];
            bool[] neighBelow = new bool[2];
            switch (direction / 90)
            { //Check the diagonal neighbours based on the rotation of the tile, and check if the needed neighbours...
              //have tiles above them
                case 0:
                diagonals = CheckDiagonal(5,6);
                diagAbove = GetAbove(5,6);
                neighAbove = GetAbove(2,3);
                temp = GetAbove(0,1);
                neighBelow = GetBelow(5,6);
                    break;
                case 1:
                diagonals = CheckDiagonal(7,4);
                diagAbove = GetAbove(7,4);
                neighAbove = GetAbove(3,1);
                temp = GetAbove(0,2);
                neighBelow = GetBelow(7,4);
                    break;
                case 2:
                diagonals = CheckDiagonal(6,5);
                diagAbove = GetAbove(6,5);
                neighAbove = GetAbove(1,0);
                temp = GetAbove(2,3);
                neighBelow = GetBelow(6,5);
                    break;
                case 3:
                diagonals = CheckDiagonal(4,7);
                diagAbove = GetAbove(4,7);
                neighAbove = GetAbove(0,2);
                temp = GetAbove(1,3);
                neighBelow = GetBelow(4,7);
                    break;
                default:
                    break;
            }
            if (above == false)
            { //If there is no tile above then spawn a normal corner wall and lid, along with the southeast lid
                Spawn(_parts.cornerWalls[0],_pieces[0]);
                Spawn(_parts.cornerLid,_pieces[0]);
                Spawn(_parts.squareLid,_pieces[2]);
            }
            else
            { //Spawn a connection corner wall
                int count = 0;
                for (int i = 0; i < 2; i++)
                {
                    if (temp[i] == true)
                    {
                        count++;
                    }
                }
                if (count == 1)
                {
                    Spawn(_parts.cornerWalls[2],_pieces[0]);                  
                }
                else
                {
                    Spawn(_parts.cornerWalls[1],_pieces[0]);
                }
            }
            if (below == false)
            { //If there is no tile below then spawn the bottom pieces for the corner and southeast piece
                Spawn(_parts.cornerBottom,_pieces[0]); 
                Spawn(_parts.squareBottom,_pieces[2]); 
            }
            for (int i = 0; i < 2; i++)//Spawn the two straight areas of the corner
            {
                if (diagonals[i] == false)
                { //If this side has no diagonal neighbours instantiate a straight piece
                    if (above == false)
                    { //If there is no tile above then spawn a normal straight wall and lid
                        Spawn(_parts.straightWalls[0],_pieces[i+j],180-(90*j));
                        Spawn(_parts.squareLid,_pieces[i+j],180-(90*j));
                    }
                    else
                    { //Check if the adjacent neighbour has a tile above
                        if (neighAbove[i] == true)
                        { //Spawn a normal connection wall
                            Spawn(_parts.straightWalls[1],_pieces[i+j],180-(90*j));
                        }
                        else
                        { //Spawn a special connection wall
                            Spawn(_parts.straightWalls[3-i],_pieces[i+j],270-(270*i));
                        }
                    }
                    if (below == false)
                    { //If there is no tile below then spawn a straight bottom
                        Spawn(_parts.straightBottom,_pieces[i+j],180-(90*j));
                    }
                }
                else
                { //If this side has a diagonal neighbour instantiate a curved piece
                    if (above == false)
                    { //If ther is no tile above then spawn a normal curved wall and lid
                        Spawn(_parts.curvedWalls[i],_pieces[i+j],90+(90*i));
                        Spawn(_parts.curveLids[i],_pieces[i+j],90+(90*i));
                    }
                    else
                    { //Check if the diagonal neighbour has a tile above
                        if (diagAbove[i] == true && neighAbove[i] == true)
                        { //Spawn a normal connection wall
                            Spawn(_parts.curvedWalls[2+i],_pieces[i+j],90+(90*i));
                        }
                        else
                        { //Spawn a special connection wall
                            Spawn(_parts.curvedWalls[4+i],_pieces[i+j],90+(90*i));
                        }
                    }
                    if (below == false)
                    { //If there is no tile below then spawn a curved bottom
                        Spawn(_parts.curveBottoms[i],_pieces[i+j],90+(90*i));
                    }
                    else
                    {
                        if (neighBelow[i] == false)
                        {
                            Spawn(_parts.halfBottoms[i],_pieces[i+j],90+(90*i));
                        }
                    }
                }
                j++;
            }
            transform.rotation = Quaternion.Euler(0,direction,0); //Set the tile's rotation in order to match properly with its neighbours
        }
        public void SetHall(int direction) //The adjacent neighbours are in oposite sides
        {
            SetName("Hall");
            Queue diagonals = new Queue();
            bool[] diagAbove = new bool[4];
            bool[] neighAbove = new bool[2];
            bool[] diagBelow = new bool[4];
            bool[] neighBelow = new bool[2];
            diagonals = SetQueue(direction); //Get the diagonal neighbours based on the tile's rotation
            //Get if the diagonal and adjacent neighbours have a tile above them
            switch (direction)
            {
                case 0:
                diagAbove = GetAbove(4,5,7,6);
                neighAbove = GetAbove(0,3);
                diagBelow = GetBelow(4,5,7,6);
                neighBelow = GetBelow(0,3);
                    break;
                case 90:
                diagAbove = GetAbove(5,7,6,4);
                neighAbove = GetAbove(2,1);
                diagBelow = GetBelow(5,7,6,4);
                neighBelow = GetBelow(2,1);
                    break;
                default:
                    break;
            }
            int[] index = new int[4]{3,2,3,2}; //Help determine which straight special connection wall to use
            for (int i = 0; i < _pieces.Length; i++)
            {
                bool temp = (bool)diagonals.Dequeue(); //Get the first value
                int j = 0; //This will be used as a variable to determine the rotation of the meshes
                int k = 0; //Variable used to help choose the meshes from an array
                int l = 0; //Variable used to help cheking tiles above neighbours
                int m = 0; //Variable used to help check if the tile needs a half bottom
                int rot = 180; //Variable used to help with the straight special connection walls rotation
                if (i > 1)
                {
                    l = 1; //The last 2 pieces need to check for the neighbour on the second place of the array
                    rot = 0;//The last 2 special connection pieces need a 0 degree rotation
                }
                if (temp == false) //Spawn straight pieces
                {
                    if (i < 3 && i > 0)
                    { //For the right side of the tile set the rotation of the mesh at 180 degrees
                        j = 180;
                    }
                    if (above == false)
                    { //If there is no tile above then spawn the normal straight wall and lid
                        Spawn(_parts.straightWalls[0],_pieces[i],j);
                        Spawn(_parts.squareLid,_pieces[i]);
                    }
                    else
                    { //Check if the adjacent neighbour has a tile above
                        if (neighAbove[l] == true)
                        { //Spawn a normal connection wall
                            Spawn(_parts.straightWalls[1],_pieces[i],j);
                        }
                        else
                        { //Spawn a special connection wall
                            Spawn(_parts.straightWalls[index[i]],_pieces[i],rot);
                        }
                    }
                    if (below == false)
                    { //If there is no tile below then spawn a straight bottom piece
                        Spawn(_parts.straightBottom,_pieces[i],j);
                    }
                }
                else //Spawn curved pieces
                {
                    if (i > 1)
                    { //For the south part of the tile set the rotation of the mesh at 180 degrees
                        j = 180;
                        m = 1;
                    }
                    if (i == 0 || i == 2)
                    { //North-West & South-East pieces use the first curved mesh
                        k = 0;
                    }
                    else
                    { //Nort-East & South-West pieces use the second curved mesh
                        k = 1;
                    }
                    if (above == false)
                    { //If there is no tile above then spawn the normal curve wall and lid
                        Spawn(_parts.curvedWalls[k],_pieces[i],j);
                        Spawn(_parts.curveLids[k],_pieces[i],j);
                    }
                    else
                    { //Check if the diagonal neighbour has a tile above
                        if (neighAbove[l] == true && diagAbove[i] == true)
                        { //Spawn a normal connection wall
                            Spawn(_parts.curvedWalls[2+k],_pieces[i],j);
                        }
                        else
                        { //Spawn a special connection wall
                            Spawn(_parts.curvedWalls[4+k],_pieces[i],j);
                        }
                    }
                    if (below == false)
                    { //If there is no tile below then spawn the curved bottom
                        Spawn(_parts.curveBottoms[k],_pieces[i],j);
                    }
                    else
                    {
                        if (neighBelow[m] == false || diagBelow[i] == false)
                        {
                            Spawn(_parts.halfBottoms[k], _pieces[i],j);
                        }
                    }
                    k++;
                }
            }
            transform.rotation = Quaternion.Euler(0, direction, 0); //Set the tile's rotation to match properly with its neighbours
        }
            #endregion
        #endregion

        #region Queues
        //Give back a Queue with the values of the different diagonal neighbours, arranged in order based on the tile's rotation
        private Queue SetQueue(int direction) //Used on tiles with 3 adjacent neighbours and halls 
        {
            Queue queue = new Queue();
            for (int i = 0; i < 2; i++)
            { //Get the first two values from the neighbours on index 4 & 5
                queue.Enqueue(_neighbours[i + 4]);
            }
            for (int i = 0; i < 2; i++)
            { //Get the second two values from the neighbours on index 7 & 6
                queue.Enqueue(_neighbours[8 - (i + 1)]);
            }
            for (int i = 0; i < direction / 90; i++)
            { //Based on the rotation of the tile, set take the first value and set it as last x amount of times...
              // this in order to match the rotation of the tile and get the correct values
                bool temp = (bool)queue.Dequeue();
                queue.Enqueue(temp);
            }
            return queue;
        }
        //Give back a Queue with the values of the different adjacent neighbours arranged in a specific order
        private bool[] QueueVariant() //Used only on the triangle tiles
        {
            //Initialize Queue with the values in order
            Queue<bool> myQueue = new Queue<bool>(new[] { _neighbours[1], _neighbours[0], _neighbours[2], _neighbours[3] });
            bool[] touching = new bool[2];
            for (int i = 0; i < rotation; i++)
            { //Based on the tile's rotation cycle the values taking the first and placing it last
                bool temp = myQueue.Dequeue();
                myQueue.Enqueue(temp);
            }
            for (int i = 0; i < touching.Length; i++)
            { //Fill the array with the needed values
                touching[i] = myQueue.Dequeue();
            }
            return touching;
        }
        #endregion
        
        public void DefaultLook(FloorMesh options) //Instantiate the default look of the tile (No neighbours) for the Edit Mode Preview
        {
            _parts = options; //Get the meshes and materials being used
            if (isTriangle == false) //For normal floors
            {
                for (int i = 0; i < _pieces.Length; i++)
                { //Spawn the 4 corners with lids and bottoms
                    Spawn(_parts.cornerWalls[0],_pieces[i],90*i);
                    Spawn(_parts.cornerLid,_pieces[i],90*i);
                    Spawn(_parts.cornerBottom,_pieces[i],90*i);
                }
            }
            else //For triangle floors
            {
                //Instantiate the 90 degree corner piece of the tile
                Spawn(_parts.cornerWalls[0],_pieces[0]);
                Spawn(_parts.cornerLid,_pieces[0]);
                Spawn(_parts.cornerBottom,_pieces[0]);
                for (int i = 0; i < 2; i++)
                { //Instantiate the sides of the tile
                    Spawn(_parts.diagonalWalls[2+i],_pieces[1+i]);
                    Spawn(_parts.diagonalLids[2+i],_pieces[1+i]);
                    Spawn(_parts.diagonalBottoms[2+i],_pieces[1+i]);
                }
            }
        }
    }
}
