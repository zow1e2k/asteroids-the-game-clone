using System;
using System.Windows.Forms;

namespace asteroids_the_game_clone
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Player player = new Player("zow1k");
            Application.Run(new MainScene(player));
        }
    }
}
