using UnityEngine;

public class GameController : MonoBehaviour
{
    GameState state;

    PieceView[,] views = new PieceView[8,8];

    [SerializeField] GameObject whitePawn;
    [SerializeField] GameObject blackPawn;
    [SerializeField] GameObject whiteRook;
    [SerializeField] GameObject blackRook;

    void Start()
    {
        state = GameInitializer.CreateInitial();
        SpawnPieces();
    }

    void SpawnPieces()
    {
        for (int x = 0; x < 8; x++)
        for (int y = 0; y < 8; y++)
        {
            var piece = state.Board.Get(x, y);
            if (piece.IsEmpty) continue;

            var prefab = GetPrefab(piece);
            var obj = Instantiate(prefab);

            var view = obj.GetComponent<PieceView>();
            view.SetPosition(x, y);

            views[x, y] = view;
        }
    }

    GameObject GetPrefab(Piece piece)
    {
        if (piece.Type == PieceType.Pawn)
            return piece.Color == PieceColor.White ? whitePawn : blackPawn;

        if (piece.Type == PieceType.Rook)
            return piece.Color == PieceColor.White ? whiteRook : blackRook;

        return null;
    }


    public void ApplyMove(Move move)
    {
        // Core更新
        state = MoveApplier.Apply(state, move);

        // --- View更新 ---
        var view = views[move.FromX, move.FromY];

        // 取る処理
        if (views[move.ToX, move.ToY] != null)
        {
            Destroy(views[move.ToX, move.ToY].gameObject);
        }

        views[move.FromX, move.FromY] = null;

        view.SetPosition(move.ToX, move.ToY);
        views[move.ToX, move.ToY] = view;
    }
}