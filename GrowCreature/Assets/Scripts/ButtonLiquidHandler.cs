using System;
using UnityEngine;
using UnityEngine.Serialization;

public enum LiquidType
{
    Cute,
    Love
}

public class ButtonLiquidHandler : MonoBehaviour
{
    [FormerlySerializedAs("buttonLiquidType")] public LiquidType liquidType;
}