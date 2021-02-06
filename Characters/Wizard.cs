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
            spellList.Add(spell);
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

        public void CastSpell(ITargetSpell spell, Character character)
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

        public void CastSpell(IGradeTargetSpell spell, Character character, int grade)
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

        public void CastSpell(IGradeSpell spell, int grade)
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

        public void CastSpell(ISelfSpell spell)
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

        private string GetLearnedSpellToPrint()
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("learnedSpells=");
                foreach (AbstractSpell spell in GetLearnedSpells())
                {
                    string[] classPath = spell.ToString().Split('.');
                    stringBuilder.Append(classPath[classPath.Length-1]).Append(", ");
                }

                stringBuilder.Append("\n");
                return stringBuilder.ToString();
            }
            catch (RpgException e)
            {
                return "No spell learned";
            }
        }

        public override string ToString()
        {
            return base.ToString() + "\n" +
                   "currentMana=" + CurrentMana + "\n" +
                   "maxMana=" + maxMana + "\n" +
                   GetLearnedSpellToPrint();
        }
    }
}