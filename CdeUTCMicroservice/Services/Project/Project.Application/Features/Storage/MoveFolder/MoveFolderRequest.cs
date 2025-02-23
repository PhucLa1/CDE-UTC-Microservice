namespace Project.Application.Features.Storage.MoveFolder
{
    public class MoveFolderRequest : ICommand<MoveFolderResponse>
    {

        //Id va parent id khong duoc giong nhau
        public int Id { get; set; }
        public int ParentId { get; set; }
    }
}
