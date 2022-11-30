using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomPiece : MonoBehaviour
{
    [SerializeField] private List<GameObject> _piecePrefabs = new List<GameObject>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            GeneratePiece();
        }
    }

    public void GeneratePiece()
    {
        GameObject piece = _piecePrefabs[Random.Range(0, _piecePrefabs.Count)];
        Instantiate(piece, transform.position, Quaternion.Euler(new Vector3(0, 90 * Random.Range(0, 4), 0)));
        Destroy(gameObject);
    }
}
