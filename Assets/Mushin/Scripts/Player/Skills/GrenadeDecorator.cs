using Mushin.Scripts.Player;
using UnityEngine;

public abstract class GrenadeDecorator : IGrenadeSkill
{
    private readonly IGrenadeSkill _skillToDecorate;
    public SkillData SkillData => _skillToDecorate.SkillData;
    public Player Player => _skillToDecorate.Player;
    public GameObject GrenadePrefab
    {
        get => _skillToDecorate.GrenadePrefab;
        set => _skillToDecorate.GrenadePrefab = value;
    }

    protected GrenadeDecorator(IGrenadeSkill skillToDecorate)
    {
        _skillToDecorate = skillToDecorate;
    }
    public virtual void Activate()
    {
        _skillToDecorate.Activate();
    }

}