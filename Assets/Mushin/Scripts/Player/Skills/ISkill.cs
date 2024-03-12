using Mushin.Scripts.Player;

public interface ISkill
{
    public SkillData SkillData { get; }
    public Player Player { get; }
    public void Activate();
}