using ChatAppTestAzure.Data;
using ChatAppTestAzure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ChatAppTestAzure.Pages;

public class IndexModel : PageModel
{
    private readonly ChatDbContext _db;

    public IndexModel(ChatDbContext db) => _db = db;

    public List<Message> Messages { get; private set; } = new();

    public async Task OnGetAsync()
    {
        Messages = await _db.Messages
                .OrderByDescending(m => m.Timestamp) // 🟢 найновіші зверху
                .AsNoTracking()
                .ToListAsync();
    }
}
