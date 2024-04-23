using CsharpConsole;

internal class Program
{
    static Player _player = new Player("이종윤", 1, "전사", 10, 5, 100, 10000);
    static Shop _shop = new Shop();
    static void Main(string[] args)
    {
        InitGame();
        Menu mainMenu = new Menu();
        mainMenu.SetTitle("스파르타 마을에 오신 여러분 환영합니다.", "이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

        mainMenu.AddOption(() => "상태 보기", StatusMenu);
        mainMenu.AddOption(() => "인벤토리", InventoryMenu);
        mainMenu.AddOption(() => "상점", ShopMenu);

        mainMenu.Run();
    }

    static void StatusMenu()
    {
        Menu statusMenu = new Menu();
        statusMenu.SetTitle("상태 보기", "캐릭터의 정보가 표시됩니다.");
        statusMenu.SetInfo(_player.DisplayInfo);
        statusMenu.Run();
    }

    static void InventoryMenu()
    {
        Menu inventoryMenu = new Menu();
        inventoryMenu.SetTitle("인벤토리", "보유 중인 아이템을 관리할 수 있습니다.");
        inventoryMenu.SetInfo(() => _player.Inventory.DisplayInfo());
        inventoryMenu.AddOption(() => "장착 관리", EquipMenu);
        inventoryMenu.Run();
    }

    static void EquipMenu()
    {
        Menu equipMenu = new Menu();
        equipMenu.SetTitle("인벤토리 - 장착 관리", "보유 중인 아이템을 관리할 수 있습니다.");
        equipMenu.SetInfo(() => { _player.Inventory.DisplayInfo(isShowItems: false); });
        for (int i = 0; i < _player.Inventory.Items.Count; i++)
        {
            int localIndex = i;
            equipMenu.AddOption(_player.Inventory.Items[localIndex].ItemInfo, () => { _player.Inventory.Equipped(_player.Inventory.Items[localIndex]); });
        }
        equipMenu.Run();
    }

    static void ShopMenu()
    {
        Menu shopMenu = new Menu();
        shopMenu.SetTitle("상점", "필요한 아이템을 얻을 수 있는 상점입니다.");
        shopMenu.SetInfo(() => { _shop.DisplayInfo(_player); });
        shopMenu.AddOption(() => "아이템 구매", ShopBuyMenu);
        shopMenu.AddOption(() => "아이템 판매", ShopSaleMenu);
        shopMenu.Run();
    }

    static void ShopBuyMenu()
    {
        Menu shopBuyMenu = new Menu();
        shopBuyMenu.SetTitle("상점 - 아이템 구매 ", "필요한 아이템을 얻을 수 있는 상점입니다.");
        shopBuyMenu.SetInfo(() => { _shop.DisplayInfo(_player, isShowItems: false); });
        for (int i = 0; i < _shop.Items.Count; i++)
        {
            int localIndex = i;
            shopBuyMenu.AddOption(_shop.Items[localIndex].ItemInfo, () => { _shop.Buy(_player, _shop.Items[localIndex]); });
        }
        shopBuyMenu.Run();
    }

    static void ShopSaleMenu()
    {
        Menu shopSaleMenu = new Menu();
        shopSaleMenu.SetTitle("상점 - 아이템 구매 ", "필요한 아이템을 얻을 수 있는 상점입니다.");
        shopSaleMenu.SetInfo(() => { _shop.DisplayInfo(_player, isShowItems: false); });
        shopSaleMenu.RefreshMenu = () =>
        {
            shopSaleMenu.ResetOption();
            for (int i = 0; i < _player.Inventory.Items.Count; i++)
            {
                int localIndex = i;
                shopSaleMenu.AddOption(_player.Inventory.Items[localIndex].ItemInfo,
                                        () =>
                                        {
                                            _shop.Sale(_player, _player.Inventory.Items[localIndex]);
                                        });
            }
        };
        shopSaleMenu.Run();
    }



    static void InitGame()
    {
        _shop.AddItem(new ShopItem("수련자의 갑옷", "수련에 도움을 주는 갑옷입니다.", 1000, ItemType.Armor, defense: 5));
        _shop.AddItem(new ShopItem("무쇠 갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 2300, ItemType.Armor, defense: 9));
        _shop.AddItem(new ShopItem("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, ItemType.Armor, defense: 15));
        _shop.AddItem(new ShopItem("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 600, ItemType.Weapon, attack: 2));
        _shop.AddItem(new ShopItem("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 1500, ItemType.Weapon, attack: 5));
        _shop.AddItem(new ShopItem("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000, ItemType.Weapon, attack: 7));

        _shop.AddItem(new ShopItem("광선검", "제다이 전사들이 사용하던 검입니다.", 5000, ItemType.Weapon, attack: 15));
    }
}