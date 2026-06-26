namespace TextilSalas.Application.Services;
public static class OrderNumberGenerator
{
    public static string NewOrderNumber() => $"TS-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}";
}
