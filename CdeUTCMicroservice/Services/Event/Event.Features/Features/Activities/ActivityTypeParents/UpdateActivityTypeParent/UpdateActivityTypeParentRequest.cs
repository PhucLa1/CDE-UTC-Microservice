﻿using BuildingBlocks.CQRS;

namespace Event.Features.Features.Activities.ActivityTypeParents.UpdateActivityTypeParent
{
    public class UpdateActivityTypeParentRequest : ICommand<UpdateActivityTypeParentResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
