using BlocketAAB;
using BlocketAAB.Repo;
using BlocketAAB.view;
using NLog;


public class Program
{
    

    static void Main(string[] args)
    {
        
        var categoryRepository = new CategoryRepository();
        var advertisementRepository = new AdvertisementRepository();

        var categoryMenu = new UICategoryMenu(categoryRepository);
        var advertisementMenu = new UIAdvertisementMenu(advertisementRepository);

        var mainMenu = new UIMainMenu(categoryMenu, advertisementMenu);

        int choice = 0; 

        do
        {
            Console.Clear();
            mainMenu.DisplayMainMenu();

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                mainMenu.RunSelectedMenu(choice);
            }
            else
            {
                mainMenu.DisplayInvalidChoiceMessage();
            }

        } while (choice != 3); 
    }
}