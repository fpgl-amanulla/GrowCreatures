using UnityEngine;

public class ItemData
{
    public int id;
    public readonly Sprite imgIconSprite;

    public ItemData(int id, Sprite imgIconSprite)
    {
        this.id = id;
        this.imgIconSprite = imgIconSprite;
    }
}