using System.Collections.Generic;
using KarrottEngine.EntitySystem;
using UnityEngine;

namespace KarrottEngine.GridSystem
{
    /// <summary>
    /// Grid system entry point, contains all publicly available methods for grid operations 
    /// and contains the grid repository
    /// </summary>
    public static class KEGrid
    {
        // EntityRepostitory probably should be something more memory efficent
        // for now a generic list will accomplish the job needed of the grid system.
        public static List<Entity> EntityRepository = new List<Entity>();

        // Requires a gameobject in the scene with the tag GridController and the GridRenderer script attached.
        private static GridRenderer Renderer = GameObject.FindGameObjectWithTag("GridController").GetComponent<GridRenderer>();

        /// <summary>
        /// Gets the mouse position in world space, normalized to the grid coordinates.
        /// </summary>
        /// <returns>Normalized Vector2 Coordinates</returns>
        public static Vector2 GetMouseGridPosition()
        {
            Vector3 rawMousePosition = Input.mousePosition;
            Vector3 gameworldPosition = Camera.main.ScreenToWorldPoint(rawMousePosition);
            return new Vector2(Mathf.Round(gameworldPosition.x), Mathf.Round(gameworldPosition.y));
        }

        /// <summary>
        /// Given a position, find that first entity at that position. Returns null if no entity was found.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Entity GetEntityAtPosition(Vector2 position)
        {
            foreach (Entity entity in EntityRepository)
            {
                if (entity.EntityObject.transform.position.Equals(position)) 
                    return entity;
            }
            return null;
        }

        /// <summary>
        /// Finds and returns a list of entities that are at a current position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Entity[] GetEntitiesAtPosition(Vector2 position)
        {
            List<Entity> entities = new List<Entity>();
            foreach (Entity entity in EntityRepository)
            {
                if (entity.EntityObject.transform.position.Equals(position)) 
                    entities.Add(entity);
            }
            return entities.ToArray();
        }

        /// <summary>
        /// Finds all Entities with a certian type
        /// </summary>
        /// <returns></returns>
        public static Entity[] GetEntitiesByType(EntityType type)
        {
            List<Entity> entities = new List<Entity>();
            foreach (Entity entity in EntityRepository)
            {
                if (entity.Type == type) 
                    entities.Add(entity);
            }
            return entities.ToArray();
        }

        /// <summary>
        /// Inserts Entity in Repository. Entity should have an instantiated gameobject.
        /// If you are trying to add a new gameobject to the grid, use CreateEntity() instead.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool AddEntity(Entity entity)
        {
            EntityRepository.Add(entity);
            return (EntityRepository.Contains(entity));
        }

        /// <summary>
        /// Use this method if you want to insert a new game object into the grid
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="type"></param>
        /// <param name="position"></param>
        public static void CreateEntity(GameObject gameObject, EntityType type, Vector2 position)
        {
            Renderer.RenderEntity(gameObject, type, position);
        }

        /// <summary>
        /// Deletes a specific entity
        /// </summary>
        /// <param name="entity"></param>
        public static void DeleteEntity(Entity entity)
        {
            Renderer.DestoryGameObject(entity.EntityObject);
            EntityRepository.Remove(entity);
        }

        /// <summary>
        /// Deletes all entities with a specific entity type
        /// </summary>
        /// <param name="type"></param>
        public static void DeleteEntitesWithType(EntityType type)
        {
            Entity[] entities = GetEntitiesByType(type);
                for(int i = 0; i < entities.Length; i++) {
                    if (entities[i] != null)
                        DeleteEntity(entities[i]);
                }
        }

        /// <summary>
        /// Deletes all entities with a specific entity type and tile type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tileType"></param>
        public static void DeleteEntitesWithType(EntityType type, TileType tileType)
        {
            Entity[] entities = GetEntitiesByType(type);
                for(int i = 0; i < entities.Length; i++) {
                    if (entities[i] != null && entities[i].SpecialType == (int)tileType)
                        DeleteEntity(entities[i]);
                }
        }

        /// <summary>
        /// Deletes entity at a specific position. Optional parameter to delete all entities at that position.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="deleteAll">Delete all at that position</param>
        public static void DeleteEntityAtPosition(Vector2 position, bool deleteAll = true)
        {
            if (deleteAll) 
            {
                Entity[] entities = GetEntitiesAtPosition(position);
                for(int i = 0; i < entities.Length; i++) {
                    if (entities[i] != null)
                        DeleteEntity(entities[i]);
                }
            }
            else
            {
                Entity entity = GetEntityAtPosition(position);
                if (entity != null)
                    DeleteEntity(entity);
            }
        }

        /// <summary>
        /// Checks to see if the provided position is free on the grid.
        /// Free means there is nothing currently in that space.
        /// </summary>
        /// <param name="position">Normalized Grid Position</param>
        /// <returns>True if empty</returns>
        public static bool IsPositionEmpty(Vector2 position)
        {
            return GetEntityAtPosition(position) == null;
        }

        /// <summary>
        /// Checks to see if the provided position is free on the grid.
        /// Free means there is nothing currently in that space.
        /// Excludes check if in the exclude types array
        /// </summary>
        /// <param name="postion"></param>
        /// <param name="excludeTypes"></param>
        /// <returns></returns>
        public static bool IsPositionEmpty(Vector2 postion, EntityType[] excludeTypes)
        {
            Entity[] entities = GetEntitiesAtPosition(postion);
            int total = entities.Length;
            foreach(Entity entity in entities) 
            {
                foreach (EntityType type in excludeTypes)
                {
                    if (entity.Type == type) {
                        total--;
                        break;
                    }
                }
            }
            return total == 0;
        }

        /// <summary>
        /// Loads pattern information from resources file and creates them in the grid and game world
        /// </summary>
        /// <param name="patternId"></param>
        /// <param name="offset"></param>
        public static void LoadPattern(int patternId, Vector2 offset)
        {
            List<Tile> tiles = PatternLoader.LoadTiles(patternId, offset);
            foreach (Tile tile in tiles)
            {
                Renderer.RenderEntityFromTile(tile);
            }
        }

        /// <summary>
        /// Finds the nearest possible path between two points
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>An array in move order</returns>
        public static Vector2[] FindPath(Vector2 start, Vector2 end)
        {
            //TODO
            return new Vector2[0];
        } 

        /// <summary>
        /// Displays data of the grid system to the debug console.
        /// </summary>
        public static void ShowDebugMessage()
        {
            Entity entity = GetEntityAtPosition(GetMouseGridPosition());
            string mousePosEntity = entity == null ? "None" : entity.Type.ToString();
            string specialType = entity == null || entity.Type != EntityType.TILE ? "None" : ((TileType)entity.SpecialType).ToString(); 
            string message = 
                  $"--- Grid System Debug --- \n" 
                + $"Current Grid Count: {EntityRepository.Count}\n"
                + $"Mouse Currently At: {GetMouseGridPosition()}\n"
                + $"Entity at Mouse Position: {mousePosEntity}\n"
                + $"Has Tile Type: {specialType}\n"
                + $"-------------------------";
            Debug.Log(message);
        }
    }
}
