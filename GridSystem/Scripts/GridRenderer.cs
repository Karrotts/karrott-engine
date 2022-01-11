using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KarrottEngine.EntitySystem;
namespace KarrottEngine.GridSystem
{
    /// <summary>
    /// Primary gameobject renderer, used to render game object to the world.
    /// </summary>
    public class GridRenderer : MonoBehaviour
    {
        public GameObject MoveTile;
        public GameObject AttackTile;
        public GameObject PlayerTile;
        public GameObject EnemyTile;

        /// <summary>
        /// Renders a single entity, must pass in the gameobject you want to create and position.
        /// </summary>
        /// <param name="entityObject"></param>
        /// <param name="position"></param>
        public void RenderEntity(GameObject entityObject, EntityType type, Vector2 position) 
        {
            GameObject entity = Instantiate(entityObject, position, Quaternion.identity);
            KEGrid.AddEntity(new Entity(entity, type));
        }

        /// <summary>
        /// Renders an Entity from tile data
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public void RenderEntityFromTile(Tile tile)
        {
            GameObject rendered = null;
            switch(tile.Type) {
                case TileType.MOVE:
                    if (MoveTile)
                        rendered = Instantiate(MoveTile, tile.Position, Quaternion.identity);
                    break;
                case TileType.ATTACK:
                    if (AttackTile)
                        rendered = Instantiate(AttackTile, tile.Position, Quaternion.identity);
                    break;
                case TileType.PLAYER:
                    if (PlayerTile)
                        rendered = Instantiate(PlayerTile, tile.Position, Quaternion.identity);
                    break;
                case TileType.ENEMY:
                    if (EnemyTile)
                        rendered = Instantiate(EnemyTile, tile.Position, Quaternion.identity);
                    break;
            }
            if (rendered != null) {
                Entity entity = new Entity(rendered, EntityType.TILE);
                entity.SpecialType = (int)tile.Type;
                KEGrid.AddEntity(entity);
            }
        }

        public void DestoryGameObject(GameObject gameObject) => Destroy(gameObject);
    }   
}
