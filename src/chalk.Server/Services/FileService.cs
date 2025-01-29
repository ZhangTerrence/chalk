using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Shared;
using Microsoft.EntityFrameworkCore;

namespace chalk.Server.Services;

public class FileService : IFileService
{
    private readonly DatabaseContext _context;
    private readonly ICloudService _cloudService;

    public FileService(DatabaseContext context, ICloudService cloudService)
    {
        _context = context;
        _cloudService = cloudService;
    }

    public async Task<T> CreateFile<T>(CreateFileRequest request)
    {
        switch (request.For)
        {
            case For.Module:
            {
                var module = await _context.Modules
                    .Include(e => e.Files)
                    .FirstOrDefaultAsync(e => e.Id == request.EntityId);
                if (module is null)
                {
                    throw new ServiceException("Module not found.", StatusCodes.Status404NotFound);
                }

                var course = await _context.Courses.FindAsync(module.CourseId);
                if (course is null)
                {
                    throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
                }

                var url = await _cloudService.UploadAsync(Guid.NewGuid().ToString(), request.File);
                var file = request.ToEntity(url);
                module.Files.Add(file);
                module.UpdatedDate = DateTime.UtcNow;
                course.UpdatedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return (T)Convert.ChangeType(module, typeof(T));
            }
            default:
                throw new NotImplementedException();
        }
    }

    public async Task<T> UpdateFile<T>(long fileId, UpdateFileRequest request)
    {
        switch (request.For)
        {
            case For.Module:
            {
                var module = await _context.Modules
                    .Include(e => e.Files)
                    .FirstOrDefaultAsync(e => e.Id == request.EntityId);
                if (module is null)
                {
                    throw new ServiceException("Module not found.", StatusCodes.Status404NotFound);
                }

                var course = await _context.Courses.FindAsync(module.CourseId);
                if (course is null)
                {
                    throw new ServiceException("Course not found.", StatusCodes.Status404NotFound);
                }

                var file = await _context.Files.FindAsync(fileId);
                if (file is null)
                {
                    throw new ServiceException("File not found.", StatusCodes.Status404NotFound);
                }

                file.Name = request.Name;
                file.Description = request.Description;
                if (request.FileChanged!.Value)
                {
                    await _cloudService.DeleteAsync(file.FileUrl);
                    file.FileUrl = await _cloudService.UploadAsync(Guid.NewGuid().ToString(), request.NewFile!);
                }
                file.UpdatedDate = DateTime.UtcNow;
                module.UpdatedDate = DateTime.UtcNow;
                course.UpdatedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return (T)Convert.ChangeType(module, typeof(T));
            }
            default:
                throw new NotImplementedException();
        }
    }

    public async Task DeleteFile(long fileId)
    {
        var file = await _context.Files.FindAsync(fileId);
        if (file is null)
        {
            throw new ServiceException("File not found.", StatusCodes.Status404NotFound);
        }

        await _cloudService.DeleteAsync(file.FileUrl);
        _context.Files.Remove(file);

        await _context.SaveChangesAsync();
    }
}