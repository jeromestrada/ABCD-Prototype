using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomPiece : MonoBehaviour
{
    [SerializeField] private TerrainSpot terrainSpot;
    [SerializeField] private List<GameObject> _piecePrefabs = new List<GameObject>();
    [SerializeField] private GameObject[] _centerRooms;
    [SerializeField] private GameObject[] _leftRooms;
    [SerializeField] private GameObject[] _rightRooms;
    [SerializeField] private GameObject[] _topRooms;
    [SerializeField] private GameObject[] _bottomRooms;
    [SerializeField] private GameObject[] _cornerRooms;

    public GameObject[] CenterRooms => _centerRooms;
    public GameObject[] LeftRooms => _leftRooms;
    public GameObject[] RightRooms => _rightRooms;
    public GameObject[] TopRooms => _topRooms;
    public GameObject[] BottomRooms => _bottomRooms;
    public GameObject[] CornerRooms => _cornerRooms;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GeneratePiece();
        }
    }

    public void GeneratePiece()
    {
        GameObject piece = null;
        Quaternion rotation = Quaternion.identity;
        switch (terrainSpot)
        {
            case TerrainSpot.Center:
                piece = CenterRooms[Random.Range(0, _piecePrefabs.Count)];
                break;
            case TerrainSpot.Left:
                piece = LeftRooms[Random.Range(0, _piecePrefabs.Count)];
                break;
            case TerrainSpot.Right:
                piece = RightRooms[Random.Range(0, _piecePrefabs.Count)];
                break;
            case TerrainSpot.Top:
                piece = TopRooms[Random.Range(0, _piecePrefabs.Count)];
                break;
            case TerrainSpot.Bottom:
                piece = BottomRooms[Random.Range(0, _piecePrefabs.Count)];
                break;
            case TerrainSpot.Corner1:
                piece = CornerRooms[Random.Range(0, _piecePrefabs.Count)];
                break;
            case TerrainSpot.Corner2:
                piece = CornerRooms[Random.Range(0, _piecePrefabs.Count)];
                rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
            case TerrainSpot.Corner3:
                piece = CornerRooms[Random.Range(0, _piecePrefabs.Count)];
                rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case TerrainSpot.Corner4:
                piece = CornerRooms[Random.Range(0, _piecePrefabs.Count)];
                rotation = Quaternion.Euler(new Vector3(0, -90, 0));
                break;
            default:
                break;
        }
        Instantiate(piece, transform.position, rotation, transform.parent);
        Destroy(gameObject);
    }
}

public enum TerrainSpot { Center, Left, Right, Top, Bottom, Corner1, Corner2, Corner3, Corner4 };
