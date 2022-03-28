using UnityEngine;
using UnityEngine.Serialization;

public class Fetus : DragToMove
{
    private int _fetusState;

    public int FetusState
    {
        get => _fetusState;
        set => _fetusState = value;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDragging) return;
        MergeController mergeController = MergeController.Instance;
        Fetus fetus2 = collision.gameObject.GetComponent<Fetus>();
        
        bool isMergePossible = mergeController.IsMergePossible(this, fetus2);
        if(!isMergePossible) return;
        
        mergeController.MergeObject(this);
        
        Destroy(this.gameObject);
        Destroy(collision.gameObject);
    }
}