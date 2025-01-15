using Auth.Data.Entities.Base;
using ServiceStack.DataAnnotations;

namespace Auth.Data.Entities
{
    public class JobTitle : BaseEntity
    {
        private string _uuid = string.Empty;
        [Unique]
        public string Name { get; set; } = string.Empty;
        public string UUID
        {
            get => _uuid = GenerateUUID();
            set => _uuid = value;
        }

        private string GenerateUUID()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return  string.Empty;
            }

            // Lấy ký tự đầu của mỗi từ trong Name
            return string.Join("", Name.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                         .Select(word => word[0]))
                         .ToUpper(); // Chuyển thành chữ hoa
        }
    }
}
