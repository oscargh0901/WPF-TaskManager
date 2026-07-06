using WPF_TaskManager.Models;
using WPF_TaskManager.ViewModels;
using Xunit;

namespace WPF_TaskManager.Tests
{
    public class MainViewModelTests
    {
        [Fact]
        public void Constructor_LoadsExistingTasksFromStorage()
        {
            var storage = new FakeTaskStorageService();
            storage.Tasks.Add(new TaskItem { Title = "Comprar leche" });

            var viewModel = new MainViewModel(storage);

            var task = Assert.Single(viewModel.Tasks);
            Assert.Equal("Comprar leche", task.Title);
        }

        [Fact]
        public void AddTaskCommand_AddsTaskAndClearsInput()
        {
            var viewModel = new MainViewModel(new FakeTaskStorageService())
            {
                NewTaskTitle = "Escribir informe"
            };

            viewModel.AddTaskCommand.Execute(null);

            var task = Assert.Single(viewModel.Tasks);
            Assert.Equal("Escribir informe", task.Title);
            Assert.Equal(string.Empty, viewModel.NewTaskTitle);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void AddTaskCommand_CanExecute_IsFalseForBlankTitle(string title)
        {
            var viewModel = new MainViewModel(new FakeTaskStorageService())
            {
                NewTaskTitle = title
            };

            Assert.False(viewModel.AddTaskCommand.CanExecute(null));
        }

        [Fact]
        public void RemoveTaskCommand_RemovesTaskFromCollection()
        {
            var viewModel = new MainViewModel(new FakeTaskStorageService())
            {
                NewTaskTitle = "Tarea temporal"
            };
            viewModel.AddTaskCommand.Execute(null);
            var task = viewModel.Tasks[0];

            viewModel.RemoveTaskCommand.Execute(task);

            Assert.Empty(viewModel.Tasks);
        }

        [Fact]
        public void AddingTask_PersistsToStorage()
        {
            var storage = new FakeTaskStorageService();
            var viewModel = new MainViewModel(storage)
            {
                NewTaskTitle = "Persistir esto"
            };

            viewModel.AddTaskCommand.Execute(null);

            var task = Assert.Single(storage.Tasks);
            Assert.Equal("Persistir esto", task.Title);
        }

        [Fact]
        public void SummaryText_ReflectsCompletedAndTotalTasks()
        {
            var viewModel = new MainViewModel(new FakeTaskStorageService())
            {
                NewTaskTitle = "Tarea 1"
            };
            viewModel.AddTaskCommand.Execute(null);
            viewModel.Tasks[0].IsCompleted = true;

            Assert.Equal(1, viewModel.CompletedCount);
            Assert.Equal(1, viewModel.TotalCount);
            Assert.Equal("1 de 1 completadas", viewModel.SummaryText);
        }
    }
}
