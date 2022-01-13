using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls all tileobject generation and functionality
/// </summary>
[System.Serializable]
public class TileController : MonoBehaviour
{
    /// <summary>
    /// Total count of all tileobjects to be spawned
    /// </summary>
    [SerializeField, Tooltip("Count of all tile objects spawned")]
    int TileObjectGenerationLength = 10;

    /// <summary>
    /// A refrence to the original tileobject prefab
    /// </summary>
    [SerializeField, Tooltip("A refrence to the prefab of the tile object")]
    TileObject TileObjectPrefab;

    /// <summary>
    /// A queued list of spawned tileobjects that is queued from first created to last created.
    /// </summary>
    Queue<TileObject> TileObjectSpawned = new Queue<TileObject>();

    /// <summary>
    /// Most recent spawned tileobject
    /// </summary>
    TileObject TileObjectRecent = null;

    /// <summary>
    /// The offset that represents the furthest off screen an object will travel. Object also starts spawning from offset point
    /// </summary>
    [SerializeField, Tooltip("A refrence to the starting position and furthest a object can travel")]
    int TileObjectOffset;

    public delegate void SetSpeedAction(float Speed);
    /// <summary>
    /// An action run when speed is changed
    /// </summary>
    public SetSpeedAction _setSpeed;

    /// <summary>
    /// The speed all tiles are to move at
    /// </summary>
    [SerializeField, Tooltip("Speed for all tiles to move at")]
    float SpeedModifer = 1;
    /// <summary>
    /// Modify the speed of all gameobjects
    /// </summary>
    public float _modifySpeed
    {
        get
        {
            return SpeedModifer;
        }
        set
        {
            SpeedModifer = value;
            _setSpeed?.Invoke(value);
        }
    }

    /// <summary>
    /// Set up all tiles to spawn in scene and appear in a uniform order
    /// </summary>
    public void _SetupTileObjects()
    {
        //If nothing is spawned yet create all the objects to be used
        if(TileObjectSpawned.Count == 0)
        {
            GameObject SpawnedTiles = new GameObject("SpawnedTileCollection");
            TileObject RecentSpawnedRefrence;
            //Create each object and run needed functionality
            for(int i = 0; i < TileObjectGenerationLength; i++)
            {
                RecentSpawnedRefrence = Instantiate(TileObjectPrefab, SpawnedTiles.transform).GetComponent<TileObject>();
                TileObjectSpawned.Enqueue(RecentSpawnedRefrence);
                TileObjectRecent = RecentSpawnedRefrence;
                _setSpeed += RecentSpawnedRefrence._modifySpeed;
            }
        }

        Vector3 ObjectSpawnPosition = Vector3.zero;
        ObjectSpawnPosition.x = TileObjectOffset;
        int ObjectSpawnDistance = 1;
        //Set each object in position and assign needed vales
        for(int i = 0; i < TileObjectSpawned.Count; i++)
        {
            //Cycle tileobjects
            TileObject t = TileObjectSpawned.Dequeue();
            TileObjectSpawned.Enqueue(t);

            //Run functionality
            t.transform.position = ObjectSpawnPosition;
            ObjectSpawnPosition.x += ObjectSpawnDistance;
        }
    }
}
