using UnityEngine;
public class SkillSlot
{
    public ISkill CurrentSkill;
    private float _cooldown;
    public float CooldownTimer;
    public Sprite UiSprite;

    public bool IsSkillReady => CooldownTimer <= 0;
    
    public void Configure(ISkill skill)
    {
        CurrentSkill = skill;
        _cooldown = skill.SkillData.cooldown;
        UiSprite = skill.SkillData.uiSprite;
        CooldownTimer = 0;
    }
    public void ResetCooldown()
    {
        CooldownTimer = _cooldown;
    }
}