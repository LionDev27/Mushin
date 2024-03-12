using Mushin.Scripts.Player;
using UnityEngine;

public class BaseGrenade : IGrenadeSkill
{
    public SkillData SkillData { get; }
    public Player Player { get; }
    public GameObject GrenadePrefab { get; set; }

    public BaseGrenade(GameObject grenadePrefab, SkillData skillData,Player player)
    {
        GrenadePrefab = grenadePrefab;
        SkillData = skillData;
        Player = player;
    }

    public void Activate()
    {
        Object.Instantiate(GrenadePrefab,Player.transform.position,Quaternion.identity);
    }
}