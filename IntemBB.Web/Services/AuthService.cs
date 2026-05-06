namespace IntemBB.Web.Services;

public class AuthService
{
    private static readonly Dictionary<string, (string Password, string Name)> Users = new()
    {
        { "1", ("321", "Admin") },
        { "014185", ("thinghiemp8700", "Tùng") },
        { "005571", ("thinghiemp8700", "Thuần") },
        { "020569", ("thinghiemp8700", "Chương") },
        { "213785", ("thinghiemp8700", "Jen Hao") },
        { "018892", ("ha19950305", "Hạ") },
        { "023999", ("123456", "Minh Đăng") },
        { "025839", ("025839", "Đức") }
    };

    public (bool success, string name) ValidateUser(string username, string password)
    {
        if (Users.TryGetValue(username, out var user) && user.Password == password)
            return (true, user.Name);
        return (false, string.Empty);
    }
}
