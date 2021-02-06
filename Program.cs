using EDU_OOP_RPG.Spells;
using EDU_OOP_RPG.Spells.BaseSpells.SpellInterfaces.Artifacts;

namespace EDU_OOP_RPG
{
    class Program
    {
        private static AddHealthSpell _addHealthSpell = new AddHealthSpell();
        private static Heal _heal = new Heal(20);
        private static Antidote _antidote = new Antidote(30);
        private static Revive _revive = new Revive(150);
        private static Armor _armor = new Armor();
        private static Unparalyze _unparalyze = new Unparalyze(85);

        private static LivingWaterBottle _littleLivingWaterBottle = new LivingWaterBottle(10, false);
        private static LivingWaterBottle _mediumLivingWaterBottle = new LivingWaterBottle(25, false);
        private static LivingWaterBottle _bigLivingWaterBottle = new LivingWaterBottle(50, false);
        private static DeadWaterBottle _littleDeadWaterBottle = new DeadWaterBottle(10, false);
        private static DeadWaterBottle _mediumDeadWaterBottle = new DeadWaterBottle(25, false);
        private static DeadWaterBottle _bigDeadWaterBottle = new DeadWaterBottle(50, false);
        private static LightningStaff _lightningStaff = new LightningStaff(125, true);
        private static FrogLegsDecoct _frogLegsDecoct = new FrogLegsDecoct(1, false);
        private static PoisonousSaliva _poisonousSaliva = new PoisonousSaliva(150, true);
        private static BasiliskEye _basiliskEye = new BasiliskEye(1, false);
        private static HealingArtifact _healingArtifact = new HealingArtifact(100, true);

        static void Main(string[] args)
        {
        }
    }
}