namespace SemesterProjectAwaze.Services
{
    public interface ILoginService
    {
        bool IsProfileOwner { get; }
        string ProfileName { get; }
        bool IsLoggedIn { get; }
        void SetProfileLoggedIn(string name, bool isOwner);
        void ProfileLoggedOut();
    }
}
