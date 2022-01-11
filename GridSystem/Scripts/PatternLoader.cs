using System.Collections.Generic;
using UnityEngine;

namespace KarrottEngine.GridSystem
{
    /// <summary>
    /// 
    /// </summary>
    public static class PatternLoader
    {
        /// <summary>
        /// Loads in image pattern from the resources folder
        /// Image must be a 110x110 image
        /// [Green (0,255,0) - Player Tile]
        /// [Red (255,0,0) - Attack Tile]
        /// [Blue (0,0,255) - Move]
        /// [Black - None]
        /// </summary>
        /// <param name="patternIndex"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static List<Tile> LoadTiles(int patternIndex, Vector2 offset)
        {
            int y = patternIndex > 9 ? (patternIndex / 10) * 11 : 0;
            int x = patternIndex > 9 ? (patternIndex % 10) * 11 : patternIndex * 11;

            //Load a Texture2D (Assets/Resources/Data/Move_Patterns.png)
            Texture2D patterns = Resources.Load<Texture2D>("Data/Move_Patterns");
            List<Tile> Tiles = new List<Tile>();
            for (int i = 0; i < 11; i++) 
            {
                for (int j = 0; j < 11; j++) 
                {
                    Vector2 position = new Vector2();
                    TileType type = TileType.EMPTY;
                    
                    position.y = i - 5 + offset.y;
                    position.x = 5 - j + offset.x;

                    Color pixelColor = patterns.GetPixel(x + j, y + i);
                    if (pixelColor.Equals(Color.black))
                        type = TileType.EMPTY;
                    if (pixelColor.Equals(Color.green))
                        type = TileType.PLAYER;
                    if (pixelColor.Equals(Color.red))
                        type = TileType.ATTACK;
                    if (pixelColor.Equals(Color.blue))
                        type = TileType.MOVE;
                    
                    Tiles.Add(new Tile(type, position));
                }
            }
            return Tiles;
        }
    }
}
