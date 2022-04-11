using System;
using Core;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class PlayerInfo
{
    public PlayerInfo(string id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public string id;
    public string name;
}

public class QuickSaveTest : MonoBehaviour
{
    private void Start()
    {
        const string fileName = "PlayerData";

        GameData gameData = GameData.CreateInstance();

        CustomSave.SaveData(gameData, fileName);
    }
}