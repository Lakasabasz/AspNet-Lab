using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Podstawy_widoków.Models;

namespace Podstawy_widoków.Services;

public class ApplicationDatabaseInMemory: IdentityDbContext
{
    public DbSet<Localization> Localizations { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }

    public ApplicationDatabaseInMemory(DbContextOptions<ApplicationDatabaseInMemory> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var localizations = new[]
        {
            new Localization()
            {
                Name = "Kaczka", Id = Guid.NewGuid(), City = "Bielsko-Biała", Street = "Willowa", StreetNumber = 2,
                ImageUrl = "", DayOfWeekBegin = (int)DayOfWeek.Monday, DayOfWeekEnd = (int)DayOfWeek.Friday,
                HourBegin = 8, HourEnd = 21
            },
            new Localization()
            {
                Name = "Taczka", Id = Guid.NewGuid(), City = "Bielsko-Biała", Street = "Czołgistów", StreetNumber = 42,
                ImageUrl = "", DayOfWeekBegin = (int)DayOfWeek.Monday, DayOfWeekEnd = (int)DayOfWeek.Friday,
                HourBegin = 8, HourEnd = 21
            },
        };
        var types = new[] { new VehicleType() { Name = "Rower" } };
        var vehicles = new[]
        {
            new Vehicle()
            {
                Description = @"We wtorek zbudziłem się o tej porze bezdusznej i nikłej, kiedy właściwie 
                            noc się już skończyła, a świt nie zdążył jeszcze zacząć się na dobre. Zbudzony 
                            nagle, chciałem pędzić taksówką na dworzec, zdawało mi się bowiem, że 
                            wyjeżdżam — dopiero w następnej minucie z biedą rozeznałem, że pociąg dla 
                            mnie na dworcu nie stoi, nie wybiła żadna godzina.",
                Id = Guid.NewGuid(), Name = "Rower A", TypeId = types[0].Name, Occupied = false, CurrentLocalizationId = localizations[0].Id,
                ImageUrl = "/img/12162_1.jpg"
            },
            new Vehicle()
            {
                Description =
                    @"Dzieje męczeńskiej Polski obejmują wiele pokoleń i niezliczone mnóstwo ofiar; krwawe sceny toczą się po wszystkich stronach ziemi naszej i po obcych krajach. — Poema, które dziś ogłaszamy, zawiera kilka drobnych rysów tego ogromnego obrazu, kilka wypadków z czasu prześladowania podniesionego przez Imperatora Aleksandra.",
                Id = Guid.NewGuid(), Name = "Rower B", TypeId = types[0].Name, Occupied = false, CurrentLocalizationId = localizations[0].Id,
                ImageUrl = "/img/12162_1.jpg"
            },
            new Vehicle()
            {
                Description =
                    @"Często przychodzili do nas dawni koledzy oగca: pan Domański, także woźny, ale z Ko- Małżeństwo
                    misగi Skarbu⁶⁷, i pan Raczek, który na Dunaగu⁶⁸ miał stragan z zieleniną. Prości to byli
                    ludzie (nawet pan Domański trochę lubił anyżówkę), ale roztropni politycy. Wszyscy,
                    nie wyłączaగąc ciotki, twierdzili గak naగbardzieగ stanowczo, że choć Napoleon I umarł
                    w niewoli⁶⁹, ród Bonapartych గeszcze wypłynie. Po pierwszym Napoleonie znaగdzie się
                    గakiś drugi, a gdyby i ten źle skończył, przyగdzie następny, dopóki గeden po drugim nie
                    uporządkuగą świata.",
                Id = Guid.NewGuid(), Name = "Rower C", TypeId = types[0].Name, Occupied = true, CurrentLocalizationId = localizations[1].Id,
                ImageUrl = "/img/12162_1.jpg"
            },
            new Vehicle()
            {
                Description = @"Obydwoగe państwo Borowiczowie postanowili odwieźć గedynaka na mieగsce. Zaprzę-
                                żono konie do malowanych i kutych sanek, główne siedzenie wysłano barwnym, strzy-
                                żonym dywanem, który zazwyczaగ wisiał nad łóżkiem pani, i około pierwszeగ z południa
                                wśród powszechnego płaczu wyruszono.",
                Id = Guid.NewGuid(), Name = "Rower D", TypeId = types[0].Name, Occupied = false, CurrentLocalizationId = localizations[1].Id,
                ImageUrl = "/img/12162_1.jpg"
            }
        };
        var roles = new[]
        {
            new IdentityRole() { Name = "Admin", Id = Guid.NewGuid().ToString(), NormalizedName = "ADMIN"},
            new IdentityRole() { Name = "Operator", Id = Guid.NewGuid().ToString(), NormalizedName = "OPERATOR"},
            new IdentityRole() { Name = "User", Id = Guid.NewGuid().ToString(), NormalizedName = "USER"},
        };
        modelBuilder.Entity<VehicleType>().HasData(types);
        modelBuilder.Entity<Localization>().HasData(localizations);
        modelBuilder.Entity<Vehicle>().HasData(vehicles);
        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}