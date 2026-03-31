using Api.Rest.IssueBoard.Data;
using Api.Rest.IssueBoard.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Rest.IssueBoard.Dtos;

namespace Api.Rest.IssueBoard.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuesController : ControllerBase
{
    private readonly IssuesDbContext _context;

    public IssuesController(IssuesDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<IssueDto>>> GetIssues()
    {
        var issues = await _context.Issues
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        return Ok(issues.Select(x => x.ToDto()));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IssueDto>> GetIssue(int id)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue is null)
            return NotFound();
        return Ok(issue.ToDto());
    }

    [HttpPost]
    public async Task<ActionResult<IssueDto>> CreateIssue(CreateIssueDto dto)
    {
        var issue = dto.ToModel();
        _context.Issues.Add(issue);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetIssue), new { id = issue.Id }, issue.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<IssueDto>> UpdateIssue(int id, UpdateIssueDto dto)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue is null)
            return NotFound();
        issue.ApplyUpdate(dto);
        await _context.SaveChangesAsync();
        return Ok(issue.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIssue(int id)
    {
        var issue = await _context.Issues.FindAsync(id);
        if (issue is null)
            return NotFound();
        _context.Issues.Remove(issue);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
