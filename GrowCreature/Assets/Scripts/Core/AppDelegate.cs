using Merge;
using UnityEngine;

namespace Core
{
    public class AppDelegate
    {
        private static AppDelegate instance = null;

        public static AppDelegate GetInstance()
        {
            instance ??= new AppDelegate();
            return instance;
        }

        public MergeObjectSetSO selectedMergeObjectSo { get; set; }
    }
}