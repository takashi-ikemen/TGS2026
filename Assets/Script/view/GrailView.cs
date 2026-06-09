using System.Collections;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class GrailView : MonoBehaviour
{
    /*-----
    Grailを表示・見た目を反映させるスクリプト
    -----*/

    public int x, y;

    private Renderer rend;

    //見えるか見えないかの情報
    public bool isView;

    //public float transparency;

    Color baseColor;

    private GameObject child;


    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();

        child = transform.Find("ViewObject").gameObject;

        if (rend == null)
        {
            Debug.Log("Rendererが見つかりません");
            return;
        }

    }

    public void SetPositionImmdiate(int x, int y)
    {
        //座標のところに移動
        this.x = x;
        this.y = y;
        transform.position = new Vector3(x, 0.5f, y);

        //透明化
        //ApplyTransparency();
        if (!isView)
        {
            child.SetActive(false);
        }


    }

}