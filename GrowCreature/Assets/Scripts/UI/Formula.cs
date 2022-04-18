using System.Collections.Generic;
using Core;

namespace UI
{
    public static class Formula
    {
        public static readonly List<string> FormulaList = new List<string>()
        {
            "1,2,3;2801",
            "1,4,2;2802",
            "2,4,3,1;2803",
            "4,1,3;2804",
            "2,1,3;2805",
            "3,2,1,4;2037"
        };

        public static string GetNextFormula()
        {
            List<string> myFormulaList = SaveManager.GetInstance().LoadGameData().myFormulaList;
            return myFormulaList.Count >= FormulaList.Count ? null : FormulaList[myFormulaList.Count];
        }

        public static List<string> GetMyFormulaList() => SaveManager.GetInstance().LoadGameData().myFormulaList;

        public static bool IsMyFormula(string formula)
        {
            GameData gameData = SaveManager.GetInstance().LoadGameData();
            for (int i = 0; i < gameData.myFormulaList.Count; i++)
            {
                if (gameData.myFormulaList[i] == formula)
                    return true;
            }

            return false;
        }
    }
}