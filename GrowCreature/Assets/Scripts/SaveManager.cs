using System;
using System.Collections.Generic;
using Core;
using UI;
using UnityEngine;

[Serializable]
public class MyProductData
{
    public MyProductData(string id, int positionIndex)
    {
        this.id = id;
        this.positionIndex = positionIndex;
    }

    public string id;
    public int positionIndex;
}

public class SaveManager
{
    private static readonly SaveManager Instance = null;
    public static SaveManager GetInstance() => Instance ?? new SaveManager();

    public static readonly string myProductFileName = "GameData";

    public static readonly List<Vector3> productPositionList = new List<Vector3>()
    {
        new Vector3(0, 2.75f, 0),
        new Vector3(-6f, 2.75f, -3f),
        new Vector3(-7.92f, 2.75f, 6.73f),
        new Vector3(-3, 2.75f, 5.35f),
        new Vector3(3, 2.75f, 5.5f),
        new Vector3(4, 2.75f, -0.65f),
        new Vector3(0, 2.75f, -4.65f)
    };

    public void SaveFormula(string formula)
    {
        GameData gameData = LoadGameData();
        gameData.selectedFormula = formula;
        CustomSave.SaveData(gameData, myProductFileName);
    }

    public string GetSaveFormula() => LoadGameData().selectedFormula.Split(';')[0];
    public string GetSaveFormulaProductId() => LoadGameData().selectedFormula.Split(';')[1];

    private GameData LoadGameData()
    {
        GameData gameData = CustomSave.LoadData<GameData>(SaveManager.myProductFileName) ?? GameData.CreateInstance();
        if (gameData.selectedFormula == null)
        {
            // Default formula
            gameData.selectedFormula = Formula.FormulaList[0];
            CustomSave.SaveData(gameData, myProductFileName);
        }

        return gameData;
    }
}