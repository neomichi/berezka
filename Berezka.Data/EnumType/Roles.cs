using System.ComponentModel;

namespace Berezka.Data.EnumType
{
    public enum Roles
    {
        [Description("user")]
        User = 10,
        [Description("manager")]
        Manager = 20,
        [Description("admin")]
        Admin = 30
    }
}