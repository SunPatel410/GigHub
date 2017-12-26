using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendance { get; private set; }
        public IFollowingRepository Following { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public IApplicationUserRepository Users { get; private set; }
        public INotificationRepository Notifications { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(context);
            Attendance = new AttendanceRepository(context);
            Following = new FollowingRepository(context);
            Genre = new GenreRepository(context);
            Users = new ApplicationUserRepository(context);
            Notifications = new NotificationRepository(context);
            UserNotifications = new UserNotificationRepository(context);


        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
