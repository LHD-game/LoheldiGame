using System.Collections.Generic;
using UnityEngine;

namespace TileMap3D
{ 
    /// <summary>
    /// The Tileset Scritable Object is used by the tool as the container of the user's Tilesets. Each instance has a List containing 
    /// all the tiles (Tile Values) included in each Tileset.
    /// </summary>
    public class Tileset : ScriptableObject
    {
        //This Scriptable object only contains a list filled with instances of Tiles with all their information,...
        //the main use for this is to save the user Tilesets and access them at a later time.
        public List<TileValues> tileset = new List<TileValues>();
    }
}