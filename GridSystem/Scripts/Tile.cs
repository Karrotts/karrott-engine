namespace KarrottEngine.GridSystem
{
    /// <summary>
    /// 
    /// </summary>
    public enum TileType
    {
        EMPTY,
        PLAYER,
        ENEMY,
        MOVE,
        ATTACK
    }

    /// <summary>
    /// 
    /// </summary>
    public class Tile 
    {
        public TileType Type { get; }
        public Tile(TileType type)
        {
            this.Type = type;
        }
    }
}