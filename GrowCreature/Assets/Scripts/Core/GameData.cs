using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class GameData
    {
        public List<MyProductData> myProductDataList = new List<MyProductData>();

        public List<string> myFormulaList = new List<string>();

        public string selectedFormula;

        public static GameData CreateInstance()
        {
            return new GameData();
        }
    }
}