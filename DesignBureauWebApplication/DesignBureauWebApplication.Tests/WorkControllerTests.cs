using DesignBureauWebApplication.Controllers;
using DesignBureauWebApplication.Interfaces;
using DesignBureauWebApplication.Models;
using DesignBureauWebApplication.Repository;
using DesignBureauWebApplication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DesignBureauWebApplication.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Index_Returns_A_View_With_All_The_Works()
        {
            // Arrange
            var work = new Mock<IWorkRepository>();
            var workDictionary = new Mock<IWorkDictionaryRepository>();
            var workHistory = new Mock<IWorkHistoryRepository>();
            var project = new Mock<IProjectRepository>();
            var projectHistory = new Mock<IProjectHistoryRepository>();

            work.Setup(repo => repo.GetAll()).ReturnsAsync(GetTestWorks());

            var controller = new WorkController(work.Object, workHistory.Object, project.Object,
                workDictionary.Object, projectHistory.Object);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Work>> (
                viewResult.Model);
            Assert.Equal(GetTestWorks().Count, model.Count());
        }
        private List<Work> GetTestWorks()
        {
            var works = new List<Work>
            {
                new Work { WorkId = 1, WorkDictionaryId = 1, ProjectId = 1, PlanStartDate = DateTime.Now, PlanOverDate = DateTime.Now.AddDays(5) },
                new Work { WorkId = 2, WorkDictionaryId = 2, ProjectId = 1, PlanStartDate = DateTime.Now.AddDays(2), PlanOverDate = DateTime.Now.AddDays(7) }
            };
            return works;
        }

        //[Fact]
        //public void CreateWorkReturnsView()
        //{
        //    var work = new Mock<IWorkRepository>();
        //    var workDictionary = new Mock<IWorkDictionaryRepository>();
        //    var workHistory = new Mock<IWorkHistoryRepository>();
        //    var project = new Mock<IProjectRepository>();
        //    var projectHistory = new Mock<IProjectHistoryRepository>();
        //    var controller = new WorkController(work.Object, workHistory.Object, project.Object,
        //        workDictionary.Object, projectHistory.Object);

        //    var _project = new Project
        //    {
        //        ProjectId = 1,
        //        ProjectTitle = "Test",
        //        PlanStartDate = DateTime.Now,
        //        PlanOverDate = DateTime.Now.AddDays(1)
        //    };

        //    var workVM = new WorkViewModel
        //    {
        //        WorkDictionaryId = 1,
        //        ProjectId = _project.ProjectId,
        //        PlanStartDate = DateTime.Now.AddDays(1),
        //        PlanOverDate = DateTime.Now
        //    };

        //    // Act
        //    var result = controller.Create(workVM);

        //    // Assert
        //    Assert.Null(result);
        //    Assert.True(controller.ModelState.IsValid);
        //    //var errorMessage = controller.ModelState.Values["PlanStartDate"].Errors.SingleOrDefault(e => e.ErrorMessage == "Дата начала не может быть позже даты окончания");
        //    //Assert.NotNull(errorMessage);

        //}

    }
}
