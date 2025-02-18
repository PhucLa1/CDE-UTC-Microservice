namespace Event.Core.Enums
{
    public enum ActivityTypeCategory
    {
        // User Management Activities (1 - 30)
        InviteUser = 1,
        RemoveUser = 2,
        LeaveProject = 3,
        JoinProject = 4,
        AddUserInGroup = 5,
        CreateNewGroup = 6,
        RemoveUserInGroup = 7,
        UpdateGroup = 8,
        DeleteGroup = 9,
        ChangeRole = 10,

        // View Activities (31 - 60)
        AddView = 31,
        DeleteView = 32,
        AssignView = 33,

        // ToDo Activities (61 - 90)
        AddTodo = 61,
        EditTodo = 62,
        DeleteTodo = 63,
        AddFileToTodo = 64,
        RemoveFileFromTodo = 65,
        AssignTodo = 66,

        // BCF Topic Activities (91 - 120)
        AddBcfTopic = 91,
        EditBcfTopic = 92,
        DeleteBcfTopic = 93,
        AddFileToBcfTopic = 94,
        RemoveFileFromBcfTopic = 95,
        AssignBcfTopic = 96,

        // Comment Activities (121 - 150)
        AddComment = 121,
        EditComment = 122,
        DeleteComment = 123,

        // Other Activities (151 - 180)
        UpdateProject = 151,
        RenameProject = 152,
        CreateProject = 153
    }
}
