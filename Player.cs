using System.Text;

namespace CsharpConsole
{
    public class Player
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public string Job { get; set; }
        public int Attack { get; set; }
        public int TotalAttack
        {
            get
            {
                return Attack + CalculateEquippedStats().equippedAttack + CalculateLevelStats().levelAttack;
            }
        }
        public int Defense { get; set; }
        public int TotalDefense
        {
            get
            {
                return Defense + CalculateEquippedStats().equippedDefense + CalculateLevelStats().levelDefense;
            }
        }
        public int HP { get; set; }
        public int Gold { get; set; }

        public Inventory Inventory { get; set; }
        public Player(string name, int level, string job, int attack, int defense, int hp, int gold)
        {
            Name = name;
            Level = level;
            Job = job;
            Attack = attack;
            Defense = defense;
            HP = hp;
            Gold = gold;
            Inventory = new Inventory();
        }
        public void DisplayInfo()
        {
            var equippedStats = CalculateEquippedStats();
            var levelStats = CalculateLevelStats();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Lv. {Level}");
            sb.AppendLine($"Chad ({Job})");
            sb.Append($"공격력 : {Attack}");
            if (equippedStats.equippedAttack != 0) sb.Append($" (+{equippedStats.equippedAttack})");
            if (levelStats.levelAttack != 0) sb.Append($" (+{levelStats.levelAttack})");
            sb.Append($"\n방어력 : {Defense}");
            if (equippedStats.equippedDefense != 0) sb.Append($" (+{equippedStats.equippedDefense})");
            if (levelStats.levelDefense != 0) sb.Append($" (+{levelStats.levelDefense})");
            sb.AppendLine($"\n체력 : {HP}");
            sb.AppendLine($"Gold : {Gold} G");

            Console.WriteLine(sb.ToString());
        }
        public void LevelUp()
        {
            Level++;
        }

        public string TakeDamage(int damage)
        {
            int oldHP = HP;
            HP -= damage;
            if (HP < 0) HP = 0;
            return $"체력 {oldHP} -> {HP}";
        }

        public string AddGold(int gold)
        {
            int oldGold = Gold;
            Gold += gold;
            return $"Gold {oldGold} G -> {Gold} G";
        }

        public void RestInfo()
        {
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Gold} G\n");
        }

        public void Rest()
        {
            if (Gold >= 500)
            {
                Gold -= 500;
                HP = 100;
                Console.WriteLine("휴식을 완료했습니다.");
            }
            else
            {
                Console.WriteLine("Gold 가 부족합니다");
            }
            Console.ReadLine();
        }

        public (int equippedAttack, int equippedDefense) CalculateEquippedStats()
        {
            int equippedAttack = 0;
            int equippedDefense = 0;

            foreach (var item in Inventory.Items)
            {
                if (item.IsEquipped)
                {
                    equippedAttack += item.Attack;
                    equippedDefense += item.Defense;
                }
            }

            return (equippedAttack, equippedDefense);
        }

        public (int levelAttack, int levelDefense) CalculateLevelStats()
        {
            int levelAttack = (Level - 1) * 1;
            int levelDefense = (Level - 1) * 2;

            return (levelAttack, levelDefense);
        }
    }
}