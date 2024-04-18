namespace Bakery.Models;

public class UserRoles
{
    public const string Administrator = "Administrator";
    public const string Manager = "Manager";
    public const string Baker = "Baker";
    public const string Driver = "Driver";

    public static List<string> users = new List<string>()
    {
        Administrator,
        Manager,
        Baker,
        Driver
    };

}