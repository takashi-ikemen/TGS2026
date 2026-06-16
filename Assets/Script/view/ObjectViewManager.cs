using UnityEngine;

public class ObjectViewManager : MonoBehaviour
{
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private GameObject grailPrefab;

    private GrailView mineView;
    private GrailView grailView;

    private bool isMineView;
    private bool isGrailView;

    public bool isMineExist;

    public void Initialize(GameState state)
    {
        Spawn(state);
    }

    public void UpdateObjects(GameState state)
    {
        if (state.TouchObject)
        {
            if (state.MineExploded)
            {
                Debug.Log("ínóãîöî≠");
                isMineExist = false;
                Destroy(mineView.gameObject);
            }
            else if (state.GrailTake)
            {
                Debug.Log("êπîtälìæ");
                Destroy(grailView.gameObject);
                Spawn(state);
            }
        }

    }

    void Spawn(GameState state)
    {
        isMineView = Random.Range(0, 2) == 0;

        if (isMineView)
        {
            isGrailView = false;
        }
        else
        {
            isGrailView = true;
        }


        if (mineView != null)
            Destroy(mineView.gameObject);

        if (grailView != null)
            Destroy(grailView.gameObject);

        mineView =
            Instantiate(minePrefab)
            .GetComponent<GrailView>();

        grailView =
            Instantiate(grailPrefab)
            .GetComponent<GrailView>();

        isMineExist = true;

        mineView.isView = isMineView;
        grailView.isView = isGrailView;

        mineView.SetPositionImmdiate(
            state.Mine.GetMineX(),
            state.Mine.GetMineY());


        grailView.SetPositionImmdiate(
            state.Grail.GetMineX(),
            state.Grail.GetMineY());
    }
}