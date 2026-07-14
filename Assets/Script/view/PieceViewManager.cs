
using UnityEngine;

public class PieceViewManager : MonoBehaviour
{
    private PieceView[,] views = new PieceView[5, 7];

    [SerializeField] private GameObject whitePawn;
    [SerializeField] private GameObject blackPawn;
    [SerializeField] private GameObject whiteRook;
    [SerializeField] private GameObject blackRook;
    [SerializeField] private GameObject whiteKnight;
    [SerializeField] private GameObject blackKnight;
    [SerializeField] private GameObject whiteKing;
    [SerializeField] private GameObject blackKing;
    [SerializeField] private GameObject whiteBishop;
    [SerializeField] private GameObject blackBishop;

    [SerializeField] ObjectViewManager objectViewManager;

    public void Initialize(GameState state)
    {
        SpawnPieces(state);
    }

    void SpawnPieces(GameState state)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 7; y++)
            {
                Piece piece = state.Board.Get(x, y);

                if (piece.IsEmpty)
                    continue;

                GameObject obj =
                    Instantiate(GetPrefab(piece));

                PieceView view =
                    obj.GetComponent<PieceView>();

                view.SetPositionImmediate(x, y);

                views[x, y] = view;
            }
        }
    }

    public void ApplyMove(Move move, GameState state)
    {
        PieceView view = views[move.FromX, move.FromY];

        if (view == null)
            return;

        views[move.FromX, move.FromY] = null;
        views[move.ToX, move.ToY] = view;

        view.MoveTo(move.ToX, move.ToY);

        if (move.ToX == state.Mine.GetMineX() && move.ToY == state.Mine.GetMineY() && objectViewManager.isMineExist == true)
        {
            Destroy(view.gameObject);
        }
    }

    GameObject GetPrefab(Piece piece)
    {
        //à¯êîÇ…ì¸ÇÍÇΩPieceTypeÇ…ÇÊÇ¡ÇƒÅAÇªÇÍÇ…ëŒâûÇµÇΩPrefabÇï‘Ç∑
        Debug.Log(piece.Type);
        if (piece.Type == PieceType.Pawn)
            return piece.Color == PieceColor.White ? whitePawn : blackPawn;

        if (piece.Type == PieceType.Rook)
            return piece.Color == PieceColor.White ? whiteRook : blackRook;

        if (piece.Type == PieceType.Knight)
            return piece.Color == PieceColor.White ? whiteKnight : blackKnight;

        if (piece.Type == PieceType.King)
            return piece.Color == PieceColor.White ? whiteKing : blackKing;

        if (piece.Type == PieceType.Bishop)
            return piece.Color == PieceColor.White ? whiteBishop : blackBishop;

        return null;
    }
}