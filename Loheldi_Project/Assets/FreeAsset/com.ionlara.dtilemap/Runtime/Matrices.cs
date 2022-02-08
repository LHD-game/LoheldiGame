using UnityEngine;

namespace TileMap3D
{
    /// <summary>
    /// The Matrices Class is used to store the information of the all the tiles that have been created on the level. It is used by the
    /// Editor Level Spawner class to save this information and pass it along to the "Self Tiling Floor'. It has what is basically a
    /// 3 dimentional array with 3 different values per item, creating the equivalent of three 3D arrays.
    /// </summary>
    [System.Serializable]
    public class Matrices
    {
        [SerializeField]
        public AxisX[] matrix; //An array containing the X axis of the matrix

        //Initialize all the dimensions of the matrix with the given values
        public void Initialize(int width, int depth, int height)
        {
            matrix = new AxisX[width]; //Initialize the X axis array
            for (int i = 0; i < width; i++) //For each tile in the X axis initialize the tiles in the Z and Y axis
            {
                matrix[i] = new AxisX(); //Create a new Axis X "Container" for each of the tiles in the X Axis
                matrix[i].z = new AxisZ[depth]; //Initialize the Z axis array
                for (int j = 0; j < depth; j++) //For each tile in the Z Axis initialize the tiles in the Y Axis
                {
                    matrix[i].z[j] = new AxisZ(); //Create a new Axis Z "Container for each of the tiles in the Z Axis
                    matrix[i].z[j].y = new AxisY[height]; //Initialize the Y axis array
                }
            }
        }
        
        //Set if the matrix has a floor tile in the point at the given index
        public void SetTile(int x, int z, int y, bool has)
        {
            matrix[x].z[z].y[y].hasTile = has;
        }
        //Set the tile considered a floor in the matrix at the given index
        public void SetTileObject(int x, int z, int y, GameObject _tile)
        {
            matrix[x].z[z].y[y].tile = _tile;
        }
        //Set the tile in the matrix at the given index
        public void SetObject(int x, int z, int y, GameObject _object)
        {
            matrix[x].z[z].y[y].obj = _object;
        }
        //Get if the point in the matrix at the given index has a floor tile 
        public bool GetTile(int x, int z, int y)
        {
            return matrix[x].z[z].y[y].hasTile;
        }
        //Get the tile considered a floor from the point in the matrix the given index
        public GameObject GetTileObject(int x, int z, int y)
        {
            return matrix[x].z[z].y[y].tile;
        }
        //Get the tile from the point in the matrix at the given index
        public GameObject GetObject(int x, int z, int y)
        {
            return matrix[x].z[z].y[y].obj;
        }
        //The X axis of the matrix (Width)
        [System.Serializable]
        public class AxisX
        {
            [SerializeField]
            public AxisZ[] z; //An array containing the Z axis of the matrix
        }
        //The Z axis of the matrix (Depth)
        [System.Serializable]
        public class AxisZ
        {
            [SerializeField]
            public AxisY[] y; //An array containing the Y axis of the matrix
        }
        //The Y axis of the matrix (Height)
        [System.Serializable]
        public class AxisY
        {
            [SerializeField]
            public bool hasTile = false; //Does the point in the matrix contain a floor tile?
            [SerializeField]
            public GameObject tile = null; //The floor tile at the point in the grid
            [SerializeField]
            public GameObject obj = null; //The tile at the point in the grid
        }

    }
}
