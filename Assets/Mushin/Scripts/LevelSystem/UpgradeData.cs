using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Data/Upgrade")]
public class UpgradeData : ScriptableObject
{
    [Header("Upgrade")]
    public Stats stat;
    public float value;
    
    [Header("Visuals")]
    public Sprite sprite;
    public string title;
    [TextArea(2, 4)] public string description;
}