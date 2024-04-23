namespace CsharpConsole
{
    public class Inventory
    {
        public List<Item> Items { get; set; }
        public Inventory()
        {
            Items = new List<Item>();
        }
        public void DisplayInfo(bool isShowItems = true)
        {
            Console.WriteLine("[아이템 목록]");
            if (isShowItems)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine($"- {Items[i].ItemInfo()}");
                }
            }
        }
        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public void Equipped(Item item)
        {
            if (item.IsEquipped)
            {
                Console.WriteLine($"[{item.Name}] 장착을 해제합니다.");
            }
            else
            {
                Item currentEquip = Items.Find(iItem => iItem.IsEquipped && iItem.Type == item.Type);
                if (currentEquip != null)
                {
                    currentEquip.IsEquipped = false;
                }
                Console.WriteLine($"[{item.Name}] 장착합니다.");
            }
            item.IsEquipped = !item.IsEquipped;
            Console.ReadLine();
        }
    }
}