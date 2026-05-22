/*using UnityEngine;
using UnityEngine.UI;

public class HpManager : MonoBehaviour
{
    [SerializeField, Header("HPアイコン")]


    private Image[] hearts;
    private int Hp;
    private int MaxHp = 3;
    private int MinHp = 0;
    void Start()
    {
        InitializerPrefabHp();

    }

    private void Update()
    {
        GetHp();
        Damage();
    }


    private void CreateHpIcon()
    {
        for ( Hp = 3; Hp < 3; Hp++)
        {
            GameObject playerHpObj = Instantiate(playerIcon);

        }
    }


    public void GetHp()  //1キーを押したらHP増える
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(Hp != MaxHp)
            {
                Hp++;
                
                Debug.Log("HPが増えました");
            }
            else
            {
                Debug.Log("HPがマックスです");
            }
        }
    }

    public void Damage() //2キーを押したらHp減る
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (currentHp != MinHp)
            {
                currentHp--;
                Debug.Log("HPが減りました");
            }else
            {
                Debug.Log("ライフはもうゼロよ！");
            }
        }
    }

    public void InitializerPrefabHp()   //初期のHP設定
    {
        for(int i = 0;i <= MaxHp; i++)
        {
            var obj = Instantiate(HpOjb, hpContainer);

            hearts[i] = obj.GetComponent<Image>();
            currentHp++;

        }

        Debug.Log("現在のcurrentHpは" + currentHp);
    }
}
*/