using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTemplates : MonoBehaviour
{
    [Header("Village Pieces")]
    [SerializeField] private List<GameObject> _centerPiecesV;
    [SerializeField] private List<GameObject> _leftPiecesV;
    [SerializeField] private List<GameObject> _rightPiecesV;
    [SerializeField] private List<GameObject> _topPiecesV;
    [SerializeField] private List<GameObject> _bottomPiecesV;
    [SerializeField] private List<GameObject> _cornerPiecesOneV;
    [SerializeField] private List<GameObject> _cornerPiecesTwoV;
    [SerializeField] private List<GameObject> _cornerPiecesThreeV;
    [SerializeField] private List<GameObject> _cornerPiecesFourV;

    [Header("Coastal Pieces")]
    [SerializeField] private List<GameObject> _centerPiecesC;
    [SerializeField] private List<GameObject> _leftPiecesC;
    [SerializeField] private List<GameObject> _rightPiecesC;
    [SerializeField] private List<GameObject> _topPiecesC;
    [SerializeField] private List<GameObject> _bottomPiecesC;
    [SerializeField] private List<GameObject> _cornerPiecesOneC;
    [SerializeField] private List<GameObject> _cornerPiecesTwoC;
    [SerializeField] private List<GameObject> _cornerPiecesThreeC;
    [SerializeField] private List<GameObject> _cornerPiecesFourC;

    public List<GameObject> CenterPiecesV => _centerPiecesV;
    public List<GameObject> LeftPiecesV => _leftPiecesV;
    public List<GameObject> RightPiecesV => _rightPiecesV;
    public List<GameObject> TopPiecesV => _topPiecesV;
    public List<GameObject> BottomPiecesV => _bottomPiecesV;
    public List<GameObject> CornerPiecesOneV => _cornerPiecesOneV;
    public List<GameObject> CornerPiecesTwoV => _cornerPiecesTwoV;
    public List<GameObject> CornerPiecesThreeV => _cornerPiecesThreeV;
    public List<GameObject> CornerPiecesFourV => _cornerPiecesFourV;

    
    public List<GameObject> CenterPiecesC => _centerPiecesC;
    public List<GameObject> LeftPiecesC => _leftPiecesC;
    public List<GameObject> RightPiecesC => _rightPiecesC;
    public List<GameObject> TopPiecesC => _topPiecesC;
    public List<GameObject> BottomPiecesC => _bottomPiecesC;
    public List<GameObject> CornerPiecesOneC => _cornerPiecesOneC;
    public List<GameObject> CornerPiecesTwoC => _cornerPiecesTwoC;
    public List<GameObject> CornerPiecesThreeC => _cornerPiecesThreeC;
    public List<GameObject> CornerPiecesFourC => _cornerPiecesFourC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
