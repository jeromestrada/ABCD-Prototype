using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    [SerializeField] private GameObject[] _topRooms;
    [SerializeField] private GameObject[] _bottomRooms;
    [SerializeField] private GameObject[] _rightRooms;
    [SerializeField] private GameObject[] _leftRooms;

    [SerializeField] private GameObject _lastRoomMarker;

    public GameObject[] TopRooms => _topRooms;
    public GameObject[] BottomRooms => _bottomRooms;
    public GameObject[] RightRooms => _rightRooms;
    public GameObject[] LeftRooms => _leftRooms;
    public GameObject LastRoomMarker => _lastRoomMarker;


}
