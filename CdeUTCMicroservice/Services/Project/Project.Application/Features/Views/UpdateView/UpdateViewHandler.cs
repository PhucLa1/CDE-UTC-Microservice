

using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Project.Application.Features.Views.UpdateView
{
    public class UpdateViewHandler(
        IBaseRepository<View> viewRepository,
        IBaseRepository<ViewTag> viewTagRepository,
        IBaseRepository<UserProject> userProjectRepository,
        IPublishEndpoint publishEndpoint)
        : ICommandHandler<UpdateViewRequest, UpdateViewResponse>
    {
        public async Task<UpdateViewResponse> Handle(UpdateViewRequest request, CancellationToken cancellationToken)
        {
            var currentUserId = userProjectRepository.GetCurrentId();

            var userProject = await userProjectRepository.GetAllQueryAble()
               .FirstOrDefaultAsync(e => e.UserId == currentUserId && e.ProjectId == request.ProjectId);

            var view = await viewRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id);

            if (userProject is null || view is null)
                throw new NotFoundException(Message.NOT_FOUND);


            //Không phải admin và cũng không phải người tạo tệp
            if (userProject.Role is not Role.Admin && view.CreatedBy != currentUserId)
                throw new ForbiddenException(Message.FORBIDDEN_CHANGE);

            var viewTags = await viewTagRepository.GetAllQueryAble()
                .Where(e => e.ViewId == request.Id)
                .ToListAsync(cancellationToken);

            view.Name = request.Name;
            view.Description = request.Description;

            viewRepository.Update(view);

            var existingTags = await viewTagRepository.GetAllQueryAble()
                .Where(e => e.ViewId == request.Id)
                .ToListAsync(cancellationToken);

            // Tag hiện tại
            var existingTagIds = existingTags.Select(e => e.TagId!.Value).ToHashSet();

            // Tag mới từ request
            var newTagIds = request.TagIds.ToHashSet();

            // Tìm tag cần thêm(Lấy những id không thuộc trong những tag id mới)
            var tagsToAdd = newTagIds.Except(existingTagIds)
                .Select(tagId => new ViewTag { TagId = tagId, ViewId = request.Id })
                .ToList();
            // Tìm tag cần xóa
            var tagsToRemove = existingTags.Where(e => !newTagIds.Contains(e.TagId!.Value)).ToList();

            // Xóa và thêm chỉ khi cần thiết
            if (tagsToRemove.Any())
                viewTagRepository.RemoveRange(tagsToRemove);
            if (tagsToAdd.Any())
                await viewTagRepository.AddRangeAsync(tagsToAdd, cancellationToken);

            await viewTagRepository.SaveChangeAsync(cancellationToken);

            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = view.Id,
                Content = "Sửa đổi góc nhín tên " + view.Name,
                TypeActivity = TypeActivity.View,
                ProjectId = request.ProjectId.Value,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new UpdateViewResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
