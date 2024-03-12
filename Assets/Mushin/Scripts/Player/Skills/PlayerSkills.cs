using Mushin.Scripts.Player;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    private Player _player;
    private SkillSlot[] skillSlots;

    //Testeo
    [SerializeField] private GameObject baseGrenadePrefab;
    [SerializeField] private GameObject fireGrenadePrefab;
    [SerializeField] private SkillData grenadeSkillData;

    public void Configure(Player player)
    {
        _player = player;
    }
    private void Start()
    {
        InitializeSkillSlots();
        SetSkill(0, new BaseGrenade(baseGrenadePrefab, grenadeSkillData,_player));
    }

    private void InitializeSkillSlots()
    {
        skillSlots = new SkillSlot[2];
        for (int i = 0; i < skillSlots.Length; i++)
        {
            skillSlots[i] = new SkillSlot();
        }
    }

    private void Update()
    {
        UpdateSkillsCooldown();

        if (Input.GetKeyDown(KeyCode.U))
        {
            SetSkill(0, new FireGrenadeModifier((IGrenadeSkill)GetSkill(0), fireGrenadePrefab));
        }
    }

    private void UpdateSkillsCooldown()
    {
        foreach (var skillSlot in skillSlots)
        {
            skillSlot.CooldownTimer -= Time.deltaTime;
        }
    }

    public void SetSkill(int slotIndex, ISkill newSkill)
    {
        if (IsSkillIndexOutOfBounds(slotIndex)) return;
        skillSlots[slotIndex].Configure(newSkill);
    }

    public ISkill GetSkill(int slotIndex)
    {
        return skillSlots[slotIndex].CurrentSkill;
    }

    public void TrySkill(int slotIndex)
    {
        if (IsSkillIndexOutOfBounds(slotIndex)||skillSlots[slotIndex].CurrentSkill==null) return;

        var currentSlot = skillSlots[slotIndex];
        if (currentSlot == null || !currentSlot.IsSkillReady) return;

        currentSlot.CurrentSkill.Activate();
        currentSlot.ResetCooldown();
    }

    private bool IsSkillIndexOutOfBounds(int skillIndex)
    {
        return skillIndex < 0 || skillIndex >= skillSlots.Length;
    }
}