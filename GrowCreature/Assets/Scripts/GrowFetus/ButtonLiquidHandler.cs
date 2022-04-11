using UnityEngine;
using UnityEngine.Serialization;

namespace GrowFetus
{
    public enum LiquidType
    {
        Cute = 1,
        Love = 2,
        Strength = 3,
        Smart = 4
    }

    public class ButtonLiquidHandler : MonoBehaviour
    {
        [FormerlySerializedAs("buttonLiquidType")] public LiquidType liquidType;
    }
}