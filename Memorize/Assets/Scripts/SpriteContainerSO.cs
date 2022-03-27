using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpriteContainer", menuName = "Container/SpriteContainer", order = 0)]
public class SpriteContainerSO : ScriptableObject
{
    public List<Sprite> allSprite;

    public Sprite GetObject(int id)
    {
        Sprite obj = null;
        foreach (Sprite item in allSprite)
        {
            if (!item.name.Contains(id.ToString())) continue;
            obj = item;
            break;
        }
        return obj == null ? null : obj;
    }
}