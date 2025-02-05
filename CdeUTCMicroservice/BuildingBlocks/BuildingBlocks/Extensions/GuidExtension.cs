namespace BuildingBlocks.Extensions
{
    public static class GuidExtensions
    {
        public static Guid Sequence(this Guid guid)
        {
            byte[] guidBytes = guid.ToByteArray();
            byte[] timestampBytes = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

            // Sắp xếp 6 byte cuối của GUID theo timestamp để tạo thứ tự
            Array.Copy(timestampBytes, 0, guidBytes, guidBytes.Length - 6, 6);

            return new Guid(guidBytes);
        }
    }
}
