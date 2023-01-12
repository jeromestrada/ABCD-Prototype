using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomPiece : MonoBehaviour
{
    [SerializeField] private TerrainSpot terrainSpot;
    [SerializeField] private PieceTemplates pieceTemplates;
    [SerializeField] private TerrainType terrainType;

    private void Start()
    {
        pieceTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<PieceTemplates>();
    }
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
        if(terrainType == TerrainType.Village)
        {
            switch (terrainSpot)
            {
                case TerrainSpot.Center:
                    piece = pieceTemplates.CenterPiecesV[Random.Range(0, pieceTemplates.CenterPiecesV.Count)];
                    break;
                case TerrainSpot.Left:
                    piece = pieceTemplates.LeftPiecesV[Random.Range(0, pieceTemplates.LeftPiecesV.Count)];
                    break;
                case TerrainSpot.Right:
                    piece = pieceTemplates.RightPiecesV[Random.Range(0, pieceTemplates.RightPiecesV.Count)];
                    break;
                case TerrainSpot.Top:
                    piece = pieceTemplates.TopPiecesV[Random.Range(0, pieceTemplates.TopPiecesV.Count)];
                    break;
                case TerrainSpot.Bottom:
                    piece = pieceTemplates.BottomPiecesV[Random.Range(0, pieceTemplates.BottomPiecesV.Count)];
                    break;
                case TerrainSpot.Corner1:
                    piece = pieceTemplates.CornerPiecesOneV[Random.Range(0, pieceTemplates.CornerPiecesOneV.Count)];
                    break;
                case TerrainSpot.Corner2:
                    piece = pieceTemplates.CornerPiecesTwoV[Random.Range(0, pieceTemplates.CornerPiecesTwoV.Count)];

                    break;
                case TerrainSpot.Corner3:
                    piece = pieceTemplates.CornerPiecesThreeV[Random.Range(0, pieceTemplates.CornerPiecesThreeV.Count)];

                    break;
                case TerrainSpot.Corner4:
                    piece = pieceTemplates.CornerPiecesFourV[Random.Range(0, pieceTemplates.CornerPiecesFourV.Count)];

                    break;
                default:
                    break;
            }
        }
        else if(terrainType == TerrainType.Coastal)
        {
            switch (terrainSpot)
            {
                case TerrainSpot.Center:
                    piece = pieceTemplates.CenterPiecesC[Random.Range(0, pieceTemplates.CenterPiecesC.Count)];
                    break;
                case TerrainSpot.Left:
                    piece = pieceTemplates.LeftPiecesC[Random.Range(0, pieceTemplates.LeftPiecesC.Count)];
                    break;
                case TerrainSpot.Right:
                    piece = pieceTemplates.RightPiecesC[Random.Range(0, pieceTemplates.RightPiecesC.Count)];
                    break;
                case TerrainSpot.Top:
                    piece = pieceTemplates.TopPiecesC[Random.Range(0, pieceTemplates.TopPiecesC.Count)];
                    break;
                case TerrainSpot.Bottom:
                    piece = pieceTemplates.BottomPiecesC[Random.Range(0, pieceTemplates.BottomPiecesC.Count)];
                    break;
                case TerrainSpot.Corner1:
                    piece = pieceTemplates.CornerPiecesOneC[Random.Range(0, pieceTemplates.CornerPiecesOneC.Count)];
                    break;
                case TerrainSpot.Corner2:
                    piece = pieceTemplates.CornerPiecesTwoC[Random.Range(0, pieceTemplates.CornerPiecesTwoC.Count)];

                    break;
                case TerrainSpot.Corner3:
                    piece = pieceTemplates.CornerPiecesThreeC[Random.Range(0, pieceTemplates.CornerPiecesThreeC.Count)];

                    break;
                case TerrainSpot.Corner4:
                    piece = pieceTemplates.CornerPiecesFourC[Random.Range(0, pieceTemplates.CornerPiecesFourC.Count)];

                    break;
                default:
                    break;
            }
        }
        
        Instantiate(piece, transform.position, rotation, transform.parent);
        Destroy(gameObject);
    }
}

public enum TerrainSpot { Center, Left, Right, Top, Bottom, Corner1, Corner2, Corner3, Corner4 };
public enum TerrainType { Village, Coastal }
