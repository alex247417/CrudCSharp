﻿using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;

namespace MinhaApi.Routes;

public static class PersonRoute
{
    public static void PersonRoutes(this WebApplication app)
    {
        var route = app.MapGroup("person");
        
        route.MapPost("", async (PersonRequest req, PersonContext context) =>
        {
            var person = new PersonModel(req.name);
            await context.AddAsync(person);
            await context.SaveChangesAsync();
        });

        route.MapGet("", async (PersonContext context) =>
        {
             var people= await context.People.ToListAsync();
            return Results.Ok(people);
        });

        route.MapPut("{id:Guid}", async (Guid id, PersonRequest req, PersonContext context) =>
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);
            if(person == null)
                return Results.NotFound();
            person.ChangeName(req.name);
            await context.SaveChangesAsync();
            return Results.Ok(person);
        });

        route.MapDelete("{id:Guid}", async (Guid id ,PersonContext context) =>
        {
            var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);
            if(person == null)
                return Results.NotFound();
            
            person.SetInactive();
            await context.SaveChangesAsync();
            return Results.Ok(person);
        });
    }
}