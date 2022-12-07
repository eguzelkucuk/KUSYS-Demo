namespace KUSYS_Demo.Repositories.Implementation
{
    public class StaticDatas
    {
        private static string _email = null;
        private static string _role = null;

        private StaticDatas()
        {

        }
        public StaticDatas(string email,string role)
        {
            _email = email; 
            _role = role;   
        }
        public static string getUserMail()
        {

                return _email;
            
        }
        public static string getUserRole()
        {

            return _role;

        }
    }

    


}
