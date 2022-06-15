using System;
using System.Collections.Generic;

public enum SelectType
{
    Skill = 1,
    Passive = 2,
    Execute = 3,
}

[Serializable]
public class SkillSelectTable
{
    public SelectType Type;
    public int TargetId;
    public string Name;
    public int Level;
    public int Probability;
}

public class GameData
{
    static public List<SkillSelectTable> SkillSelectTable = new List<SkillSelectTable>()
    {
        new SkillSelectTable(){ Type = SelectType.Skill, TargetId = 1, Name = "ムチ", Level = 0, Probability = 10 },
        new SkillSelectTable(){ Type = SelectType.Skill, TargetId = 2, Name = "魔法の杖", Level = 0, Probability = 5 },
        new SkillSelectTable(){ Type = SelectType.Skill, TargetId = 3, Name = "結界", Level = 0, Probability = 50 },
        new SkillSelectTable(){ Type = SelectType.Skill, TargetId = 4, Name = "スパイク", Level = 0, Probability = 10 },
        new SkillSelectTable(){ Type = SelectType.Passive, TargetId = 1, Name = "最大体力UP", Level = 0, Probability = 40 },
        new SkillSelectTable(){ Type = SelectType.Passive, TargetId = 2, Name = "速度UP", Level = 0, Probability = 50 },
        new SkillSelectTable(){ Type = SelectType.Passive, TargetId = 3, Name = "経験値UP", Level = 0, Probability = 50 },
        new SkillSelectTable(){ Type = SelectType.Execute, TargetId = 1, Name = "回復", Level = 0, Probability = 30 }
    };

    static public List<int> LevelTable = new List<int>()
    {
        0, //level 1
        5, //level 1→2
        25,//...
        60,
        120,
        200,
        300,
        400,
        500,
        600,
        700,
        800,
    };
}