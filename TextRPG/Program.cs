// See https://aka.ms/new-console-template for more information
using TextRPG.Data;

namespace TextRPG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataManager.Instance.LoadData();
            GameManager runer = new GameManager();
            runer.StartGame();
            //runer.run();
        }
    }
}