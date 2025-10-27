using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using SoftPlan.TaskManager.Application.DTOs;
using SoftPlan.TaskManager.Application.Services;
using SoftPlan.TaskManager.Domain.Entities;
using SoftPlan.TaskManager.Domain.Interfaces;
using Xunit;

namespace SoftPlan.TaskManager.Tests.Services
{
    public class TaskServiceTests
    {
        private readonly Mock<ITaskRepository> _repositoryMock;
        private readonly TaskService _service;

        public TaskServiceTests()
        {
            _repositoryMock = new Mock<ITaskRepository>();
            _service = new TaskService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateTask_WhenValid()
        {
            // Arrange
            var dto = new CreateTaskDto(
                "Nova Tarefa",
                "Descrição da tarefa",
                DateTime.UtcNow.AddDays(1),
                Guid.NewGuid()
            );

            _repositoryMock.Setup(r => r.AddAsync(It.IsAny<TaskItem>()))
                           .Returns(Task.FromResult(It.IsAny<TaskItem>()));

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Title, result.Title);
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<TaskItem>()), Times.Once);
        }

        [Fact]
        public async Task GetByUserAsync_ShouldReturnTasks_WhenUserHasTasks()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var tasks = new List<TaskItem>
            {
                new TaskItem("Tarefa 1", "Descrição", DateTime.UtcNow.AddDays(1), userId),
                new TaskItem("Tarefa 2", "Descrição", DateTime.UtcNow.AddDays(2), userId)
            };

            _repositoryMock.Setup(r => r.GetByUserAsync(userId))
                           .ReturnsAsync(tasks);

            // Act
            var result = await _service.GetByUserAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CompleteAsync_ShouldMarkTaskAsCompleted_WhenExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var task = new TaskItem("Teste", "Descrição", DateTime.UtcNow.AddDays(1), userId);

            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(task);

            // Act
            await _service.CompleteTaskAsync(task.Id);

            // Assert
            Assert.True(task.IsCompleted);
            _repositoryMock.Verify(r => r.UpdateAsync(task), Times.Once);
        }

        [Fact]
        public async Task CompleteAsync_ShouldThrow_WhenTaskNotFound()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync((TaskItem?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.CompleteTaskAsync(Guid.NewGuid()));
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveTask_WhenExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var task = new TaskItem("Teste", "Descrição", DateTime.UtcNow.AddDays(1), userId);

            _repositoryMock.Setup(r => r.GetByIdAsync(task.Id))
                           .ReturnsAsync(task);

            // Act
            await _service.DeleteTaskAsync(task.Id);

            // Assert
            await _service.DeleteTaskAsync(task.Id);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrow_WhenTaskNotFound()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync((TaskItem?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteTaskAsync(Guid.NewGuid()));
        }
    }
}
