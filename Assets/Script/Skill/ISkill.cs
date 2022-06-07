interface ISkill
{
    SkillDef SkillId { get; }
    void Setup();
    void SkillUpdate();
    void Levelup();
}

public enum SkillDef
{
    Invalid = 0,
    MeleeWeapon = 1,
    ShotBullet = 2,
    AreaAttack = 3,
}
