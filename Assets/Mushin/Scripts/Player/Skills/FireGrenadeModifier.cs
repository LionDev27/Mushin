using UnityEngine;

public class FireGrenadeModifier : GrenadeDecorator
{
    private readonly GameObject _fireGrenadePrefab;

    public FireGrenadeModifier(IGrenadeSkill skillToDecorate, GameObject fireGrenadePrefab) : base(skillToDecorate)
    {
        _fireGrenadePrefab = fireGrenadePrefab;
    }

    public override void Activate()
    {
        GrenadePrefab = _fireGrenadePrefab;
        base.Activate();
    }
}