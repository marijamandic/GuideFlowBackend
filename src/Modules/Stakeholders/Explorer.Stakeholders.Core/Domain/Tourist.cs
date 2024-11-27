namespace Explorer.Stakeholders.Core.Domain;

public class Tourist : User
{
    public double Wallet { get; private set; }
    public int Xp { get; private set; }
    public int Level { get; private set; }

    public Tourist(string username, string password, UserRole role, bool isActive, Location location, double wallet, int xp, int level) : base(username, password, role, isActive, location)
    {
        Wallet = wallet;
        Xp = xp;
        Level = level;
    }
}
