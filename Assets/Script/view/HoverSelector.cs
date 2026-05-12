/*using UnityEngine;

public class HoverSelector : MonoBehaviour
{
    public Camera cam;
    public LayerMask pieceLayer;

    PieceView currentHover;

    private void Update()
    {
        UpdateHover();
    }

    void UpdateHover()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray , out RaycastHit hit , 100f, pieceLayer))
        {
            var piece = hit.collider.GetComponent<PieceView>();

            //選択している駒をハイライトで表示
            if(piece != null)
            {
                //前のハイライトを戻す
                if(currentHover != null && currentHover != piece)
                {
                    currentHover.SetHighLight(false);
                }

                currentHover = piece;
                currentHover.SetHighLight(true);

                return;
            }
        }

        //何も当たっていないとき
        if(currentHover != null)
        {
            currentHover.SetHighLight(false);
            currentHover = null;
        }

       
    }
}
*/