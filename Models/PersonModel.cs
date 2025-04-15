using Microsoft.AspNetCore.Mvc;

namespace MinhaApi.Models;

public class PersonModel
{
    public PersonModel(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; init; }
    public string Name { get; set; }

    public void ChangeName(string name)
    {
        Name = name;
        
    }

    public void SetInactive()
    {
        Name = "desativado";
    }


}