using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalHPView : MonoBehaviour
{
    [SerializeField] private GameObject crystalPrefab;

    [SerializeField] private Sprite fullCrystal;
    [SerializeField] private Sprite emptyCrystal;

    private List<Image> crystals = new();

    //最大HP分のクリスタルを生成

    public void Initialize(int maxHP)
    {

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        crystals.Clear();
    
        for(int i=0; i<maxHP; i++)
        {
            GameObject crystalObj = Instantiate(crystalPrefab, transform);

            Image image = crystalObj.GetComponent<Image>();

            image.sprite = fullCrystal;

            crystals.Add(image);
        }
    }

    //HP表示更新
    public void UpdateHP(int currentHP)
    {
        for(int i=0;i<crystals.Count; i++)
        {
            crystals[i].sprite = i < currentHP ? fullCrystal : emptyCrystal;
        }
    }
}
