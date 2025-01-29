using chalk.Server.Configurations;
using chalk.Server.Data;
using chalk.Server.DTOs.Requests;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using File = chalk.Server.Entities.File;

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

    public async Task<Module> CreateFileForModule(long moduleId, CreateFileRequest request)
    {
        var module = await _context.Modules
            .Include(e => e.Files)
            .FirstOrDefaultAsync(e => e.Id == moduleId);
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
        return module;
    }

    public async Task<File> UpdateFile(long fileId, UpdateFileRequest request)
    {
        var file = await _context.Files.FindAsync(fileId);
        if (file is null)
        {
            throw new ServiceException("File not found.", StatusCodes.Status404NotFound);
        }

        file.Name = request.Name;
        file.Description = request.Description;
        if (request.UpdatedFile!.Value)
        {
            file.FileUrl = await _cloudService.UploadAsync(Guid.NewGuid().ToString(), request.File);
        }
        file.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return file;
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