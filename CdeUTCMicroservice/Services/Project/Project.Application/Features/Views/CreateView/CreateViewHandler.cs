
namespace Project.Application.Features.Views.CreateView
{
    public class CreateViewHandler
        (IBaseRepository<Annotation> annotationRepository,
        IBaseRepository<View> viewRepository)
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

            await viewRepository.CommitTransactionAsync(transaction, cancellationToken);

            return new CreateViewResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };



        }
    }
}
