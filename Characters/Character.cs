using System;
using System.Collections;
using System.Collections.Generic;

public enum states
{
    Normal,
    Weakened,
    Ill,
    Poisoned,
    Paralyzed,
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

    private static HashSet<Int32> globalIdSet;
    public Character(int id, string name, races race, genders gender)
    {
        if (!globalIdSet.Contains(id))
        {
            this.id = id;
        } else
        {
            throw new ArgumentException("Идентификатор персонажа не уникален!");
        }
        this.name = name;
        this.race = race;
        this.gender = gender;
    }

    private double GetHealthDegree(int health)
    {
        if (MaxHealth != 0)
        {
            return health / MaxHealth;
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
        } else if (healthDegree == 0)
        {
            State = states.Dead;
        } else
        {
            State = states.Weakened;
        }
    }

    #region getters and setters
    public int Id
    {
        get => id;
    }

    public string Name
    {
        get => name;
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
    }

    public genders Gender
    {
        get => gender;
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
            } else if (value >= 0)
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

    public override string ToString()
    {
        return "Character" + "\n" +
                "id=" + Id + "\n" +
                "name='" + Name + '\'' + "\n" +
                "state='" + State + '\'' + "\n" +
                "canSpeak=" + CanSpeak + "\n" +
                "canMove=" + CanMove + "\n" +
                "race='" + Race + '\'' + "\n" +
                "gender='" + Gender + '\'' + "\n" +
                "ge=" + Age + "\n" +
                "currentHealth=" + CurrentHealth + "\n" +
                "maxHealth=" + MaxHealth + "\n" +
                "experience=" + Experience;
    }
}
