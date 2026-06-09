using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask pieceLayer;
    [SerializeField] private LayerMask tileLayer;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private PieceViewManager pieceViewManager;

    private List<Move> currentMoves;

    private PieceView currentHoverPiece;
    private PieceView selectedPiece;
    private Tile currentTile;

    private bool selectingMove;

    void Update()
    {
        if (!selectingMove)
        {
            SelectPiece();
        }
        else
        {
            SelectDestination();
        }
    }

    void SelectPiece()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, pieceLayer))
        {
            PieceView piece = hit.collider.GetComponent<PieceView>();

            if (piece != null)
            {
                //前のハイライトを消す
                if (currentHoverPiece != null && currentHoverPiece != piece)
                {
                    currentHoverPiece.SetHighLight(PieceView.HighLightType.None);
                }
                currentHoverPiece = piece;
                currentHoverPiece.SetHighLight(PieceView.HighLightType.Hover);

                //駒をクリックしているときに移動できる範囲を表示する
                if (Input.GetMouseButtonDown(0))
                {
                    selectedPiece = piece;

                    currentMoves = MoveGenerator.GenerateMoves(gameManager.State)
                        .FindAll(m =>
                            m.FromX == piece.x &&
                            m.FromY == piece.y);

                    tileManager.ViewArea(piece.x, piece.y);

                    selectingMove = true;
                }
                return;
            }
        }
    }

    void SelectDestination()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, tileLayer))
        {
            currentTile = hit.collider.GetComponent<Tile>();
        }

        if (Input.GetMouseButtonDown(0))
        {
            foreach (Move move in currentMoves)
            {
                if (move.ToX == currentTile.tileX &&
                    move.ToY == currentTile.tileY)
                {
                    gameManager.ApplyMove(move);
                    break;
                }
            }

            tileManager.ClearHighLight();

            selectingMove = false;
        }
    }


}