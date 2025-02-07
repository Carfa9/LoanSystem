// app.MapGet("users/{id}", async (LoanDbContext db, int id) =>
// {
//     //Hämta användaren med id från databasen
//     var user = await db.Users.FindAsync(id);
//     //Om användaren inte finns, skicka 404 NOT FOUND
//     if (user == null)
//     {
//         return Results.NotFound();
//     }
//     //Annars skicka tillbaka användaren
//     return Results.Ok(user);
// });

// app.MapPost("/users", async (LoanDbContext db, User user) =>
// {
//     //Lägga till user till databasen
//     db.Users.Add(user);
//     //Spara databasen
//     await db.SaveChangesAsync();
//     //Skicka tillbaka ett svar till klienten -> 201 CREATED
//     return Results.Created($"/users/{user.Id}", user);
// });