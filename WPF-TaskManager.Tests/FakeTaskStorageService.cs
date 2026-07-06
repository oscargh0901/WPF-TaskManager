using System.Collections.Generic;
using System.Linq;
using WPF_TaskManager.Models;
using WPF_TaskManager.Services;

namespace WPF_TaskManager.Tests
{
    public class FakeTaskStorageService : ITaskStorageService
    {
        public List<TaskItem> Tasks { get; set; } = new();

        public int SaveCallCount { get; private set; }

        public List<TaskItem> Load() => Tasks;

        public void Save(IEnumerable<TaskItem> tasks)
        {
            Tasks = tasks.ToList();
            SaveCallCount++;
        }
    }
}
