using HelpDesk.Core.Enums;
using System.Diagnostics.CodeAnalysis;

namespace HelpDesk.Core.Entities;

public class Ticket
{

    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string SubmittedByUserId { get; set; }
    public string? AssignedToUserId { get; set; }
    public TicketStatus Status { get; set; }
    public TicketPriority Priority { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ResolvedAt { get; set; }

    //CONSTRUCTOR
    [SetsRequiredMembers]
    public Ticket(string title, string description, string submittedByUserId)
    {
        this.Title = title;
        this.Description = description;
        this.SubmittedByUserId = submittedByUserId;

        CreatedAt = DateTime.UtcNow;
        Status = TicketStatus.Open;
        Priority = TicketPriority.Medium;

        //VALIDATION RULES
        ArgumentException.ThrowIfNullOrWhiteSpace(title);           //CANNOT EXCEED 100 CHARACTERS
        if (title.Length > 100)
        {
            throw new ArgumentException("Title cannot exceed 100 characters.");
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(description);     //CANNOT EXCEED 2000 CHARACTERS
        if (description.Length > 2000)
        {
            throw new ArgumentException("Description cannot exceed 2000 characters.");
        }
        ArgumentException.ThrowIfNullOrWhiteSpace(submittedByUserId);

    }

    
}