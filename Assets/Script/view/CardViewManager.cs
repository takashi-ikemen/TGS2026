using System.Collections.Generic;
using UnityEngine;

public class CardViewManager : MonoBehaviour
{
    //[SerializeField] private Transform handRoot;

    //カードのPrefab
    [SerializeField] private GameObject RandomHPCard;
    [SerializeField] private GameObject ShieldCard;
    [SerializeField] private GameObject SwitchPieceCard;
    //カードを生成する親
    [SerializeField] Transform handRoot;

    List<CardView> cardViews = new();

    public void GenerateCard(List<Card> cards)
    {
        //カードを初期化
        ClearCards();

        if (cards.Count <= 0) return;
        Debug.Log("カードを生成");
        foreach (Card item in cards)
        {
            AddCard(item);
        }

        //並び替え
        ArrangeCards();
    }
    public void AddCard(Card card)
    {
        //prefabのnullチェック
        var prefab = GetPrefab(card.name);

        if(prefab == null)
        {
            Debug.Log($"Prefabがありません : {card.name}");
            return;
        }

        //カードの生成
        GameObject cardObj = Instantiate(prefab,handRoot);
        CardView view = cardObj.GetComponent<CardView>();

        //★★★★★怪しい★★★
        //初期設定  
        view.Initialize(card);

        //Viewの配列に追加
        cardViews.Add(view);
    }

    public void ClearCards()
    {
        //カードを一度クリアするメソッド
        foreach(var card in cardViews)
        {
            Destroy(card.gameObject);
        }

        cardViews.Clear();
    }


    void ArrangeCards()//並び変えるスクリプト
    {
        float spacing = 0.3f;

        for(int i=0;i<cardViews.Count; i++)
        {
            Vector3 pos = new Vector3(i * spacing, 0, 0);
            cardViews[i].transform.localPosition = pos;

        }
    }

    GameObject GetPrefab(string name)
    {
        Debug.Log(name);

        switch (name)
        {
            case "RandomHPCard":
                return RandomHPCard;
            case "ShieldCard":
                return ShieldCard;
            case "SwitchPieceCard":
                return SwitchPieceCard;
            default:
                return null;
                

        }
    }

    public void Invisible()
    {
        foreach(var item in cardViews)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void Visible()
    {
        foreach(var item in cardViews)
        {
            item.gameObject.SetActive(true);
        }
    }
}


