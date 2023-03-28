using API.database;
using API;

using Microsoft.AspNetCore.Cors;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("OpenPolicy",
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("OpenPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthorization();



app.MapControllers();

app.Run();


// RemoveSong.DropSongTable();
// SaveSong.CreateSongTable();

// // DateTime now = new DateTime();
// Song s = new Song(){Title = "Dinosaur", Artist = "Hank Williams Jr.", DateAdded = DateTime.Now, Favorited = false, Deleted = false};
// Song song = new Song(){Title = "American Way", Artist = "Hank Williams Jr.", DateAdded = DateTime.Now, Favorited = false, Deleted = false};
// s.Save.CreateSong(s);
// song.Save.CreateSong(song);
