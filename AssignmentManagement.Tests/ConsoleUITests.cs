using Xunit;
using Moq;
using AssignmentManagement.Core;
using AssignmentManagement.UI;
using System.Collections.Generic;

namespace AssignmentManagement.Tests
{
    public class ConsoleUITests
    {
        [Fact]
        public void AddAssignment_CallsServiceWithAssignmentObject()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var newAssignment = new Assignment("Lab 4", "Unit testing with Moq");
            var consoleUI = new ConsoleUI(mockService.Object);

            // Act
            mockService.Object.AddAssignment(newAssignment);

            // Assert
            mockService.Verify(s => s.AddAssignment(newAssignment), Times.Once);
        }

        [Fact]
        public void DeleteAssignment_ReturnsTrue_WhenAssignmentExists()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var titleToDelete = "Week 4 Task";
            mockService.Setup(s => s.DeleteAssignment(titleToDelete)).Returns(true);

            var consoleUI = new ConsoleUI(mockService.Object);

            // Act
            var result = mockService.Object.DeleteAssignment(titleToDelete);

            // Assert
            Assert.True(result);
            mockService.Verify(s => s.DeleteAssignment(titleToDelete), Times.Once);
        }

        [Fact]
        public void FindByTitle_ReturnsAssignment_WhenFound()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var expected = new Assignment("Midterm", "Complete by next week");
            mockService.Setup(s => s.FindByTitle("Midterm")).Returns(expected);

            var consoleUI = new ConsoleUI(mockService.Object);

            // Act
            var result = mockService.Object.FindByTitle("Midterm");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Midterm", result.Title);
            mockService.Verify(s => s.FindByTitle("Midterm"), Times.Once);
        }

        [Fact]
        public void UpdateAssignment_ReturnsTrue_WhenSuccessful()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var oldTitle = "Quiz 1";
            var newTitle = "Quiz 1 - Updated";
            var newDesc = "New description";
            mockService.Setup(s => s.UpdateAssignment(oldTitle, newTitle, newDesc)).Returns(true);

            var consoleUI = new ConsoleUI(mockService.Object);

            // Act
            var result = mockService.Object.UpdateAssignment(oldTitle, newTitle, newDesc);

            // Assert
            Assert.True(result);
            mockService.Verify(s => s.UpdateAssignment(oldTitle, newTitle, newDesc), Times.Once);
        }

        [Fact]
        public void MarkAssignmentComplete_ReturnsTrue_WhenAssignmentIsMarked()
        {
            // Arrange
            var mockService = new Mock<IAssignmentService>();
            var title = "Research Paper";
            mockService.Setup(s => s.MarkAssignmentComplete(title)).Returns(true);

            var consoleUI = new ConsoleUI(mockService.Object);

            // Act
            var result = mockService.Object.MarkAssignmentComplete(title);

            // Assert
            Assert.True(result);
            mockService.Verify(s => s.MarkAssignmentComplete(title), Times.Once);
        }
    }
}
