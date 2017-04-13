namespace ItForum.Controllers.DTO.UserController
{
    public class LoginResponseCode
    {
        public readonly int Logged = 0;
        public readonly int Incorrect = 1;
        public readonly int NotExist = 2;
    }
}