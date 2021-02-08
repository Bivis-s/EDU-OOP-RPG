using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDU_OOP_RPG.Artifacts;
using EDU_OOP_RPG.Characters;
using EDU_OOP_RPG.Exceptions;
using EDU_OOP_RPG.Spells;
using EDU_OOP_RPG.Spells.BaseSpells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts;

namespace EDU_OOP_RPG
{
    internal class Run
    {
        private static readonly List<Character> globalCharacterList = new List<Character>();
        private static Character globalCurrentCharacter;

        private static readonly List<AbstractSpell> globalSpellList = new List<AbstractSpell>
        {
            new AddHealthSpell(),
            new Heal(20),
            new Antidote(30),
            new Revive(150),
            new Armor(),
            new Unparalyze(85)
        };

        private static readonly List<AbstractArtifact> globalArtifactList = new List<AbstractArtifact>
        {
            new LivingWaterBottle(10, false),
            new LivingWaterBottle(25, false),
            new LivingWaterBottle(50, false),
            new DeadWaterBottle(10, false),
            new DeadWaterBottle(25, false),
            new DeadWaterBottle(50, false),
            new LightningStaff(125, true),
            new FrogLegsDecoct(1, false),
            new PoisonousSaliva(150, true),
            new BasiliskEye(1, false),
            new HealingArtifact(100, true)
        };

        private static Character CreateStandardCharacterFromConsole()
        {
            var id = 0;
            var name = "Default";
            var race = Races.Human;
            var gender = Genders.Male;

            var state = true;
            while (state)
            {
                var subState = true;
                while (subState)
                {
                    Console.WriteLine("Введите уникальный id:");
                    try
                    {
                        id = Convert.ToInt32(Console.ReadLine());
                        subState = false;
                    }
                    catch (FormatException)
                    {
                        Console.Write("Введено неверное значение id");
                    }
                    catch (ArgumentException e)
                    {
                        Console.Write(e.Message);
                    }
                }

                subState = true;
                while (subState)
                {
                    Console.WriteLine("Введите имя:");
                    name = Console.ReadLine();
                    subState = false;
                }

                subState = true;
                while (subState)
                {
                    Console.WriteLine("Выберите расу персонажа:\n" +
                                      "1 - Человек\n" +
                                      "2 - Гном\n" +
                                      "3 - Эльф\n" +
                                      "4 - Орк\n" +
                                      "5 - Гоблин\n\n");
                    try
                    {
                        var sw = Convert.ToInt32(Console.ReadLine());
                        if (sw >= 1 && sw <= 5)
                        {
                            switch (sw)
                            {
                                case 1:
                                    race = Races.Human;
                                    break;
                                case 2:
                                    race = Races.Gnome;
                                    break;
                                case 3:
                                    race = Races.Elf;
                                    break;
                                case 4:
                                    race = Races.Orc;
                                    break;
                                case 5:
                                    race = Races.Goblin;
                                    break;
                            }

                            subState = false;
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели значение вне диапазона допустимых");
                        }
                    }
                    catch (Exception)
                    {
                        Console.Write("Введено неверное значение");
                    }
                }

                subState = true;
                while (subState)
                {
                    Console.WriteLine("Выберите пол персонажа:\n" +
                                      "1 - Мужчина\n" +
                                      "2 - Женщина\n\n");
                    try
                    {
                        var sw = Convert.ToInt32(Console.ReadLine());
                        if (sw >= 1 && sw <= 2)
                        {
                            switch (sw)
                            {
                                case 1:
                                    gender = Genders.Male;
                                    break;
                                case 2:
                                    gender = Genders.Female;
                                    break;
                            }

                            subState = false;
                            state = false;
                        }
                        else
                        {
                            Console.WriteLine("Вы ввели значение вне диапазона допустимых");
                        }
                    }
                    catch (Exception)
                    {
                        Console.Write("Введено неверное значение");
                    }
                }
            }

            return new Character(id, name, race, gender);
        }

