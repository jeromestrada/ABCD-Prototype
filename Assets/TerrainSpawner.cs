using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    [Tooltip(" 0: Top\n 1: Bottom\n 2: Left\n 3: Right")]
    [SerializeField] private int openingDirection;

    [SerializeField] private RoomTemplates roomTemplates;

    private bool hasSpawned = false;
    private int rand;

    private void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke(nameof(SpawnTerrain), 0.2f);
    }

    public void SpawnTerrain()
    {
        if(!hasSpawned)
        {
            switch (openingDirection)
            {
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Terrain Spawner"))
        {
            Destroy(gameObject);
        }
    }
}
