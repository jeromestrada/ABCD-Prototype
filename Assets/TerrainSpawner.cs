using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    [Tooltip(" 0: Top\n 1: Bottom\n 2: Left\n 3: Right")]
    [SerializeField] private int openingDirection;

    

    [SerializeField] private RoomTemplates roomTemplates;
    // maybe have a list of elite room templates to spawn here too,
    // boss rooms as well, so we can fight different bosses per Act/Chamber/Chapter/Kabanata
    private TerrainManager terrainManager;

    private bool LastRoom;

    public static System.Action OnTerrainSpawned;
    public static System.Action OnTerrainDespawned;

    private bool hasSpawned = false;
    private int rand;

    private void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        terrainManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<TerrainManager>();
        Invoke(nameof(SpawnTerrain), 0.2f);
    }

    public void SpawnTerrain()
    {
        if(!hasSpawned && terrainManager.HasRoom)
        {
            OnTerrainSpawned?.Invoke(); // this increases the terrain count so that future spawns will adjust accordingly
            switch (openingDirection)
            {
                // add logic to what type of rooms to spawn here, maybe when we have spawned 2-4 regular rooms already, the next room will be an elite room, etc...
                // the last room will always be a Boss room.
                case 0:
                    // spawn with a top opening
                    rand = Random.Range(0, roomTemplates.TopRooms.Length);
                    Instantiate(roomTemplates.TopRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 1:
                    // spawn bot opening
                    rand = Random.Range(0, roomTemplates.BottomRooms.Length);
                    Instantiate(roomTemplates.BottomRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 2:
                    // spawn left opening
                    rand = Random.Range(0, roomTemplates.LeftRooms.Length);
                    Instantiate(roomTemplates.LeftRooms[rand], transform.position, Quaternion.identity);
                    break;
                case 3:
                    // spawn right opening
                    rand = Random.Range(0, roomTemplates.RightRooms.Length);
                    Instantiate(roomTemplates.RightRooms[rand], transform.position, Quaternion.identity);
                    break;
                default:
                    break;
            }
            hasSpawned = true;
            // Test logic for the marker
            // Add an event that marks this terrain as a boss room/ spawn a boos room prefab instead
            if (terrainManager.LastRoom)
            {
                Instantiate(roomTemplates.LastRoomMarker, transform.position + (transform.up * 5), Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Terrain Spawner"))
        {
            /*OnTerrainDespawned?.Invoke();*/
            Destroy(gameObject);
        }
    }
}
