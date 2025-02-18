namespace BuildingBlocks
{
    public static class Message 
    {
        #region Notice Exception
        public static string NOT_FOUND = "Không thể tìm thấy bản ghi";
        public static string CODE_NOT_RIGHT = "Mã code nhập vào không đúng";
        public static string CODE_EXPIRED = "Mã code đã hết hạn dùng";
        public static string FORBIDDEN_CHANGE_PASSWORD = "Không có quyền thay đổi mật khẩu";
        public static string FORBIDDEN_CHANGE = "Không có quyền thay đổi dữ kiện";
        public static string IS_EXIST = "Người dùng đã tồn tại trong rồi";
        public static string MUST_HAVE_ONE_ADMIN = "Một dự án phải có ít nhất 1 admin";
        #endregion

        #region Notice Successfully
        public static string LOGIN_SUCCESSFULLY = "Đăng nhập thành công!";
        public static string SIGNUP_SUCCESSFULLY = "Đăng kí thành công!";
        public static string CREATE_SUCCESSFULLY = "Tạo mới bản ghi thành công";
        public static string UPDATE_SUCCESSFULLY = "Sửa đổi bản ghi thành công";
        public static string GET_SUCCESSFULLY = "Lấy thành công bản ghi";
        public static string DELETE_SUCCESSFULLY = "Xóa thành công bản ghi";
        public static string CRAWL_SUCCESSFULLY = "Cào dữ liệu thành công";
        public static string SEND_CODE_SUCCESSFULLY = "Gửi code qua email thành công";
        public static string CODE_VERIFY_SUCCESSFULLY = "Xác nhận mã code thành công";
        public static string CHANGE_PASSWORD_SUCCESSFULLY = "Thay đổi mật khẩu thành công";
        public static string RESET_SUCCESSFULLY = "Reset dữ liệu thành công";
        public static string INVITATION_USER = "Mời người dùng thành công";
        #endregion


        #region Message Validator
        public static string NAME_NOTEMPTY = "Tên không được để trống";
        #endregion

    }
}