        private static Character CreateFullCharacterFromConsole()
        {
            var character = CreateStandardCharacterFromConsole();

            var subState = true;
            while (subState)
            {
                Console.WriteLine("Введите возраст персонажа: ");
                try
                {
                    character.Age = Convert.ToInt32(Console.ReadLine());
                    subState = false;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            subState = true;
            while (subState)
            {
                Console.WriteLine("Введите значение максимального здоровья персонажа персонажа: ");
                try
                {
                    character.MaxHealth = Convert.ToInt32(Console.ReadLine());
                    subState = false;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            subState = true;
            while (subState)
            {
                Console.WriteLine("Введите значение текущего здоровья персонажа: ");
                try
                {
                    character.CurrentHealth = Convert.ToInt32(Console.ReadLine());
                    subState = false;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            subState = true;
            while (subState)
            {
                Console.WriteLine("Введите значение опыта персонажа: ");
                try
                {
                    character.Experience = Convert.ToInt32(Console.ReadLine());
                    subState = false;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            subState = true;
            while (subState)
            {
                Console.WriteLine("Может ли персонаж двигаться?:\n" +
                                  "1 - Да\n" +
                                  "2 - Нет\n\n");
                try
                {
                    var sw = Convert.ToInt32(Console.ReadLine());
                    if (sw >= 1 && sw <= 2)
                    {
                        switch (sw)
                        {
                            case 1:
                                character.CanMove = true;
                                break;
                            case 2:
                                character.CanMove = false;
                                break;
                        }

                        subState = false;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели значение вне диапазона допустимых");
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            subState = true;
            while (subState)
            {
                Console.WriteLine("Может ли персонаж говорить?:\n" +
                                  "1 - Да\n" +
                                  "2 - Нет\n\n");
                try
                {
                    var sw = Convert.ToInt32(Console.ReadLine());
                    if (sw >= 1 && sw <= 2)
                    {
                        switch (sw)
                        {
                            case 1:
                                character.CanSpeak = true;
                                break;
                            case 2:
                                character.CanSpeak = false;
                                break;
                        }

                        subState = false;
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели значение вне диапазона допустимых");
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            return character;
        }

        private static Wizard CreateFullWizardFromConsole()
        {
            var wizard = new Wizard(CreateFullCharacterFromConsole());

            while (true)
            {
                Console.WriteLine("Введите значение максимальной маны персонажа персонажа: ");
                try
                {
                    wizard.MaxMana = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            while (true)
            {
                Console.WriteLine("Введите значение текущей маны персонажа: ");
                try
                {
                    wizard.CurrentMana = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                }
            }

            return wizard;
        }

        private static void AddCharacterToListFromConsole()
        {
            Character character = null;
            var subState = true;
            while (subState)
            {
                Console.WriteLine("Выберите тип создаваемого персонажа:\n" +
                                  "1 - Обычный\n" +
                                  "2 - Маг\n\n");

                try
                {
                    var sw = Convert.ToInt32(Console.ReadLine());
                    switch (sw)
                    {
                        case 1:
                            character = CreateFullCharacterFromConsole();
                            subState = false;
                            break;
                        case 2:
                            character = CreateFullWizardFromConsole();
                            subState = false;
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.Write("Введено неверное значение");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            globalCharacterList.Add(character);
        }

        private static void PrintCharacterList()
        {
            if (globalCharacterList.Count != 0)
            {
                var stringBuilder = new StringBuilder();
                foreach (var character in globalCharacterList)
                    stringBuilder
                        .Append(character)
                        .Append("\n\n");

                Console.WriteLine(stringBuilder.ToString());
            }
            else
            {
                Console.WriteLine("Ни один персонаж не создан");
            }
        }

        private static Character GetCharacterFromConsole()
        {
            if (globalCharacterList.Count != 0)
                try
                {
                    var subState = true;
                    while (subState)
                    {
                        Console.WriteLine("Введите id персонажа: ");
                        var characterId = 0;
                        try
                        {
                            characterId = Convert.ToInt32(Console.ReadLine());
                            subState = false;
                        }
                        catch (Exception e)
                        {
                            Console.Write(e.Message);
                        }

                        var selectedCharacter = from t in globalCharacterList
                            where t.Id.Equals(characterId)
                            select t;
                        return selectedCharacter.First();
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Персонаж с заданным id не найден");
                }
            else
                Console.WriteLine("Список персонажей пуст!");

            throw new RpgException("Не удалось выбрать персонажа");
        }

        private static void PrintGlobalArtifactList()
        {
            for (var i = 0; i < globalArtifactList.Count; i++) Console.WriteLine(i + " - " + globalArtifactList[i]);
        }

        private static void PrintArtifactInventory(Character character)
        {
            for (var i = 0; i < character.GetArtifactInventory().Count; i++)
                Console.WriteLine(i + " - " + character.GetArtifactInventory()[i]);
        }

        private static int GetGradeFromConsole()
        {
            while (true)
            {
                Console.WriteLine("Введите силу воздействия: ");
                try
                {
                    return Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Вы ввели неверное значение");
                }
            }
        }

        private static void UseArtifactFromConsole(Character character)
        {
            PrintCharacterList();
            PrintArtifactInventory(character);
            Console.WriteLine("Введите номер артефакта: ");
            int num;
            try
            {
                num = Convert.ToInt32(Console.ReadLine());
                if (num >= 0 && num < character.GetArtifactInventory().Count)
                {
                    var artifact = character.GetArtifactInventory()[num];

                    if (artifact is IGradeTargetSpell)
                        character.UseArtifact((IGradeTargetSpell) artifact, GetCharacterFromConsole(),
                            GetGradeFromConsole());
                    else if (artifact is ITargetSpell)
                        character.UseArtifact((ITargetSpell) artifact, GetCharacterFromConsole());

                    else if (artifact is IGradeSpell)
                        character.UseArtifact((IGradeSpell) artifact,
                            GetGradeFromConsole());

                    else if (artifact is ISelfSpell) character.UseArtifact((ISelfSpell) artifact);
                }
                else
                {
                    Console.WriteLine("Артефакта под таким номером нет в инвентаре");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void TransferArtifact(Character character, Character character2)
        {
            var state = true;
            while (state)
            {
                PrintArtifactInventory(character);
                Console.WriteLine("Введите номер артефакта: ");
                int num;
                try
                {
                    num = Convert.ToInt32(Console.ReadLine());
                    if (num >= 0 && num < character.GetArtifactInventory().Count)
                    {
                        character.GiveArtifactTo(character.GetArtifactInventory()[num], character2);
                        state = false;
                    }
                    else
                    {
                        Console.WriteLine("Артефакта под таким номером нет в инвентаре");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void RemoveArtifactFromInventory(Character character)
        {
            PrintArtifactInventory(character);
            Console.WriteLine("Введите номер артефакта: ");
            int num;
            try
            {
                num = Convert.ToInt32(Console.ReadLine());
                if (num >= 0 && num < character.GetArtifactInventory().Count)
                    character.RemoveArtifactFromInventory(character.GetArtifactInventory()[num]);
                else
                    Console.WriteLine("Артефакта под таким номером нет в инвентаре");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void AddArtifactFromGlobalListToInventory(Character character)
        {
            PrintGlobalArtifactList();
            Console.WriteLine("Введите номер артефакта: ");
            int num;
            try
            {
                num = Convert.ToInt32(Console.ReadLine());
                if (num >= 0 && num < globalArtifactList.Count)
                    character.AddArtifactToInventory(globalArtifactList[num]);
                else
                    Console.WriteLine("Артефакта под таким номером нет");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void PrintGlobalSpellList()
        {
            for (var i = 0; i < globalSpellList.Count; i++) Console.WriteLine(i + " - " + globalSpellList[i]);
        }

        private static void LearnSpellFromGlobalListFromConsole(Character character)
        {
            if (character is Wizard)
            {
                PrintGlobalSpellList();
                Console.WriteLine("Введите номер заклинания: ");
                int num;
                try
                {
                    num = Convert.ToInt32(Console.ReadLine());
                    if (num >= 0 && num < globalSpellList.Count)
                        ((Wizard) character).LearnSpell(globalSpellList[num]);
                    else
                        Console.WriteLine("Заклинания под таким номером нет");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Персонаж не владеет магией!");
            }
        }

        private static void PrintLearnedSpell(Wizard wizard)
        {
            for (var i = 0; i < wizard.GetLearnedSpells().Count; i++)
                Console.WriteLine(i + " - " + wizard.GetLearnedSpells()[i]);
        }

        private static void ForgetSpellFromConsole(Character character)
        {
            if (character is Wizard)
            {
                var wizard = (Wizard) character;
                PrintLearnedSpell(wizard);
                Console.WriteLine("Введите номер артефакта: ");
                int num;
                try
                {
                    num = Convert.ToInt32(Console.ReadLine());
                    if (num >= 0 && num < wizard.GetLearnedSpells().Count)
                        wizard.ForgetSpell(wizard.GetLearnedSpells()[num]);
                    else
                        Console.WriteLine("Артефакта под таким номером нет в инвентаре");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Персонаж не владеет магией");
            }
        }

        private static void CastSpellFromConsole(Character character)
        {
            if (character is Wizard)
            {
                var wizard = (Wizard) character;
                PrintCharacterList();
                PrintLearnedSpell(wizard);
                Console.WriteLine("Введите номер заклинания: ");
                int num;
                try
                {
                    num = Convert.ToInt32(Console.ReadLine());
                    if (num >= 0 && num < wizard.GetLearnedSpells().Count)
                    {
                        var spell = wizard.GetLearnedSpells()[num];

                        if (spell is IGradeTargetSpell)
                            wizard.CastSpell((IGradeTargetSpell) spell, GetCharacterFromConsole(),
                                GetGradeFromConsole());
                        else if (spell is ITargetSpell)
                            wizard.CastSpell((ITargetSpell) spell, GetCharacterFromConsole());

                        else if (spell is IGradeSpell)
                            wizard.CastSpell((IGradeSpell) spell,
                                GetGradeFromConsole());

                        else if (spell is ISelfSpell) wizard.CastSpell((ISelfSpell) spell);
                    }
                    else
                    {
                        Console.WriteLine("Артефакта под таким номером нет в инвентаре");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Персонаж не владеет магией!");
            }
        }

        private static void CharacterConsoleMenu()
        {
            try
            {
                PrintCharacterList();
                globalCurrentCharacter = GetCharacterFromConsole();

                var subState = true;
                while (subState)
                {
                    Console.WriteLine("Выберите действие:\n" +
                                      "1 - Добавить предмет в инвентарь\n" +
                                      "2 - Выбросить предмет из инвентаря\n" +
                                      "3 - Передать артефакт другому персонажу\n" +
                                      "4 - Использовать артефакт\n" +
                                      "5 - Выучить заклинание\n" +
                                      "6 - Забыть заклинание\n" +
                                      "7 - Произнести заклинание\n" +
                                      "0 - Выход\n\n");
                    try
                    {
                        var sw = Convert.ToInt32(Console.ReadLine());
                        switch (sw)
                        {
                            case 1:
                                AddArtifactFromGlobalListToInventory(globalCurrentCharacter);
                                break;
                            case 2:
                                RemoveArtifactFromInventory(globalCurrentCharacter);
                                break;
                            case 3:
                                TransferArtifact(globalCurrentCharacter, GetCharacterFromConsole());
                                break;
                            case 4:
                                UseArtifactFromConsole(globalCurrentCharacter);
                                break;
                            case 5:
                                LearnSpellFromGlobalListFromConsole(globalCurrentCharacter);
                                break;
                            case 6:
                                ForgetSpellFromConsole(globalCurrentCharacter);
                                break;
                            case 7:
                                CastSpellFromConsole(globalCurrentCharacter);
                                break;
                            case 0:
                                subState = false;
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private static void Main(string[] args)
        {
            var state = true;
            while (state)
            {
                Console.WriteLine(
                    "Выберите действие:\n" +
                    "1 - Вывод списка персонажей\n" +
                    "2 - Создать персонажа\n" +
                    "3 - Войти в меню персонажа\n" +
                    "0 - Выход\n\n"
                );
                try
                {
                    var sw = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch (sw)
                    {
                        case 1:
                            PrintCharacterList();
                            break;
                        case 2:
                            AddCharacterToListFromConsole();
                            break;
                        case 3:
                            CharacterConsoleMenu();
                            break;
                        case 0:
                            state = false;
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Введено неверное значение");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}