using System.Collections.Generic;
using WPF_TaskManager.Models;

namespace WPF_TaskManager.Services
{
    public interface ITaskStorageService
    {
        List<TaskItem> Load();

        void Save(IEnumerable<TaskItem> tasks);
    }
}
