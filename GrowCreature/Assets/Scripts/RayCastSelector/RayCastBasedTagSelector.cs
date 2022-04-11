using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RayCastBasedTagSelector : MonoBehaviour, ISelector
{
    [SerializeField] private List<string> selectableTag = new List<string>();

    private Transform _selection;

    public void Check(Ray ray)
    {
        _selection = null;

        if (!Physics.Raycast(ray, out var hit)) return;

        Transform selection = hit.transform;
        foreach (string Item in selectableTag.Where(Item => selection.CompareTag(Item)))
        {
            _selection = selection;
            break;
        }
    }

    public Transform GetSelection()
    {
        return _selection;
    }
}