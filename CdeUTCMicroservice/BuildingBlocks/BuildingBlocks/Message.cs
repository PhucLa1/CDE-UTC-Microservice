namespace BuildingBlocks
{
    public static class Message 
    {
        #region Notice Exception
        public static string NOT_FOUND = "Không thể tìm thấy bản ghi";
        #endregion

        #region Notice Successfully
        public static string LOGIN_SUCCESSFULLY = "Đăng nhập thành công!";
        public static string SIGNUP_SUCCESSFULLY = "Đăng kí thành công!";
        public static string CREATE_SUCCESSFULLY = "Tạo mới bản ghi thành công";
        public static string UPDATE_SUCCESSFULLY = "Sửa đổi bản ghi thành công";
        public static string GET_SUCCESSFULLY = "Lấy thành công bản ghi";
        public static string DELETE_SUCCESSFULLY = "Xóa thành công bản ghi";
        #endregion


        #region Message Validator
        public static string NAME_NOTEMPTY = "Tên không được để trống";
        #endregion

    }
}
