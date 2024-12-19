using Explorer.API.Controllers.Authoring.Tour;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Author;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Level = Explorer.Tours.API.Dtos.Level;
using TourStatus = Explorer.Tours.Core.Domain.Tours.TourStatus;
using TransportType = Explorer.Tours.API.Dtos.TransportType;

namespace Explorer.Tours.Tests.Integration.Author;

[Collection("Sequential")]
public class TourCommandTests : BaseToursIntegrationTest
{
    public TourCommandTests(ToursTestFactory factory) : base(factory) { }
    
    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new TourDto
        {
            Name = "Planinarenje",
            AuthorId = 101,
            Description = "Planinarska tura sa vodičem kroz najlepše predele.",
            Level = Level.Advanced,
            Status = Explorer.Tours.API.Dtos.TourStatus.Published,
            LengthInKm = 15.0,
            Price = 120,
            AverageGrade = 4.5,
            Taggs = new List<string> { "Adventure", "Mountain", "Hiking" },
            WeatherRequirements = new WeatherConditionDto
            {
                MinTemperature = 10,
                MaxTemperature = 20,
                SuitableConditions = new List<API.Dtos.WeatherConditionType> { API.Dtos.WeatherConditionType.CLOUDS, API.Dtos.WeatherConditionType.RAIN }
            }
        };


        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(newEntity.Name);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == newEntity.Name);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new TourDto
        {
            Description = "Test"
        };

        // Act
        var result = (ObjectResult)controller.Create(updatedEntity).Result;

        // Assert
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(400);
    }

    [Fact]
    public void Updates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var updatedEntity = new TourDto
        {
            Id=-1,
            Name = "Strasna tura",
            AuthorId = 101,
            Description = "Planinarska tura sa vodičem kroz najlepše predele.",
            Level = Level.Advanced,
            Status = Explorer.Tours.API.Dtos.TourStatus.Published,
            LengthInKm = 15.0,
            /*Price = new PriceDto
            {
                Cost = 120.50,
                Currency = 0
            },*/
            Price = 120,
            AverageGrade = 4.5,
            Taggs = new List<string> { "Adventure", "Mountain", "Hiking" },
            WeatherRequirements = new WeatherConditionDto
            {
                MinTemperature = 0,
                MaxTemperature = 10,
                SuitableConditions = new List<API.Dtos.WeatherConditionType> { API.Dtos.WeatherConditionType.CLEAR, API.Dtos.WeatherConditionType.CLOUDS }
            }
        };

        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(updatedEntity.Id);
        result.Name.ShouldBe(updatedEntity.Name);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == "Strasna tura");
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(updatedEntity.Id);
        storedEntity.Description.ShouldBe(updatedEntity.Description);
    }

    [Fact]
    public void Deletes()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Id == -3);
        storedEntity.ShouldBeNull();
    }

    [Theory]
    [MemberData(nameof(CheckpointAdding))]
    public void AddingCheckpoint(int tourId,CheckpointDto checkpoint,int expectedStatusCode)
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.AddCheckpoint(tourId,checkpoint).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedStatusCode);

        // Assert - Database
        if (result.StatusCode == expectedStatusCode && expectedStatusCode == 200)
        {
            var storedEntity = dbContext.Checkpoint.FirstOrDefault(c => c.Name == checkpoint.Name);
            storedEntity.ShouldNotBeNull();
            storedEntity.Secret.ShouldBe(checkpoint.Secret);
        }
    }

    public static IEnumerable<object[]> CheckpointAdding()
    {
        return new List<object[]>
        {
            new object[]
            {
                -1,
                new CheckpointDto
                {
                    Name = "Novi checkpoint",
                    Description = "Starting checkpoint for the tour.",
                    Latitude = 45.2671,
                    Longitude = 20.8335,
                    ImageUrl = "/images/start-point.jpg",
                    ImageBase64 = "",
                    Secret = "tajna",
                    IsEncounterEssential = false
                },
                200
            },
            new object[]
            {
                -1,
                new CheckpointDto
                {
                    Name = "Lose zadat checkpoint",
                    Description = "Starting checkpoint for the tour.",
                    Latitude = 45.2671,
                    Longitude = 20.8335,
                    ImageUrl = "/images/start-point.jpg",
                    ImageBase64 = "",
                    IsEncounterEssential= false
                },
                400
            },
            new object[]
            {
                -5,
                new CheckpointDto
                {
                    Name = "Los id ture checkpoint",
                    Description = "Starting checkpoint for the tour.",
                    Latitude = 45.2671,
                    Longitude = 20.8335,
                    ImageUrl = "/images/start-point.jpg",
                    ImageBase64 = "",
                    Secret = "tajna",
                    IsEncounterEssential = false
                },
                404
            }
        };
    }

    [Theory]
    [InlineData(-1,25.0,200)]
    [InlineData(-2,-20.0,400)]
    [InlineData(-5,20.0,404)]
    public void UpdatingLength(int tourId, double length, int expectedStatusCode)
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.UpdateLength(tourId,length).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedStatusCode);

        // Assert - Database
        if (result.StatusCode == expectedStatusCode && expectedStatusCode == 200)
        {
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.LengthInKm.ShouldBe(length); ;
        }
    }

    [Theory]
    [MemberData(nameof(TransportDurationsAdding))]
    public void AddingTransportDurations(int tourId, List<TransportDurationDto> transportDurations,int expectedStatusCode)
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.AddTransportDurations(tourId, transportDurations).Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedStatusCode);
    }


    public static IEnumerable<object[]> TransportDurationsAdding()
    {
        return new List<object[]>
        {
            new object[]
            {
                -1,
                new List<TransportDurationDto>
                {
                    new TransportDurationDto{
                        Time=20,
                        TransportType=TransportType.Car
                    },
                    new TransportDurationDto{
                        Time=40,
                        TransportType=TransportType.Walking
                    },
                },
                200
            },
            new object[]
            {
                -1,
                new List<TransportDurationDto>
                {
                    new TransportDurationDto{
                        Time=-10,
                        TransportType=TransportType.Car
                    },
                    new TransportDurationDto{
                        Time=40,
                        TransportType=TransportType.Walking
                    },
                },
                400
            },
            new object[]
            {
                -5,
                new List<TransportDurationDto>
                {
                    new TransportDurationDto{
                        Time=20,
                        TransportType=TransportType.Car
                    },
                    new TransportDurationDto{
                        Time=40,
                        TransportType=TransportType.Walking
                    },
                },
                404
            }
        };
    }

    [Theory]
    [InlineData(-2,200,TourStatus.Archived)]
    [InlineData(-4,404,TourStatus.Published)]
    public void Archives(int tourId,int expectedStatusCode,TourStatus expectedStatus)
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.ChangeStatus(tourId, "Archive").Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedStatusCode);

        // Assert - Database
        if (result.StatusCode == expectedStatusCode && expectedStatusCode == 200)
        {
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedStatus);
        }
    }

    [Theory]
    [InlineData(-11, 200, TourStatus.Published)]
    [InlineData(-12, 400, TourStatus.Draft)]
    [InlineData(-13, 404, TourStatus.Published)]
    public void TourPublishes(int tourId, int expectedStatusCode, TourStatus expectedStatus)
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

        // Act
        var result = (ObjectResult)controller.ChangeStatus(tourId,"Publish").Result;

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(expectedStatusCode);

        // Assert - Database
        if (result.StatusCode == expectedStatusCode && expectedStatusCode == 200)
        {
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedStatus);
        }
    }

    private static TourController CreateController(IServiceScope scope)
    {
        return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>(), scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
    
}
