﻿namespace Project.Application.Features.Storage.UpdateFolder
{
    public class UpdateFolderRequest : ICommand<UpdateFolderResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public int? ProjectId { get; set; }
        public List<int> TagIds { get; set; } = new List<int> { };
    }
}
