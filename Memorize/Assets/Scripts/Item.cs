using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [HideInInspector] public int id;
    public Image imgIcon;
    public Image imgIconBg;
    public Button btnItem;

    private GridItems _gridItems;

    private void Start()
    {
        btnItem.onClick.AddListener(ItemClickCallBack);
    }

    private void ItemClickCallBack()
    {
        _gridItems.totalClick++;
        switch (id)
        {
            case -1:
                imgIconBg.color = Color.red;
                break;
            default:
                _gridItems.correctClicked++;
                imgIconBg.color = Color.green;
                break;
        }

        CheckForGameOver();
    }

    private void CheckForGameOver()
    {
        if (_gridItems.totalClick > 4)
        {
            _gridItems.GameOver();
            Debug.Log("GameOver");
        }
    }

    public void Init(ItemData itemData, GridItems gridItems)
    {
        _gridItems = gridItems;
        id = itemData.id;
        imgIcon.sprite = itemData.imgIconSprite;
    }
}