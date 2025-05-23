﻿

namespace Project.Application.Features.Tags.UpdateTag
{
    public class UpdateTagRequest : ICommand<UpdateTagResponse>
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Id { get; set; }
    }
}
