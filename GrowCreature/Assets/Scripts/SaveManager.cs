using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

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
    public static SaveManager GetInstance() => new SaveManager();

    public static readonly string myProductFileName = "MyProductData";

    public static readonly List<Vector3> productPositionList = new List<Vector3>()
    {
        new Vector3(0, 2.75f, 0),
        new Vector3(-6f, 2.75f, -3f),
        new Vector3(-7.92f, 2.75f, 6.73f),
    };

    public void SaveFormula(string formula)
    {
        GameData gameData = LoadGameData();
        gameData.selectedFormula = formula;
        CustomSave.SaveData(gameData, myProductFileName);
    }

    public GameData LoadGameData() =>
        CustomSave.LoadData<GameData>(SaveManager.myProductFileName) ?? GameData.CreateInstance();
}