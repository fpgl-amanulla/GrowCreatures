using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = System.Random;

public class GridItems : MonoBehaviour
{
    public SpriteContainerSO iconContainerSO;

    public Item itemPrefab;
    public Image imageFade;
    public GameObject panelGameOver;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtWarning;
    public TextMeshProUGUI txtCountDown;
    public TextMeshProUGUI txtGameRule;

    public List<Item> allItem = new List<Item>();
    private List<ItemData> allItemData = new List<ItemData>();
    private int itemLength = 9;

    public int totalClick = 0;
    public int correctClicked = 0;

    private void Start()
    {
        Random rnd = new Random();
        iconContainerSO.allSprite = iconContainerSO.allSprite.OrderBy(item => rnd.Next()).ToList();

        for (int i = 0; i < itemLength + 10; i++)
        {
            ItemData itemData = new ItemData(i, iconContainerSO.allSprite[i]);
            allItemData.Add(itemData);
        }

        //txtWarning.rectTransform.DOScale(Vector3.one * .8f, .8f).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(PopulateItem());
    }

    private IEnumerator PopulateItem()
    {
        for (int i = 0; i < itemLength; i++)
        {
            Item item = Instantiate(itemPrefab, this.transform);
            item.Init(allItemData[i], this);
            allItem.Add(item);
            yield return new WaitForSeconds(.05f);
        }

        for (int i = 10; i >= 0; i--)
        {
            txtCountDown.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }

        txtCountDown.gameObject.SetActive(false);
        txtWarning.gameObject.SetActive(false);
        RearrangeItem();
    }

    private void RearrangeItem()
    {
        imageFade.DOFade(1, 1.0f).OnComplete(delegate
        {
            Random rnd = new Random();
            //iconContainerSO.allSprite = iconContainerSO.allSprite.OrderBy(item => rnd.Next()).ToList();
            List<int> randListForNewItem = GetRandUniqueList(9, 19, 4);
            List<int> randListForReplaceItem = GetRandUniqueList(0, 9, 4);
            for (int i = 0; i < randListForReplaceItem.Count; i++)
            {
                allItem[randListForReplaceItem[i]].imgIcon.sprite = iconContainerSO.allSprite[randListForNewItem[i]];
                allItem[randListForReplaceItem[i]].id = -1;
            }

            txtGameRule.gameObject.SetActive(true);
            imageFade.DOFade(0, 1.0f).OnComplete(delegate { imageFade.gameObject.SetActive(false); });
        });
    }

    public void GameOver()
    {
        panelGameOver.SetActive(true);
        txtGameRule.gameObject.SetActive(false);
        txtScore.text = "Score : " + correctClicked + "/" + totalClick;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private static List<int> GetRandUniqueList(int min, int max, int length)
    {
        List<int> numbersList = new List<int>();

        while (numbersList.Count < length)
        {
            while (true)
            {
                int random = new Random().Next(min, max);
                if (numbersList.Contains(random)) continue;
                numbersList.Add(random);
                break;
            }
        }

        return numbersList;
    }
}