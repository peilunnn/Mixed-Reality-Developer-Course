using System;

namespace MRDC_Lesson_2_Intro
{
    class Program
    {
        static void Main(string[] args)
        {
            int MyCountDrinks = 3;
            string ThingToCount = "drinks";
            PrintCount(MyCountDrinks, ThingToCount);
        }
        
        static int AddMysteryDrink(int InitialCountDrinks)
        {
            return InitialCountDrinks + 25;
        }
        
        static void PrintCount(int counter, string noun)
        {
            Console.WriteLine("you now have " + counter + " " + noun);
        }
    }
}
