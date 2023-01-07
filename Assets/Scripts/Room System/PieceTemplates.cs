using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceTemplates : MonoBehaviour
{
    [SerializeField] private List<GameObject> _centerPieces;
    [SerializeField] private List<GameObject> _leftPieces;
    [SerializeField] private List<GameObject> _rightPieces;
    [SerializeField] private List<GameObject> _topPieces;
    [SerializeField] private List<GameObject> _bottomPieces;
    [SerializeField] private List<GameObject> _cornerPieces;

    public List<GameObject> CenterPieces => _centerPieces;
    public List<GameObject> LeftPieces => _leftPieces;
    public List<GameObject> RightPieces => _rightPieces;
    public List<GameObject> TopPieces => _topPieces;
    public List<GameObject> BottomPieces => _bottomPieces;
    public List<GameObject> CornerPieces => _cornerPieces;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}