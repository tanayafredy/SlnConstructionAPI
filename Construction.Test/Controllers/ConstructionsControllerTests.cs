using AppConstructionAPI.Controllers;
using Construction.Application.Services;
using Construction.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Construction.Test.Controllers
{
    public class ConstructionsControllerTests
    {
        private readonly Mock<ConstructionService> _mockConstructionService;
        private readonly ConstructionController _controller;

        public ConstructionsControllerTests()
        {
            _mockConstructionService = new Mock<ConstructionService>(null);
            _controller = new ConstructionController(_mockConstructionService.Object);
        }

        [Fact]
        public async Task PutConstruction_ShouldReturnBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            DateTime dateTime = DateTime.Now.AddYears(1);
            var construction = new ConstructionProject
            {
                ProjectID = 100001,
                ProjectName = "Project 1",
                ProjectLocation = "Location 1",
                ProjectStage = "Concept",
                ProjectCategory = "Education",
                ProjectConstructionStartDate = dateTime,
                ProjectDetail = "Detail 1",
                ProjectCreatorID = "ID 1",
            };

            // Act
            var result = await _controller.PutConstruction(1, construction);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

    }
}
