using Construction.Application.Interfaces;
using Construction.Application.Services;
using Construction.Domain.Entities;
using FluentAssertions;
using Moq;

namespace Construction.Test.Services
{
    public class ConstructionServiceTests
    {
        private readonly Mock<IConstructionRepository> _mockConstructionRepository;
        private readonly ConstructionService _constructionService;

        public ConstructionServiceTests()
        {
            _mockConstructionRepository = new Mock<IConstructionRepository>();
            _constructionService = new ConstructionService(_mockConstructionRepository.Object);
        }

        [Fact]
        public async Task GetAllConstructionsAsync_ShouldReturnListOfConstructions()
        {
            // Arrange
            DateTime dateTime = DateTime.Now.AddYears(1);
            var constructions = new List<ConstructionProject>
            {
                new ConstructionProject {
                    ProjectID = 100000,
                    ProjectName = "Project 0",
                    ProjectLocation = "Location 0",
                    ProjectStage = "Concept",
                    ProjectCategory = "Education",
                    ProjectConstructionStartDate = dateTime,
                    ProjectDetail = "Detail 0",
                    ProjectCreatorID = "ID 0",
                },
                new ConstructionProject {
                    ProjectID = 100001,
                    ProjectName = "Project 1",
                    ProjectLocation = "Location 1",
                    ProjectStage = "Concept",
                    ProjectCategory = "Education",
                    ProjectConstructionStartDate = dateTime,
                    ProjectDetail = "Detail 1",
                    ProjectCreatorID = "ID 1",
                }
            };
            _mockConstructionRepository.Setup(repo => repo.GetAllAsync(null, null, true, 1, 10))
                                  .ReturnsAsync(constructions);

            // Act
            var result = await _constructionService.GetAllConstructionsAsync();

            // Assert
            result.Should().HaveCount(2);
            result.Should().ContainSingle(s => s.ProjectName == "Project 0");
            result.Should().ContainSingle(s => s.ProjectName == "Project 1");
        }

        [Fact]
        public async Task GetConstructionByIdAsync_ShouldReturnConstruction_WhenConstructionExists()
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
            _mockConstructionRepository.Setup(repo => repo.GetByIdAsync(100001)).ReturnsAsync(construction);

            // Act
            var result = await _constructionService.GetConstructionByIdAsync(100001);

            // Assert
            result.Should().NotBeNull();
            result.ProjectName.Should().Be("Project 1");
        }

        [Fact]
        public async Task GetConstructionByIdAsync_ShouldReturnNull_WhenConstructionDoesNotExist()
        {
            // Arrange
            _mockConstructionRepository.Setup(repo => repo.GetByIdAsync(100001)).ReturnsAsync((ConstructionProject)null);

            // Act
            var result = await _constructionService.GetConstructionByIdAsync(100001);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddConstructionAsync_ShouldInvokeRepositoryOnce()
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
            await _constructionService.AddConstructionAsync(construction);

            // Assert
            _mockConstructionRepository.Verify(repo => repo.AddAsync(construction), Times.Once);
        }

        [Fact]
        public async Task UpdateConstructionAsync_ShouldInvokeRepositoryOnce()
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
            await _constructionService.UpdateConstructionAsync(construction);

            // Assert
            _mockConstructionRepository.Verify(repo => repo.UpdateAsync(construction), Times.Once);
        }

        [Fact]
        public async Task DeleteConstructionAsync_ShouldInvokeRepositoryOnce()
        {
            // Act
            await _constructionService.DeleteConstructionAsync(100001);

            // Assert
            _mockConstructionRepository.Verify(repo => repo.DeleteAsync(100001), Times.Once);
        }
    }
}
