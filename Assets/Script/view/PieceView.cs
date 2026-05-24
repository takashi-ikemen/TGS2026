using System.Collections;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    /* -----
    Pieceを表示・見た目を反映させるスクリプト
    -----*/


    public int x, y;

    Renderer rend;
    [SerializeField] Renderer renderObj;

    //Color defaultColor;

    Color baseColor;

    public enum HighLightType //ハイライトの区別をするHighLightType
    {
        None,
        Hover,
        Selected
    }

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        baseColor = rend.material.color;
    }



    public void SetPositionImmediate(int x, int y) //初期配置
    {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(x, 0.65f, y);
    }

    public void MoveTo(int x, int y, float duration = 0.3f)  //移動
    {
        this.x = x;
        this.y = y;
        StartCoroutine(MoveCoroutine(new Vector3(x, 0, y), duration));
    }

    IEnumerator MoveCoroutine(Vector3 target, float duration)  //移動するさいの動き
    {
        Vector3 start = transform.position;
        float time = 0f;
        while(time < duration)
        {
            time  += Time.deltaTime;
            float t = time / duration;

            //なめらか補正(Ease)
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target;

    }

    public void SetHighLight(HighLightType type)  //ハイライトをかえる
    {
        switch(type)
        {
            case HighLightType.Hover:
                rend.material.color = Color.yellow;
                break;

            case HighLightType.Selected:
                rend.material.color = Color.blue;
                break;

            default:
                rend.material.color = baseColor;
                break;

        }
    }


}