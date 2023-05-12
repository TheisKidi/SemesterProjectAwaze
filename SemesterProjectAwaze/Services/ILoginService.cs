namespace SemesterProjectAwaze.Services
{
    public interface ILoginService
    {
        bool IsProfileOwner { get; }
        string ProfileName { get; }
        string Email { get; }
        bool IsLoggedIn { get; }
        void SetProfileLoggedIn(string name, string email, bool isOwner);
        void ProfileLoggedOut();
    }
}
