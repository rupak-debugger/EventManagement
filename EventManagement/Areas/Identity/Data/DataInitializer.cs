using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Areas.Identity.Data
{
    public class DataInitializer
    {
        private readonly ApplicationDbContext _context;
        

        public DataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public void Run()
        {
            _context.Database.Migrate();
            
        }
    }
}
