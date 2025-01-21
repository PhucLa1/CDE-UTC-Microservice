using Auth.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace Auth.Data.Data
{
    public class AuthDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthDBContext(DbContextOptions<AuthDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options) { }
        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditFields()
        {
            var userId = GetCurrentUserId();

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added))
            {
                if (entry.Entity is IAuditable)
                {
                    var entity = (IAuditable)entry.Entity;
                    entity.UpdatedAt = DateTime.UtcNow;
                    entity.UpdatedBy = userId;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.CreatedBy = userId;
                    }
                }
            }
        }

        #region Get Token in middleware
        /*
        public Guid GetCurrentUserId()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                return Guid.Empty; // Trả về Guid mặc định nếu HttpContext không tồn tại
            }
            if (!_httpContextAccessor.HttpContext.Items.ContainsKey("UserId"))
            {
                return Guid.Empty; // Trả về Guid mặc định nếu không có key "UserId"
            }
            var userIdObj = _httpContextAccessor.HttpContext.Items["UserId"];
            if (userIdObj == null)
            {
                return Guid.Empty; // Trả về Guid mặc định nếu "UserId" không tồn tại
            }
            if (Guid.TryParse(userIdObj.ToString(), out var userId))
            {
                return userId; // Trả về Guid nếu chuyển đổi thành công
            }

            return Guid.Empty; // Trả về Guid mặc định nếu chuyển đổi thất bại
        }
        */
        #endregion

        #region Get token from gateways
        
        public Guid GetCurrentUserId()
        {
            var context = _httpContextAccessor.HttpContext;
            var userIdObj = context.Request.Headers["X-UserId"].FirstOrDefault();
            if (userIdObj is null)
                throw new Exception("Không có request header gửi từ yarp gateway");
            if (Guid.TryParse(userIdObj.ToString(), out var userId))
            {
                return userId; // Trả về Guid nếu chuyển đổi thành công
            }

            return Guid.Empty; // Trả về Guid mặc định nếu chuyển đổi thất bại
        }
        
        #endregion
        public DbSet<City> Cities { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
