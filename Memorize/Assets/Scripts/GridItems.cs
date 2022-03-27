using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Random = System.Random;

public class GridItems : MonoBehaviour
{
    public SpriteContainerSO iconContainerSO;

    public Item itemPrefab;

    public List<Item> allItem = new List<Item>();
    private List<ItemData> allItemData = new List<ItemData>();
    private int itemLength = 9;

    private void Start()
    {
        Random rnd = new Random();
        iconContainerSO.allSprite = iconContainerSO.allSprite.OrderBy(item => rnd.Next()).ToList();

        for (int i = 0; i < itemLength; i++)
        {
            ItemData itemData = new ItemData(i, iconContainerSO.allSprite[i]);
            allItemData.Add(itemData);
        }

        StartCoroutine(PopulateItem());
    }

    private IEnumerator PopulateItem()
    {
        for (int i = 0; i < itemLength; i++)
        {
            Item item = Instantiate(itemPrefab, this.transform);
            item.Init(allItemData[i]);
            allItem.Add(item);
            yield return new WaitForSeconds(.05f);
        }

        yield return new WaitForSeconds(3.0f);
        StartCoroutine(RearrangeItem());
    }

    public IEnumerator RearrangeItem()
    {
        Random rnd = new Random();
        iconContainerSO.allSprite = iconContainerSO.allSprite.OrderBy(item => rnd.Next()).ToList();
        for (int i = 0; i < allItem.Count; i++)
        {
            allItem[i].imgIcon.sprite = iconContainerSO.allSprite[i];
            yield return new WaitForSeconds(.05f);
        }
    }
}