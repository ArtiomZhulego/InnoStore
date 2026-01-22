namespace Application.Abstractions.FileAggregate;

public interface IFileService
{
    public UploadFileResponse UploadFile(UploadFileModel request);
}
