using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    [FormerlySerializedAs("stat")] [Header("Upgrade")]
    public Upgrades upgrade;
    public float value;
    
    [Header("Visuals")]
    public Sprite sprite;
    public string title;
    [TextArea(2, 4)] public string description;
}