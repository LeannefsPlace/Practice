using LizaItogPractice.MainForms;

namespace LizaItogPractice
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            String connectionString = "Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903";
            DAO dao = new DAO(connectionString);
            ApplicationConfiguration.Initialize();
            
            FormEnter helloForm = new FormEnter();
            Application.Run(helloForm);

            //Я ненавижу свою жизнь
        }
    }
}