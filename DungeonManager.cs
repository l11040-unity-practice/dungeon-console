using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpConsole
{
    public class Dungeon
    {
        public string Name { get; set; }
        public int RewardGold { get; set; }
        public int Defense { get; set; }

        public Dungeon(string name, int rewardGold, int defense)
        {
            Name = name;
            RewardGold = rewardGold;
            Defense = defense;
        }

        // 던전 기본 정보 한줄
        public string DungeonInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Name}\t| ");
            sb.Append($"방어력 {Defense} 이상 권장");

            return sb.ToString();
        }
    }
    public class DungeonManager
    {
        public List<Dungeon> Dungeons { get; set; }
        public DungeonManager()
        {
            Dungeons = new List<Dungeon>();
        }

        // 던전 추가
        public void AddDungeon(Dungeon dungeon)
        {
            Dungeons.Add(dungeon);
        }

        // 던전 입장
        public void EnterDengeon(Player _player, Dungeon dungeon)
        {
            Menu menu = new Menu();
            Random random = new Random();
            bool isClear = false;

            // 방어력에 따른 클리어 여부
            if (_player.TotalDefense < dungeon.Defense &&
                random.Next(0, 10) < 4)
            {
                menu.SetTitle("던전 클리어 실패", $"아쉽습니다ㅠㅠ\n{dungeon.Name}을 클리어 하지 못했습니다.");
                isClear = false;
            }
            else
            {
                menu.SetTitle("던전 클리어", $"축하합니다!!\n{dungeon.Name}을 클리어 하였습니다.");
                isClear = true;
            }

            menu.SetInfo(() =>
            {
                Console.WriteLine("[탐험 결과]");
                if (isClear)
                {
                    int calDefense = dungeon.Defense - _player.TotalDefense;
                    int damage = random.Next(20 + calDefense, 35 + calDefense);
                    Console.WriteLine(_player.TakeDamage(damage));

                    int additionalGoldPer = random.Next(_player.TotalAttack, _player.TotalAttack * 2);
                    int rewardGold = Convert.ToInt32(Math.Round(dungeon.RewardGold * (additionalGoldPer / 100f + 1)));
                    Console.WriteLine(_player.AddGold(rewardGold));
                    _player.LevelUp();
                }
                else
                {
                    Console.WriteLine(_player.TakeDamage(_player.HP / 2));
                }
            });
            menu.Run();
        }
    }
}