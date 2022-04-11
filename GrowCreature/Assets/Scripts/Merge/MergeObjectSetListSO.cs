using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Merge
{
    [CreateAssetMenu(fileName = "MergeObjectSetList", menuName = "MergeObject/Create MergeObjectSetList", order = 0)]
    public class MergeObjectSetListSO : ScriptableObject
    {
        public List<MergeObjectSetSO> AllMergeObjectSO = new List<MergeObjectSetSO>();

        public MergeObjectSetSO GetRandomMergeObjectSO() => AllMergeObjectSO[Random.Range(0, AllMergeObjectSO.Count)];

        public MergeObjectSetSO GetMergeObjectSetListSo(string productId)
        {
            foreach (MergeObjectSetSO item in AllMergeObjectSO.Where(item => item.productId == productId))
                return item;
            return AllMergeObjectSO[0];
        }
    }
}