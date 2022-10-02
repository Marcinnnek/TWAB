using Domain.Interfaces;

namespace Domain.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; }
}