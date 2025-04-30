
using Project.Application.Hubs;

namespace Project.Application.Features.Views.AddAnnotation
{
    public class AddAnnotationHandler
        (IBaseRepository<Annotation> annotationRepository,
        IAnnotationHub annotationHub)
        : ICommandHandler<AddAnnotationRequest, AddAnnotationResponse>
    {
        public async Task<AddAnnotationResponse> Handle(AddAnnotationRequest request, CancellationToken cancellationToken)
        {
            var annotation = new Annotation()
            {
                ViewId = request.ViewId,
                InkString = request.InkString,
                Action = request.Action
            };
            await annotationHub.SendAnnotationToChannel(request.ViewId, request.InkString, (int)request.Action);

            //Lưu vào db
            await annotationRepository.AddAsync(annotation, cancellationToken);
            await annotationRepository.SaveChangeAsync(cancellationToken);

            return new AddAnnotationResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };

        }
    }
}
