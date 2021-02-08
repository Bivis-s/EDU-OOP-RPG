using System;
using System.Collections.Generic;
using System.Text;
using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

public enum States
{
    Normal,
    Weakened,
    Ill,
    Poisoned,
    Paralyzed,
    Invulnerable,
    Dead
}

public enum Races
{
    Human,
    Gnome,
    Elf,
    Orc,
    Goblin
}

public enum Genders
{
    Male,
    Female
}

public class Character : IComparable
{
    private static readonly HashSet<int> globalIdSet = new HashSet<int>();
    private readonly List<AbstractArtifact> artifactList = new List<AbstractArtifact>();
    private int age;
    private int currentHealth;
    private int experience;

    private int maxHealth;

    protected Character()
    {
    }

    public Character(int id, string name, Races race, Genders gender)
    {
        if (!globalIdSet.Contains(id))
        {
            Id = id;
            globalIdSet.Add(id);
        }
        else
        {
            throw new ArgumentException("Идентификатор персонажа не уникален!");
        }

        Name = name;
        Race = race;
        Gender = gender;
    }

    public int CompareTo(object obj)
    {
        if (obj is Character)
            return Experience.CompareTo(((Character) obj).Experience);
        throw new ArgumentException("Попытка сравнить разнородные объекты");
    }

    public void RemoveCharacterFromGlobalIdSet()
    {
        globalIdSet.Remove(Id);
    }

    private double GetHealthDegree(int health)
    {
        if (MaxHealth != 0)
            return (double) health / MaxHealth;
        return 0;
    }

    private void SetStateByHealth(int health)
    {
        var healthDegree = GetHealthDegree(health);
        if (healthDegree >= 0.1)
            State = States.Normal;
        else if (healthDegree == 0)
            State = States.Dead;
        else
            State = States.Weakened;
    }

    public int HealthDifference()
    {
        return MaxHealth - CurrentHealth;
    }

    public bool IsInventoryContainsArtifact(AbstractArtifact artifact)
    {
        return artifactList.Contains(artifact);
    }

    public void AddArtifactToInventory(AbstractArtifact artifact)
    {
        artifactList.Add(artifact);
    }

    public void RemoveArtifactFromInventory(AbstractArtifact artifact)
    {
        if (IsInventoryContainsArtifact(artifact))
            artifactList.Remove(artifact);
        else
            throw new RpgException("Персонаж не имеет этого артефакта!");
    }

    public void GiveArtifactTo(AbstractArtifact artifact, Character character)
    {
        RemoveArtifactFromInventory(artifact);
        character.AddArtifactToInventory(artifact);
    }

    public List<AbstractArtifact> GetArtifactInventory()
    {
        if (artifactList.Capacity != 0)
            return artifactList;
        throw new RpgException("Персонаж не имеет ни одного предмета в инвентаре!");
    }

    private string GetArtifactsInInventoryToPrint()
    {
        try
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("Inventory=");
            var learnedSpells = GetArtifactInventory();
            for (var i = 0; i < learnedSpells.Count; i++)
            {
                var classPath = learnedSpells[i].ToString().Split('.');
                stringBuilder.Append(classPath[classPath.Length - 1]);
                if (i != learnedSpells.Count - 1) stringBuilder.Append(", ");
            }

            return stringBuilder.ToString();
        }
        catch (RpgException)
        {
            return "Инвентарь пуст";
        }
    }

    public override string ToString()
    {
        return GetType().Name +
               " id=" + Id +
               " name='" + Name + '\'' +
               " state='" + State + '\'' +
               " canSpeak=" + CanSpeak +
               " canMove=" + CanMove +
               " race='" + Race + '\'' +
               " gender='" + Gender + '\'' +
               " age=" + Age +
               " currentHealth=" + CurrentHealth +
               " maxHealth=" + MaxHealth +
               " experience=" + Experience + " " +
               GetArtifactsInInventoryToPrint();
    }

    #region getters and setters

    public int Id { get; set; }

    public string Name { get; set; }

    public States State { get; set; }

    public bool CanSpeak { get; set; }

    public bool CanMove { get; set; }

    public Races Race { get; set; }

    public Genders Gender { get; set; }

    public int Age
    {
        get => age;
        set
        {
            if (value >= age)
                age = value;
            else
                throw new ArgumentException("Возраст не может быть ниже текущего");
        }
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value > MaxHealth) throw new ArgumentException("Текущее здоровье не может стать больше максимального");

            if (value >= 0)
            {
                SetStateByHealth(value);
                currentHealth = value;
            }
            else
            {
                throw new ArgumentException("Здоровье не может стать отрицательным");
            }
        }
    }

    public int MaxHealth
    {
        get => maxHealth;
        set
        {
            if (value > 0)
                maxHealth = value;
            else
                throw new ArgumentException("Максимальное здоровье не может стать отрицательным или равным нулю");
        }
    }

    public int Experience
    {
        get => experience;
        set
        {
            if (value >= 0)
                experience = value;
            else
                throw new ArgumentException("Опыт не может стать отрицательным");
        }
    }

    #endregion

    #region UseArtifact

    public void UseArtifact(ITargetSpell spell, Character character)
    {
        if (State != States.Dead)
        {
            var abstractArtifact = (AbstractArtifact) spell;
            if (IsInventoryContainsArtifact(abstractArtifact))
                spell.Cast(character);
            else
                throw new RpgException("Персонаж не имеет этот предмет в инвентаре");
        }
        else
        {
            throw new RpgException("Персонаж мёртв!");
        }
    }

    public void UseArtifact(IGradeTargetSpell spell, Character character, int grade)
    {
        if (State != States.Dead)
        {
            var abstractArtifact = (AbstractArtifact) spell;
            if (IsInventoryContainsArtifact(abstractArtifact))
                spell.Cast(character, grade);
            else
                throw new RpgException("Персонаж не имеет этот предмет в инвентаре");
        }
        else
        {
            throw new RpgException("Персонаж мёртв!");
        }
    }

    public void UseArtifact(IGradeSpell spell, int grade)
    {
        if (State != States.Dead)
        {
            var abstractArtifact = (AbstractArtifact) spell;
            if (IsInventoryContainsArtifact(abstractArtifact))
                spell.Cast(grade);
            else
                throw new RpgException("Персонаж не имеет этот предмет в инвентаре");
        }
        else
        {
            throw new RpgException("Персонаж мёртв!");
        }
    }

    public void UseArtifact(ISelfSpell spell)
    {
        if (State != States.Dead)
        {
            var abstractArtifact = (AbstractArtifact) spell;
            if (IsInventoryContainsArtifact(abstractArtifact))
                spell.Cast();
            else
                throw new RpgException("Персонаж не имеет этот предмет в инвентаре");
        }
        else
        {
            throw new RpgException("Персонаж мёртв!");
        }
    }

    #endregion
}