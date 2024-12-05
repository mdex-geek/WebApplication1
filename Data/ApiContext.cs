using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data
{
    public class ApiContext: DbContext
    {
        public DbSet<HotelBooking> Bookings { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options):base(options) {

        }
    }
}
