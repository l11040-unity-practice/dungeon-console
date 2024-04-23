using System.Text;

namespace CsharpConsole
{
    public enum ItemType
    {
        Armor,
        Weapon,
    }
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Price { get; set; }
        public ItemType Type { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public bool IsEquipped { get; set; } = false;

        public Item(string name, string desc, int price, ItemType type, int attack = 0, int defense = 0)
        {
            Id = Guid.NewGuid();
            Name = name;
            Desc = desc;
            Price = price;
            Type = type;
            Attack = attack;
            Defense = defense;
        }

        public virtual string ItemInfo()
        {
            StringBuilder sb = new StringBuilder();
            if (IsEquipped) sb.Append("[E]");
            sb.Append($"{Name}\t| ");
            if (Attack != 0) sb.Append($"공격력 +{Attack}\t| ");
            if (Defense != 0) sb.Append($"방어력 +{Defense}\t| ");
            sb.Append($"{Desc}");
            return sb.ToString();
        }
    }
}