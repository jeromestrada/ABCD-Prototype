using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomPiece : MonoBehaviour
{
    [SerializeField] private TerrainSpot terrainSpot;
    [SerializeField] private PieceTemplates pieceTemplates;

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
                piece = pieceTemplates.CenterPieces[Random.Range(0, pieceTemplates.CenterPieces.Count)];
                break;
            case TerrainSpot.Left:
                piece = pieceTemplates.LeftPieces[Random.Range(0, pieceTemplates.LeftPieces.Count)];
                break;
            case TerrainSpot.Right:
                piece = pieceTemplates.RightPieces[Random.Range(0, pieceTemplates.RightPieces.Count)];
                break;
            case TerrainSpot.Top:
                piece = pieceTemplates.TopPieces[Random.Range(0, pieceTemplates.TopPieces.Count)];
                break;
            case TerrainSpot.Bottom:
                piece = pieceTemplates.BottomPieces[Random.Range(0, pieceTemplates.BottomPieces.Count)];
                break;
            case TerrainSpot.Corner1:
                piece = pieceTemplates.CornerPieces[Random.Range(0, pieceTemplates.CornerPieces.Count)];
                break;
            case TerrainSpot.Corner2:
                piece = pieceTemplates.CornerPieces[Random.Range(0, pieceTemplates.CornerPieces.Count)];
                rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
            case TerrainSpot.Corner3:
                piece = pieceTemplates.CornerPieces[Random.Range(0, pieceTemplates.CornerPieces.Count)];
                rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                break;
            case TerrainSpot.Corner4:
                piece = pieceTemplates.CornerPieces[Random.Range(0, pieceTemplates.CornerPieces.Count)];
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
