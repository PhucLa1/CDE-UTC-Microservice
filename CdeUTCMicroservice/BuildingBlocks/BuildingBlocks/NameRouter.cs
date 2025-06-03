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
        public const string TARGETS = "targets";
        #endregion

        #region Team
        public const string GROUP_ROUTER = "group";
        public const string USERS = "user";
        #endregion

        #region Folder
        public const string FOLDER_ROUTER = "folder";
        public const string FOLDER_DESTINATION = "destination";
        public const string MOVE_FOLDER = "move-folder";
        #endregion

        #region File
        public const string FILE_ROUTER = "file";
        public const string MOVE_FILE = "move-file";
        #endregion

        #region Storage
        public const string STORAGE_ROUTER = "storage";
        public const string FULL_PATH = "full-path";
        public const string TREE_STORAGE = "tree-storage";
        #endregion

        #region View
        public const string VIEW = "view";
        public const string ADD_ANNOTATION = "add-annotation";
        public const string GET_ANNOTATIONS = "get-annotations";
        #endregion

        #region Folder Comment
        public const string FOLDER_COMMENT_ROUTER = "folder-comment";
        #endregion

        #region View Comment
        public const string VIEW_COMMENT_ROUTER = "view-comment";
        #endregion

        #region File Comment
        public const string FILE_COMMENT_ROUTER = "file-comment";
        #endregion

        #region To do
        public const string TODO_ROUTER = "todo";
        public const string TODO_COMMENT_ROUTER = "todo-comment";
        #endregion

        #region Activity
        public const string ACTIVITY_ROUTER = "activities";
        #endregion

        #region Activity type
        public const string ACTIVITY_TYPE_ROUTER = "activities-type";
        #endregion


        #endregion

        #endregion
    }
}
