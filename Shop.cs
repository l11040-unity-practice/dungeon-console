using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpConsole
{
    public class ShopItem : Item
    {
        public bool IsSale { get; set; } = false;
        public ShopItem(string name, string desc, int price, ItemType type, int attack = 0, int defense = 0)
        : base(name, desc, price, type, attack, defense) { }

        // 아이템 정보 한줄로 만듦
        public new string ItemInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Name}\t| ");
            if (Attack != 0)
            {
                sb.Append($"공격력 +{Attack}\t| ");
            }
            if (Defense != 0)
            {
                sb.Append($"방어력 +{Defense}\t| ");
            }

            sb.Append($"{Desc}\t| ");

            if (IsSale)
            {
                sb.Append($"구매완료");
            }
            else
            {
                sb.Append($"{Price} G");
            }

            return sb.ToString();
        }
    }
    public class Shop
    {
        // 상점에 진열된 아이템
        public List<ShopItem> Items { get; set; }

        public Shop()
        {
            Items = new List<ShopItem>();
        }

        // 아이템 추가
        public void AddItem(ShopItem item)
        {
            Items.Add(item);


        }

        // Console.write로 보여줄 상점 내용
        public void DisplayInfo(Player player, bool isShowItems = true)
        {
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.Gold} G\n");

            Console.WriteLine("[아이템 목록]");
            if (isShowItems)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    Console.WriteLine($"- {Items[i].ItemInfo()}");
                }
            }
        }

        // 상점 아이템 구매
        public void Buy(Player player, ShopItem buyItem)
        {
            if (buyItem.IsSale)
            {
                Console.WriteLine("구매 완료된 아이템입니다.");
            }
            else if (player.Gold >= buyItem.Price)
            {
                player.Gold -= buyItem.Price;
                player.Inventory.AddItem(buyItem);
                buyItem.IsSale = true;
                Console.WriteLine("구매 완료했습니다.");
            }
            else if (player.Gold < buyItem.Price)
            {
                Console.WriteLine("Gold 가 부족합니다.");
            }
            Console.ReadLine();
        }

        // 상점에 아이템 판매
        public void Sale(Player player, Item invenItem)
        {
            int salePrice = Convert.ToInt32(Math.Round(invenItem.Price * 0.85));
            player.Gold += salePrice;
            player.Inventory.Items.Remove(invenItem);
            Items.Find(item => item.Id == invenItem.Id).IsSale = false;

            Console.WriteLine($"[{invenItem.Name}] 이 {salePrice} G 에 판매되었습니다.");

            Console.ReadLine();
        }
    }
}