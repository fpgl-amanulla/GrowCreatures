using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [HideInInspector] public int id;
    public Image imgIcon;

    public void Init(ItemData itemData)
    {
        id = itemData.id;
        imgIcon.sprite = itemData.imgIconSprite;

    }
}
