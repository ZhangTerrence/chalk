using chalk.Server.DTOs;
using chalk.Server.DTOs.Requests;
using chalk.Server.DTOs.Responses;
using chalk.Server.Mappings;
using chalk.Server.Services.Interfaces;
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

    [HttpPost("modules/{moduleId:long}")]
    public async Task<IActionResult> CreateModuleFile([FromRoute] long moduleId, [FromForm] CreateFileRequest request)
    {
        var validationResult = await _createFileRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var module = await _fileService.CreateFileForModule(moduleId, request);
        return Ok(new Response<ModuleDTO>(null, module.ToDTO()));
    }

    [HttpPut("{fileId:long}")]
    public async Task<IActionResult> UpdateModuleFile([FromRoute] long fileId, [FromForm] UpdateFileRequest request)
    {
        var validationResult = await _updateFileRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(new Response<object>(validationResult.GetErrorMessages()));
        }

        var file = await _fileService.UpdateFile(fileId, request);
        return Ok(new Response<FileDTO>(null, file.ToDTO()));
    }
}