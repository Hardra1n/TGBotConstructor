public class TelegramAdminRepository
{
    public TelegramAdminRepository()
    {
        Members = new HashSet<string>();
    }

    public string? SecretPhrase { get; set; }

    public HashSet<string> Members { get; set; }

    public async Task AddAdminMember(string? chatId)
    {
        if (chatId != null)
        {
            Members.Add(chatId);
            await Extensions.UpdateTelegramAdminRepository(this);
        }
    }
}