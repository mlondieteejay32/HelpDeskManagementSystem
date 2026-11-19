using System;
using HelpDesk.Core.Enums;
using HelpDesk.Core.Entities;
using Xunit;

namespace HelpDesk.Tests.Entities;

public class TicketTests
{
    [Fact]
    public void Constructor_ShouldSetTicketDetails()
    {
        // Arrange
        string title = "Cannot access email";
        string description = "I can't log into my company email account.";
        string submittedByUserId = "USER001";

        // Act
        Ticket ticket = new Ticket(title, description, submittedByUserId);

        // Assert
        Assert.Equal(title, ticket.Title);
        Assert.Equal(description, ticket.Description);
        Assert.Equal(submittedByUserId, ticket.SubmittedByUserId);
    }

    [Fact]
    public void Constructor_ShouldSetDefaultValues()
    {
        //CREATE ANOTHER TICKET
        Ticket ticket = new Ticket(
            "Cannot access email", 
            "I can't log into my company email account.", 
            "USER001"
        );

        //VERIFY THAT ITS STATUS IS OPEN, PRIORITY IS MEDIUM, ASSIGNTOUSERID IS NULL, AND CREATEDAT IS NULL
        Assert.Equal(TicketStatus.Open, ticket.Status);
        Assert.Equal(TicketPriority.Medium, ticket.Priority);
        Assert.Null(ticket.AssignedToUserId);
        Assert.Null(ticket.ResolvedAt);
        Assert.NotEqual(default(DateTime), ticket.CreatedAt);
    }

    [Fact]
    public void Constructor_ShouldSetCreationTimestamp()
    {
        //CAPTURE THE CURRENT UTC TIME
        DateTime beforeCreation = DateTime.UtcNow;

        //CREATE A NEW TICKET
        Ticket ticket = new Ticket
        (
            "Cannot access email", 
            "I can't log into my company email account.", 
            "USER001"
        );

        //CAPTURE THE CURRENT UTC TIME AFTER CREATION
        DateTime afterCreation = DateTime.UtcNow;

        //VERIFY THAT CREATEDAT IS BETWEEN BEFORECREATION AND AFTERCREATION
        Assert.InRange(ticket.CreatedAt, beforeCreation, afterCreation);
    }

    //TEST CASES FOR INVALID INPUTS
    [Fact]
    public void Constructor_ShouldRejectEmptyWhiteSpaceTitle()
    {
        //ATTEMPT TO CREATE A TICKET WITH AN EMPTY TITLE
        //This is a useful Xunit method that checks if the specified action throws an exception of the specified type.
        Assert.Throws<ArgumentException>(() => new Ticket("", "Description", "USER001"));           
        Assert.Throws<ArgumentException>(() => new Ticket("   ", "Description", "USER001"));
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyWhiteSpaceDescription()
    {
        //ATTEMPT TO CREATE A TICKET WITH AN EMPTY DESCRIPTION
        Assert.Throws<ArgumentException>(() => new Ticket("Title", "", "USER001"));
        Assert.Throws<ArgumentException>(() => new Ticket("Title", "   ", "USER001"));
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyWhiteSpaceSubmittedByUserId()
    {
        //ATTEMPT TO CREATE A TICKET WITH AN EMPTY SUBMITTEDBYUSERID
        Assert.Throws<ArgumentException>(() => new Ticket("Title", "Description", ""));
        Assert.Throws<ArgumentException>(() => new Ticket("Title", "Description", "   "));
    }

    [Fact]
    public void Constructor_ShouldRejectTitleExceedingMaxLength()
    {
        //ATTEMPT TO CREATE A TICKET WITH A TITLE EXCEEDING 100 CHARACTERS
        string longTitle = new string('A', 101);
        Assert.Throws<ArgumentException>(() => new Ticket(longTitle, "Description", "USER001"));
    }

    [Fact]
    public void Constructor_ShouldRejectDescriptionExceedingMaxLength()
    {
        //ATTEMPT TO CREATE A TICKET WITH A DESCRIPTION EXCEEDING 2000 CHARACTERS
        string longDescription = new string('B', 2001);
        Assert.Throws<ArgumentException>(() => new Ticket("Title", longDescription, "USER001"));
    }
}