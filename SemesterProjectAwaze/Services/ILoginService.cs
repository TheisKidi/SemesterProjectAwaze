namespace SemesterProjectAwaze.Services
{
    public interface ILoginService
    {
        bool IsProfileOwner { get; }
        string ProfileName { get; }
        bool IsLoggedIn { get; }
        string Email { get; }
        void SetProfileLoggedIn(string name, string email, bool isOwner);
        void ProfileLoggedOut();
    }
}
