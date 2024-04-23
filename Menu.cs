using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsharpConsole
{
    public class Menu
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public Action Info { get; set; }

        private List<Func<string>> options;
        private List<Action> actions;
        public Menu()
        {
            options = new List<Func<string>>();
            actions = new List<Action>();
        }

        public void SetTitle(string title, string desc)
        {
            Title = title;
            Desc = desc;
        }

        public void setInfo(Action info)
        {
            Info = info;
        }

        public int AddOption(Func<string> option, Action action)
        {
            options.Add(option);
            actions.Add(action);
            return options.Count();
        }
        public void DeleteOption(int index)
        {
            options.RemoveAt(index);
            actions.RemoveAt(index);
        }

        public void Show()
        {
            if (Title != null) Console.WriteLine(Title);
            if (Desc != null) Console.WriteLine(Desc);

            if (Info != null) { Console.WriteLine(); Info(); }

            Console.WriteLine();
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i].Invoke()}");
            }

            Console.WriteLine("0. 나가기");
        }

        private int GetChoice()
        {
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            string input = Console.ReadLine();
            Console.WriteLine();

            if (int.TryParse(input, out int choice) && (choice <= options.Count || choice == 0))
            {
                return choice;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                return GetChoice();
            }
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();

                Show();
                int choice = GetChoice();

                if (choice == 0)
                {
                    break;
                }

                if (choice > 0 && choice <= actions.Count)
                {
                    actions[choice - 1].Invoke();
                }
                else
                {
                    Console.WriteLine("유효하지 않은 선택입니다. 다시 시도하세요.");
                }
            }
        }
    }
}