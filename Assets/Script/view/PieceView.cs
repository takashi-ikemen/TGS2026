using System.Collections;
using UnityEngine;

public class PieceView : MonoBehaviour
{
    public int x, y;

    Renderer rend;
    [SerializeField] Renderer renderObj;

    Color defaultColor;

    Color baseColor;

    public enum HighLightType
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



    public void SetPositionImmediate(int x, int y)
    {
        this.x = x;
        this.y = y;
        transform.position = new Vector3(x, 0.65f, y);
    }

    public void MoveTo(int x, int y, float duration = 0.3f)
    {
        this.x = x;
        this.y = y;
        StartCoroutine(MoveCoroutine(new Vector3(x, 0, y), duration));
    }

    IEnumerator MoveCoroutine(Vector3 target, float duration)
    {
        Vector3 start = transform.position;
        float time = 0f;
        while(time < duration)
        {
            time  += Time.deltaTime;
            float t = time / duration;

            //�Ȃ߂炩�␳(Ease)
            t = t * t * (3f - 2f * t);

            transform.position = Vector3.Lerp(start, target, t);
            yield return null;
        }

        transform.position = target;

    }

    public void SetHighLight(HighLightType type)
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