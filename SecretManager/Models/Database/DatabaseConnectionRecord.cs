namespace SecretManager.Models.Database
{
    public record DatabaseConnectionRecord
    {
        public required string Server { get; init; }
        public required string UserName { get; init; }
        public required string Password { get; init; }
    }
}
