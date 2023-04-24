namespace TodoREST.utility
{
    internal class Utility
    {
        public static int GetUniqueId()
        {
            Random rnd = new Random();
            int id = rnd.Next(1000,2147483647);
            return id;
        }
    }
}
