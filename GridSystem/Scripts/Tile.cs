using UnityEngine;

namespace KarrottEngine.GridSystem
{
    /// <summary>
    /// Defines the tile type
    /// </summary>
    public enum TileType
    {
        EMPTY,
        PLAYER,
        ENEMY,
        MOVE,
        ATTACK,
        PATH,
    }

    /// <summary>
    /// Provides a data store for the tile type and position
    /// </summary>
    public class Tile 
    {
        public TileType Type { get; }
        public Vector2 Position { get; }
        public Tile(TileType type, Vector2 position)
        {
            this.Type = type;
            this.Position = position;
        }
    }
}