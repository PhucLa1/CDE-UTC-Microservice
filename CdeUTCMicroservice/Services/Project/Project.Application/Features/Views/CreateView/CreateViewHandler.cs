
using BuildingBlocks.Enums;
using BuildingBlocks.Messaging.Events;
using MassTransit;
using MassTransit.Transports;

namespace Project.Application.Features.Views.CreateView
{
    public class CreateViewHandler
        (IBaseRepository<Annotation> annotationRepository,
        IBaseRepository<View> viewRepository,
        IPublishEndpoint publishEndpoint,
        IBaseRepository<File> fileRepository)
        : ICommandHandler<CreateViewRequest, CreateViewResponse>
    {
        public async Task<CreateViewResponse> Handle(CreateViewRequest request, CancellationToken cancellationToken)
        {
            //Tạo mới một view
            using var transaction = await viewRepository.BeginTransactionAsync(cancellationToken);

            var view = new View()
            {
                FileId = request.FileId,
                Name = request.Name,
                Description = request.Description,
                ViewType = ViewType.View2D
            };

            await viewRepository.AddAsync(view, cancellationToken); //Lưu thông tin view
            await viewRepository.SaveChangeAsync(cancellationToken);

            var annotations = new List<Annotation>();
            var annotationModels = request.AnnotationModels;

            foreach (var annotationModel in annotationModels)
            {
                var annotation = new Annotation()
                {
                    ViewId = view.Id,
                    InkString = annotationModel.InkString,
                    Action = annotationModel.AnnotationAction
                };
                annotations.Add(annotation);
            }
            await annotationRepository.AddRangeAsync(annotations, cancellationToken);
            await annotationRepository.SaveChangeAsync(cancellationToken);

            var file = await fileRepository.GetAllQueryAble().FirstAsync(e => e.Id == request.FileId);

            await viewRepository.CommitTransactionAsync(transaction, cancellationToken);

            //Gửi message sang bên event
            var eventMessage = new CreateActivityEvent()
            {
                Action = "ADD",
                ResourceId = view.Id,
                Content = "Đã tạo mới góc nhín tên " + view.Name,
                TypeActivity = TypeActivity.View,
                ProjectId = file.ProjectId.Value,
            };
            await publishEndpoint.Publish(eventMessage, cancellationToken);

            return new CreateViewResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };



        }
    }
}
