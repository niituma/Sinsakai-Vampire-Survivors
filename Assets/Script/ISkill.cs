interface ISkill
{
    SkillDef SkillId { get; }
    void Setup();
    void Update();
    void Levelup();
}

public enum SkillDef
{
    Invalid = 0,
    ShotBullet = 1,
    AreaAttack = 2,
}
