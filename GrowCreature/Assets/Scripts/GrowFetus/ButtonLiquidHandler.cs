using UnityEngine;
using UnityEngine.Serialization;

namespace GrowFetus
{
    public enum LiquidType
    {
        Cute,
        Love,
        Strength,
        Smart
    }

    public class ButtonLiquidHandler : MonoBehaviour
    {
        [FormerlySerializedAs("buttonLiquidType")] public LiquidType liquidType;
    }
}