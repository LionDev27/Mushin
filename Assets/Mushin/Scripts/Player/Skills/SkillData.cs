using Mushin.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Skill Data")]
public class SkillData : ScriptableObject
{
    public float cooldown;
    public Sprite uiSprite;
}
