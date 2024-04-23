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
                return Attack + Inventory.CalculateEquippedStats().TotalAttack;
            }
        }
        public int Defense { get; set; }
        public int TotalDefense
        {
            get
            {
                return Defense + Inventory.CalculateEquippedStats().TotalDefense;
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
            var stats = Inventory.CalculateEquippedStats();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Lv. {Level}");
            sb.AppendLine($"Chad ({Job})");
            sb.Append($"공격력 : {Attack}");
            if (stats.TotalAttack != 0) sb.Append($" (+{stats.TotalAttack})");
            sb.Append($"\n방어력 : {Defense}");
            if (stats.TotalDefense != 0) sb.Append($" (+{stats.TotalDefense})");
            sb.AppendLine($"\n체력 : {HP}");
            sb.AppendLine($"Gold : {Gold} G");

            Console.WriteLine(sb.ToString());
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
    }
}