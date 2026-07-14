using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask pieceLayer;
    [SerializeField] private LayerMask tileLayer;
    [SerializeField] private LayerMask cardLayer;
    [SerializeField] private LayerMask changeModeLayer;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private TileManager tileManager;
    [SerializeField] private PieceViewManager pieceViewManager;
    [SerializeField] private CardViewManager cardViewManager;

    //UI
    [SerializeField] private FadeController fadeController;

    [SerializeField] GameObject target;

    private List<Move> currentMoves;

    private PieceView currentHoverPiece;
    private PieceView selectedPiece;
    private Tile currentTile;
    private CardView currentCard;

    private bool selectingMove;
    private bool selectingUseCard;  //カードを使用するモードかどうか
    private bool isExecutedGenerateCard;

    void Update()
    {
        //Tick
        fadeController.Tick();


        ChangeUseCardMode();

        if (selectingUseCard)
        {
            //cardViewManager.Visible();

            UseCardMode();
        }
        else
        {
            //cardViewManager.Invisible();

            if (!selectingMove)
            {
                SelectPiece();
            }
            else
            {
                SelectDestination();
            }
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

    void ChangeUseCardMode()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out RaycastHit hit, 100f, changeModeLayer))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectingUseCard)
                {
                    Debug.Log("クリックできてる、true");
                    selectingUseCard = false;//カードモードのフラグをfalseにする
                    cardViewManager.ClearCards();
                    isExecutedGenerateCard = false;//カード生成のフラグをfalseにする
                    //cardViewManager.Invisible();
                }
                else
                {
                    Debug.Log("クリックできてる、false");
                    selectingUseCard = true;//カードモードのフラグをtrueにする
                    //cardViewManager.Visible();
                }
            }
            
        }
    }

    void UseCardMode()
    {
        if (!isExecutedGenerateCard)
        {
            gameManager.GenerateCard();
            isExecutedGenerateCard = true;
        }

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out RaycastHit hit, 100f, cardLayer))
        {
            currentCard = hit.collider.GetComponent<CardView>();
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("カードを使用！");
                gameManager.UseCard(currentCard);
            }
        }
       
    }

}