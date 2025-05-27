using AI_LLM.Entities;
using Microsoft.EntityFrameworkCore;

namespace AI_LLM.Data
{
    public class AILLMDBContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AILLMDBContext(DbContextOptions<AILLMDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public AILLMDBContext(DbContextOptions<AILLMDBContext> options) : base(options) { }
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
        public int GetCurrentUserId()
        {
            if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            {
                return 0;
            }
            if (!_httpContextAccessor.HttpContext.Items.ContainsKey("UserId"))
            {
                return 0;
            }
            var userIdObj = _httpContextAccessor.HttpContext.Items["UserId"];
            if (userIdObj == null)
            {
                return 0;
            }
            if (int.TryParse(userIdObj.ToString(), out int userId))
            {
                return userId;
            }
            return 0;
        }
        */
        #endregion

        #region Get token from gateways

        public int GetCurrentUserId()
        {
            var context = _httpContextAccessor.HttpContext;
            var userIdObj = context.Request.Headers["X-UserId"].FirstOrDefault();
            if (userIdObj is null)
                return 0;
            if (int.TryParse(userIdObj.ToString(), out var userId))
            {
                return userId; // Trả về int nếu chuyển đổi thành công
            }

            return 0; // Trả về int mặc định nếu chuyển đổi thất bại
        }
        #endregion

        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

    }
}
