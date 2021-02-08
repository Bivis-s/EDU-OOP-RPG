using System;
using System.Collections.Generic;
using System.Text;
using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;

namespace EDU_OOP_RPG.Characters
{
    public class Wizard : Character
    {
        private List<AbstractSpell> spellList = new List<AbstractSpell>();

        private int currentMana;
        private int maxMana;

        private Character character;

        public Wizard(Character character)
        {
            Id = character.Id;
            Name = character.Name;
            State = character.State;
            CanSpeak = character.CanSpeak;
            CanMove = character.CanMove;
            Race = character.Race;
            Gender = character.Gender;
            Age = character.Age;
            MaxHealth = character.MaxHealth;
            CurrentHealth = character.CurrentHealth;
            Experience = character.Experience;
        }

        public Wizard(int id, string name, races race, genders gender) : base(id, name,
            race, gender)
        {
        }

        public int CurrentMana
        {
            get => currentMana;
            set
            {
                if (value > MaxMana)
                {
                    throw new ArgumentException("Текущая мана не может стать больше максимальной");
                }
                else if (value >= 0)
                {
                    currentMana = value;
                }
                else
                {
                    throw new ArgumentException("Мана не может стать отрицательной");
                }
            }
        }

        public int MaxMana
        {
            get => maxMana;
            set
            {
                if (value >= 0)
                {
                    maxMana = value;
                }
                else
                {
                    throw new ArgumentException("Максимальная мана не может стать отрицательной");
                }
            }
        }

        public int ManaDifference()
        {
            return MaxMana - CurrentMana;
        }

        public bool IsSpellLearned(AbstractSpell spell)
        {
            return spellList.Contains(spell);
        }

        public void LearnSpell(AbstractSpell spell)
        {
            if (!IsSpellLearned(spell))
            {
                spellList.Add(spell);
            }
            else
            {
                throw new RpgException("Персонаж уже знает это заклинание");
            }
        }

        public void ForgetSpell(AbstractSpell spell)
        {
            if (IsSpellLearned(spell))
            {
                spellList.Remove(spell);
            }
            else
            {
                throw new RpgException("Персонаж не знает этого заклинания!");
            }
        }

        public List<AbstractSpell> GetLearnedSpells()
        {
            if (spellList.Capacity != 0)
            {
                return spellList;
            }
            else
            {
                throw new RpgException("Персонаж не знает ни одного заклинания!");
            }
        }

        #region CastSpell

        public void CastSpell(ITargetSpell spell, Character character)
        {
            if (State != states.Dead)
            {
                AbstractSpell abstractSpell = (AbstractSpell) spell;
                if (IsSpellLearned(abstractSpell))
                {
                    int manaCost = abstractSpell.ManaCost;
                    if (manaCost <= CurrentMana)
                    {
                        spell.Cast(character);
                    }
                    else
                    {
                        throw new RpgException("Недостаточно маны");
                    }

                    CurrentMana -= manaCost;
                }
                else
                {
                    throw new RpgException("Персонаж не знает этого заклинания!");
                }
            }
            else
            {
                throw new RpgException("Персонаж мёртв!");
            }
        }

        public void CastSpell(IGradeTargetSpell spell, Character character, int grade)
        {
            if (State != states.Dead)
            {
                AbstractSpell abstractSpell = (AbstractSpell) spell;
                if (IsSpellLearned(abstractSpell))
                {
                    int manaCost = abstractSpell.ManaCost + grade;
                    if (manaCost <= CurrentMana)
                    {
                        spell.Cast(character, grade);
                    }
                    else
                    {
                        throw new RpgException("Недостаточно маны");
                    }

                    CurrentMana -= manaCost;
                }
                else
                {
                    throw new RpgException("Персонаж не знает этого заклинания!");
                }
            }
            else
            {
                throw new RpgException("Персонаж мёртв!");
            }
        }

        public void CastSpell(IGradeSpell spell, int grade)
        {
            if (State != states.Dead)
            {
                AbstractSpell abstractSpell = (AbstractSpell) spell;
                if (IsSpellLearned(abstractSpell))
                {
                    int manaCost = abstractSpell.ManaCost + grade;
                    if (manaCost <= CurrentMana)
                    {
                        spell.Cast(grade);
                    }
                    else
                    {
                        throw new RpgException("Недостаточно маны");
                    }

                    CurrentMana -= manaCost;
                }
                else
                {
                    throw new RpgException("Персонаж не знает этого заклинания!");
                }
            }
            else
            {
                throw new RpgException("Персонаж мёртв!");
            }
        }

        public void CastSpell(ISelfSpell spell)
        {
            if (State != states.Dead)
            {
                AbstractSpell abstractSpell = (AbstractSpell) spell;
                if (IsSpellLearned(abstractSpell))
                {
                    int manaCost = abstractSpell.ManaCost;
                    if (manaCost <= CurrentMana)
                    {
                        spell.Cast();
                    }
                    else
                    {
                        throw new RpgException("Недостаточно маны");
                    }

                    CurrentMana -= manaCost;
                }
                else
                {
                    throw new RpgException("Персонаж не знает этого заклинания!");
                }
            }
            else
            {
                throw new RpgException("Персонаж мёртв!");
            }
        }

        #endregion

        private string GetLearnedSpellToPrint()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("learnedSpells=");
                List<AbstractSpell> learnedSpells = GetLearnedSpells();
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
            catch (RpgException e)
            {
                return "No spell learned";
            }
        }

        public override string ToString()
        {
            return base.ToString() + " " +
                   " currentMana=" + CurrentMana +
                   " maxMana=" + maxMana + " " +
                   GetLearnedSpellToPrint();
        }
    }
}