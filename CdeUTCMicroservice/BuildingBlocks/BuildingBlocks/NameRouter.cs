namespace BuildingBlocks
{
    public static class NameRouter
    {
        #region AuthService
        #region Auth
        public const string AUTH_ROUTER = "auth";

        public const string CHANGE_INFO = "change-info";
        public const string GET_INFO = "get-info";
        public const string SEND_EMAIL_VERIFY = "send-email-verify";
        public const string LOGIN = "login";
        public const string VERIFY_CODE = "verify-code";
        public const string SIGN_UP = "sign-up";
        public const string CHANGE_PASSWORD = "change-password";
        #endregion


        #region JobTitles
        public const string JOB_TITLES_ROUTER = "job-titles";
        #endregion


        #region Provinces
        public const string PROVINCES_ROUTER = "provinces";

        public const string CRAWL_PROVINCES = "crawl";
        #endregion


        #endregion



        #region Project
        #region Project
        public const string PROJECT_ROUTER = "projects";
        public const string UNIT = "unit";
        public const string TAG = "tag";
        public const string TYPE = "type";
        public const string STATUS = "status";
        public const string PRIORITY = "priority";
        public const string GET_ROLE = "role";
        public const string PERMISSION = "permission";

        #region Team
        public const string TEAM_ROUTER = "team";
        public const string LEAVE_PROJECT = "leave-project";
        public const string INVITE_USER = "invite-user";
        public const string CHANGE_ROLE = "change-role";
        public const string KICK_USER = "kick-user";
        public const string APPROVE_INVITE = "approve-invite";
        #endregion

        #region Team
        public const string GROUP_ROUTER = "group";
        public const string USERS = "user";
        #endregion

        #region Folder
        public const string FOLDER_ROUTER = "folder";
        #endregion


        #endregion

        #endregion
    }
}
