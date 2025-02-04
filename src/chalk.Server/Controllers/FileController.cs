using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Entities;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
using chalk.Server.Shared;
using chalk.Server.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chalk.Server.Controllers;

[ApiController]
[Route("/api/files"), Authorize]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    private readonly IValidator<CreateFileRequest> _createFileRequestValidator;
    private readonly IValidator<UpdateFileRequest> _updateFileRequestValidator;

    public FileController(
        IFileService fileService,
        IValidator<CreateFileRequest> createFileRequestValidator,
        IValidator<UpdateFileRequest> updateFileRequestValidator
    )
    {
        _fileService = fileService;
        _createFileRequestValidator = createFileRequestValidator;
        _updateFileRequestValidator = updateFileRequestValidator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateFile([FromForm] CreateFileRequest request)
    {
        var validationResult = await _createFileRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        switch (request.For)
        {
            case For.Module:
                var module = await _fileService.CreateFile<Module>(request);
                return Ok(new Response<ModuleDTO>(null, module.ToDTO()));
            case For.Assignment:
                var assignment = await _fileService.CreateFile<Assignment>(request);
                return Ok(new Response<AssignmentDTO>(null, assignment.ToDTO()));
            default:
                throw new NotImplementedException();
        }
    }

    [HttpPut("{fileId:long}")]
    public async Task<IActionResult> UpdateFile([FromRoute] long fileId, [FromForm] UpdateFileRequest request)
    {
        var validationResult = await _updateFileRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        switch (request.For)
        {
            case For.Module:
                var module = await _fileService.UpdateFile<Module>(fileId, request);
                return Ok(new Response<ModuleDTO>(null, module.ToDTO()));
            case For.Assignment:
                var assignment = await _fileService.UpdateFile<Assignment>(fileId, request);
                return Ok(new Response<AssignmentDTO>(null, assignment.ToDTO()));
            default:
                throw new NotImplementedException();
        }
    }

    [HttpDelete("{fileId:long}")]
    public async Task<IActionResult> DeleteModuleFile([FromRoute] long fileId)
    {
        await _fileService.DeleteFile(fileId);
        return NoContent();
    }
}