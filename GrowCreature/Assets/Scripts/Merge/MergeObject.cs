using UnityEngine;

namespace Merge
{
    public class MergeObject : DragToMove
    {
        public int MergeObjectState { get; set; }

        private void OnCollisionEnter(Collision collision)
        {
            if (!isDragging) return;
            if (!dragEnabled) return;
            MergeController mergeController = MergeController.Instance;
            MergeObject mergeObj = collision.gameObject.GetComponent<MergeObject>();

            bool isMergePossible = mergeController.IsMergePossible(this, mergeObj);
            if (!isMergePossible) return;

            mergeController.MergeObject(this);

            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}