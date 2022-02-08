using UnityEngine;

namespace TileMap3D
{
    /// <summary>
    /// The Tile Values Class is used to store all the information of each of the Tiles in the user's Tileset. The Tileset Class is just
    /// a collection of different TileValues instances.
    /// </summary>
    [System.Serializable]
    public class TileValues
    {
        public GameObject prefab; //The tile prefab
        public string prefabName; //The tile name
        public Texture2D image; //The tile icon
        public bool canRotate; //Can the tile be rotated?
        public int turns; //If the tile can be rotated, how many 90 degrees turns has the user set it to?
        public bool isFloor; //Is the tile being used as floor?
        public bool needsFloor; //If the tile is not a floor, does it need a floor below it?
        public bool eraser; //Is the tile the eraser? (Only used with the default eraser)
        public bool isTriangle; //Is the tile a triangle? (Only used with the self tiling floor)
    }
}