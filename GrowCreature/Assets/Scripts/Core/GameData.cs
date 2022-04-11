using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class GameData
    {
        public List<MyProductData> myProductDataList = new List<MyProductData>();

        public static GameData CreateInstance()
        {
            return new GameData();
        }
    }
}