using Microsoft.EntityFrameworkCore;
using RetroVHSRental.Data;
using RetroVHSRental.Models;

namespace RetroVHSRental.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext context;

        public StaffRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Staff staff)
        {
            context.Staff.Add(staff);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await context.Staff.Where(s => s.StoreId==1).ToListAsync();
        }

        public async Task<Staff> GetByIdAsync(byte id)
        {
            return await context.Staff.FirstOrDefaultAsync(s => s.StaffId == id);
        }

        public async Task RemoveAsync(Staff staff)
        {
            context.Staff.Remove(staff);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Staff staff)
        {
            context.Staff.Update(staff);
            await context.SaveChangesAsync();
        }
    }
}
