namespace App1.Models1;

public class Participant
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public List<EventParticipant> EventParticipants { get; set; } = new();
}
