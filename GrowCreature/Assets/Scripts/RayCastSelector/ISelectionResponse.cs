using UnityEngine;

public interface ISelectionResponse
{
    public void OnSelect(Transform selection);
    public void OnDeselect(Transform selection);
}