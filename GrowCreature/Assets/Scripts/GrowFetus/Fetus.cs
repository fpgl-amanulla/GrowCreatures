using Merge;
using UnityEngine;

namespace GrowFetus
{
    public class Fetus : DragToMove
    {
        public int FetusState { get; set; }

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
}