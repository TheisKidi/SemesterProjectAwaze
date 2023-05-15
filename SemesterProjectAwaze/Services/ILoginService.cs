namespace SemesterProjectAwaze.Services
{
    public interface ILoginService
    {
        bool IsProfileOwner { get; }
        string ProfileName { get; }
        bool IsLoggedIn { get; }
        string Id { get; }
        void SetProfileLoggedIn(string name, string id, bool isOwner);
        void ProfileLoggedOut();
    }
}
