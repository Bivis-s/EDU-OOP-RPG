using System;
using System.Collections.Generic;
using System.Text;
using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

public enum states
{
    Normal,
    Weakened,
    Ill,
    Poisoned,
    Paralyzed,
    Invulnerable,
    Dead
}

public enum races
{
    Human,
    Gnome,
    Elf,
    Orc,
    Goblin
}

public enum genders
{
    Male,
    Female
}

public class Character : IComparable
{
    private List<AbstractArtifact> artifactList = new List<AbstractArtifact>();

    private int id;
    private string name;
    private states state;
    private bool canSpeak;
    private bool canMove;
    private races race;
    private genders gender;
    private int age;
    private int currentHealth;
    private int maxHealth;
    private int experience;

    private static HashSet<Int32> globalIdSet = new HashSet<int>();

    public void RemoveCharacterFromGlobalIdSet()
    {
        globalIdSet.Remove(Id);
    }

    protected Character()
    {
    }

    public Character(int id, string name, races race, genders gender)
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

    private double GetHealthDegree(int health)
    {
        if (MaxHealth != 0)
        {
            return (double) health / MaxHealth;
        }
        else
        {
            return 0;
        }
    }

    private void SetStateByHealth(int health)
    {
        double healthDegree = GetHealthDegree(health);
        if (healthDegree >= 0.1)
        {
            State = states.Normal;
        }
        else if (healthDegree == 0)
        {
            State = states.Dead;
        }
        else
        {
            State = states.Weakened;
        }
    }

    #region getters and setters

    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public states State
    {
        get => state;
        set => state = value;
    }

    public bool CanSpeak
    {
        get => canSpeak;
        set => canSpeak = value;
    }

    public bool CanMove
    {
        get => canMove;
        set => canMove = value;
    }

    public races Race
    {
        get => race;
        set => race = value;
    }

    public genders Gender
    {
        get => gender;
        set => gender = value;
    }

    public int Age
    {
        get => age;
        set
        {
            if (value >= age)
            {
                age = value;
            }
            else
            {
                throw new ArgumentException("Возраст не может быть ниже текущего");
            }
        }
    }

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (value > MaxHealth)
            {
                throw new ArgumentException("Текущее здоровье не может стать больше максимального");
            }
            else if (value >= 0)
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
            {
                maxHealth = value;
            }
            else
            {
                throw new ArgumentException("Максимальное здоровье не может стать отрицательным или равным нулю");
            }
        }
    }

    public int Experience
    {
        get => experience;
        set
        {
            if (value >= 0)
            {
                experience = value;
            }
            else
            {
                throw new ArgumentException("Опыт не может стать отрицательным");
            }
        }
    }

    #endregion

    public int HealthDifference()
    {
        return MaxHealth - CurrentHealth;
    }

    public int CompareTo(object obj)
    {
        if (obj is Character)
        {
            return Experience.CompareTo(((Character) obj).Experience);
        }
        else
        {
            throw new ArgumentException("Попытка сравнить разнородные объекты");
        }
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
        {
            artifactList.Remove(artifact);
        }
        else
        {
            throw new RpgException("Персонаж не имеет этого артефакта!");
        }
    }

    public void GiveArtifactTo(AbstractArtifact artifact, Character character)
    {
        RemoveArtifactFromInventory(artifact);
        character.AddArtifactToInventory(artifact);
    }

    public List<AbstractArtifact> GetArtifactInventory()
    {
        if (artifactList.Capacity != 0)
        {
            return artifactList;
        }
        else
        {
            throw new RpgException("Персонаж не имеет ни одного предмета в инвентаре!");
        }
    }

    #region UseArtifact

    public void UseArtifact(ITargetSpell spell, Character character)
    {
        if (State != states.Dead)
        {
            AbstractArtifact abstractArtifact = (AbstractArtifact) spell;
            if (IsInventoryContainsArtifact(abstractArtifact))
            {
                spell.Cast(character);
            }
            else
            {
                throw new RpgException("Персонаж не имеет этот предмет в инвентаре");
            }
        }
        else
        {
            throw new RpgException("Персонаж мёртв!");
        }
    }

    public void UseArtifact(IGradeTargetSpell spell, Character character, int grade)
    {
        if (State != states.Dead)
        {
            AbstractArtifact abstractArtifact = (AbstractArtifact) spell;
            if (IsInventoryContainsArtifact(abstractArtifact))
            {
                spell.Cast(character, grade);
            }
            else
            {
                throw new RpgException("Персонаж не имеет этот предмет в инвентаре");
            }
        }
        else
        {
            throw new RpgException("Персонаж мёртв!");
        }
    }

    public void UseArtifact(IGradeSpell spell, int grade)
    {
        if (State != states.Dead)
        {
            AbstractArtifact abstractArtifact = (AbstractArtifact) spell;
            if (IsInventoryContainsArtifact(abstractArtifact))
            {
                spell.Cast(grade);
            }
            else
            {
                throw new RpgException("Персонаж не имеет этот предмет в инвентаре");
            }
        }
        else
        {
            throw new RpgException("Персонаж мёртв!");
        }
    }

    public void UseArtifact(ISelfSpell spell)
    {
        if (State != states.Dead)
        {
            AbstractArtifact abstractArtifact = (AbstractArtifact) spell;
            if (IsInventoryContainsArtifact(abstractArtifact))
            {
                spell.Cast();
            }
            else
            {
                throw new RpgException("Персонаж не имеет этот предмет в инвентаре");
            }
        }
        else
        {
            throw new RpgException("Персонаж мёртв!");
        }
    }

    #endregion

    private string GetArtifactsInInventoryToPrint()
    {
        try
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Inventory=");
            List<AbstractArtifact> learnedSpells = GetArtifactInventory();
            for (int i = 0; i < learnedSpells.Count; i++)
            {
                string[] classPath = learnedSpells[i].ToString().Split('.');
                stringBuilder.Append(classPath[classPath.Length - 1]);
                if (i != learnedSpells.Count - 1)
                {
                    stringBuilder.Append(", ");
                }
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
}