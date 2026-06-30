using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class HPViews : MonoBehaviour
{
    [SerializeField] private GameObject crystalPrefab;
    [SerializeField] private Transform hpRoot;
    [SerializeField] private float spacing;
    [SerializeField] private bool isWhite;
    [SerializeField] private int maxHp = 6;

    private readonly List<GameObject> crystals = new();

    private void Awake()
    {
        for (int i = 0; i < maxHp; i++)
        {
            GameObject crystal = Instantiate(crystalPrefab, hpRoot);

            crystals.Add(crystal);

            float z = i * spacing;

            if (isWhite) z *= -1;

            crystal.transform.localPosition =
                new Vector3(0f, 0f, z);
        }
    }

    /// <summary>
    /// 表示だけ更新
    /// </summary>
    public void UpdateView(int hp)
    {
        for (int i = 0; i < crystals.Count; i++)
        {
            crystals[i].SetActive(i < hp);
        }
    }
}
    /*//最大HP分のクリスタルを生成
    public void SetHP(int hp)
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        crystals.Clear();

        while(crystals.Count < hp)
        {
            GameObject crystalObject = Instantiate(crystalPrefab, hpRoot);

            crystals.Add(crystalObject);
        }

        //表示・非表示を切り替え
        for(int i=0; i<crystals.Count; i++)
        {
            crystals[i].SetActive(i < hp);
        }

        ArrangeCrystals(crystals,isWhite);
    }

    private void ArrangeCrystals(List<GameObject> crystals, bool tmp_isWhtie)
    {
        int index = 0;

        foreach (var crystal in crystals)
        {
            if (!crystal.activeSelf)
                continue;

            float x = index * spacing;

            if (tmp_isWhtie)
            {
                x *= -1;
            }

            crystal.transform.localPosition =
                new Vector3(0f, 0f, x);

            index++;
        }
    }

}*/
