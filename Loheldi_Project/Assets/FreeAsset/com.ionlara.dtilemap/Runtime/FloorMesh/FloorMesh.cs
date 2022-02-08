using UnityEngine;

namespace TileMap3D
{
    /// <summary>
    /// The Floor Mesh Scriptable Object is used to store information needed by the Self Tiling Floor, including the different meshes used
    /// by this system, along with the material it should use for the floor tiles.
    /// </summary>
    [CreateAssetMenu(fileName= "New Floor", menuName = "ScriptableObjects/Floors" )]
    public class FloorMesh : ScriptableObject
    {
        public Material material; //The material used by the Self Tiling floors
        public GameObject squareLid; //The basic square lid mesh on top of the floors
        public GameObject squareBottom; //The basic square bottom mesh of the floors
        //The meshes used for corners walls
        // 0 = Normal mesh (No tiles above)
        // 1 = Connection mesh (Tile above)
        // 2 = Special connection mesh
        public GameObject[] cornerWalls = new GameObject[3]; 
        public GameObject cornerLid; //The mesh of the corner's lid
        public GameObject cornerBottom; //The mesh of the corner's bottom
        
        // The 4 different meshes used for the straight walls
        // 0 = Normal mesh (No tiles above)
        // 1 = Normal Connection mesh (Tile above both on the tile and its corresponding neighbour)
        // 2 = Special Connection used used for NorthEast and SouthWest (Neighbour has no above tile)
        // 3 = Special Connection used used for NorthWest and SouthEast (Neighbour has no above tile)
        public GameObject[] straightWalls = new GameObject[4];
        public GameObject straightBottom; //The mesh used for the bottom of straight pieces
       
        //The meshes used to connect with other floors diagonally
        // 0 = Normal mesh NW & SE (No tiles above)
        // 1 = Normal mesh NE & SW (No tiles above)
        // 2 = Normal Connection mesh NW & SE (Tiles above both this tile and its correspoding neighbours)
        // 3 = Normal Connection mesh NE & SW (Tiles above both this tile and its correspoding neighbours)
        // 4 = Special Connection mesh NW & SE (Corresponding neighbours have no tiles above)
        // 5 = Special Connection mesh NE & SW (Corresponding neighbours have no tiles above)
        public GameObject[] curvedWalls = new GameObject[6]; 
        //The curved Lid meshes
        // 0 = NW & SE 
        // 1 = NE & SW 
        public GameObject[] curveLids = new GameObject[2]; 
        //The curved Lid meshes
        // 0 = NW & SE 
        // 1 = NE & SW 
        public GameObject[] curveBottoms = new GameObject[2];
        public GameObject[] halfBottoms = new GameObject[2]; //The half bottom meshes for the curves
        
        //The 5 different diagonal side meshes used for the triangle floor
        // 0 = Open diagonal side for the NE
        // 1 = Open diagonal side for the SW
        // 2 = Diagonal mesh used for the NE
        // 3 = Diagonal mesh used for the SW
        // 4 = Straight mesh
        public GameObject[] diagonalWalls = new GameObject[5];
        //The 4 different diagonal top meshes used for the triangle floor
        // 0 = Open diagonal lid for the NE
        // 1 = Open diagonal lid for the SW
        // 2 = Closed diagonal lid for the NE
        // 3 = Closed diagonal lid for the SW
        // 4 = Straight mesh
        public GameObject[] diagonalLids = new GameObject[5];
        //The 4 different diagonal top meshes used for the triangle floor
        // 0 = Open diagonal bottom for the NE
        // 1 = Open diagonal bottom for the SW
        // 2 = Closed diagonal bottom for the NE
        // 3 = Closed diagonal bottom for the SW
        // 4 = Straight mesh
        public GameObject[] diagonalBottoms = new GameObject[5];
    }
}
