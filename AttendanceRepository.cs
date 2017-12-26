using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = _context;
        }

        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .ToList();
        }
    }
}
