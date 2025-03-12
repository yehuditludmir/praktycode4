
using Swashbuckle.AspNetCore.Swagger;
using TodoApi.Models;
using Microsoft.OpenApi.Models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
//הוספת cors
builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//הוספת swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "A simple ASP.NET Core Web API"
    });
});
//חיבור למסד mySql
builder.Services.AddDbContext<Praktykod2Context>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 41)) ,  mysqlOptions => mysqlOptions.EnableRetryOnFailure()));
var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
      options.RoutePrefix = string.Empty;;
    });
    
// }

app.UseCors("MyPolicy");
app.UseHttpsRedirection();

app.MapGet("/", () =>
{
    return "hi - the applicatin";
});
//שליפת כל המשימות
app.MapGet("/items", async (Praktykod2Context db) =>
{
    var users = await db.Items.ToListAsync();
    return Results.Ok(users);
})
;
//הופסת משימה
app.MapPost("/additem", async (Item i,Praktykod2Context p) =>
{

Item it=new Item();
it.Name=i.Name;
it.IsComplete=false;
    var a= p.Items.Add(it);
   return Results.Ok(p.SaveChanges());
});
//מחיקת משימה
app.MapDelete("/del/{iid}", async (int iid,Praktykod2Context p) => 
{
   
    var ddd=p.Items.FirstOrDefault(z=>z.Id==iid);
    if(ddd!=null){
         var d = p.Items.Remove(ddd);
    }
   
    return Results.Ok(p.SaveChanges());

} );
//עדכון משימה
app.MapPut("/updateItem/{id}", async (int Id, Item ic, Praktykod2Context p) =>
{
   
    var todo = await p.Items.FindAsync(Id);

    if (todo is null) return Results.NotFound();

    todo.IsComplete =ic.IsComplete;

    await p.SaveChangesAsync();

    return Results.NoContent();
});


app.Run();
