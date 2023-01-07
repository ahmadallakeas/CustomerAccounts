namespace AccountsService.API.Messages
{
    public sealed record TotalChanged
    {
        public string CustomerId { get; set; }
        public double NewBalance { get; set; }

    }
}
