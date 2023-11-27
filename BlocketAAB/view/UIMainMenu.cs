using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocketAAB.view
{
    public class UIMainMenu
    {
          private static readonly Logger logger = LogManager.GetCurrentClassLogger();
            private readonly UICategoryMenu categoryMenu;
            private readonly UIAdvertisementMenu advertisementMenu;

            public UIMainMenu(UICategoryMenu categoryMenu, UIAdvertisementMenu advertisementMenu)
            {
                this.categoryMenu = categoryMenu;
                this.advertisementMenu = advertisementMenu;
            }

            public void DisplayMainMenu()
            {
                Console.Clear();
                Console.WriteLine($"                                          ╔════════════════════════════════════════╗");
                Console.WriteLine($"                                          ║      Welcome to Blocket - Main Menu    ║");
                Console.WriteLine($"                                          ╠════════════════════════════════════════╣");
                Console.WriteLine($"                                          ║  1. Advertisement Management           ║");
                Console.WriteLine($"                                          ║  2. Category Management                ║");
                Console.WriteLine($"                                          ║  3. Exit                               ║");
                Console.WriteLine($"                                          ╚════════════════════════════════════════╝");
                Console.Write("Enter your choice (1, 2, or 3): ");
            }

            public void DisplayExitMessage()
            {
                Console.WriteLine($"                                          ╔════════════════════════════════════════════════╗");
                Console.WriteLine($"                                          ║  Exiting program. Thank you for using Blocket! ║");
                Console.WriteLine($"                                          ╚════════════════════════════════════════════════╝");
            }

            public void DisplayInvalidChoiceMessage()
            {
                Console.WriteLine($"                                          ╔═════════════════════════════════════════════════╗");
                Console.WriteLine($"                                          ║ Invalid choice. Please enter a valid option.    ║");
                Console.WriteLine($"                                          ╚═════════════════════════════════════════════════╝");
            }

            public void RunSelectedMenu(int choice)
            {
            Console.Clear();
            switch (choice)
                {
                    case 1:
                        Console.Clear();
                        advertisementMenu.DisplayAdvertisementMenu();
                        break;
                    case 2:
                        Console.Clear();
                        categoryMenu.DisplayCategoryMenu();
                        break;
                    case 3:
                        DisplayExitMessage();
                        break;
                    default:
                        DisplayInvalidChoiceMessage();
                        break;
                }
            }
        }
    }
