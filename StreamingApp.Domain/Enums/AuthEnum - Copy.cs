namespace StreamingApp.Domain.Enums;

public enum AppAuthEnum
{
    Admin = 1, // Do anyting
    SuperMod = 2, // Limeted edit writes
    Mod = 3, // More Limited edit writes
    VIP = 4, // NO edit writes
    user = 5, // No Access
}
