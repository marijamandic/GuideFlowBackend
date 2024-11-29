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
    public void UpdateWallet(double wallet)
    {
        Wallet = wallet;
    }

    public void UpdateXp(int xp)
    {
        Xp = xp;
        UpdateLevel();
    }

    public void UpdateLevel()
    {
        int newLevel = Level;
        int xpRequiredForNextLevel = 20 * newLevel;

        int remainingXp = Xp;

        while (remainingXp >= xpRequiredForNextLevel)
        {
            remainingXp -= xpRequiredForNextLevel;
            newLevel++;
            xpRequiredForNextLevel = 20 * newLevel;
        }

        Xp = remainingXp;

        Level = newLevel;
    }
    public void AddXp(int amount)
    {
        Xp += amount;
        UpdateLevel();
    }
}
