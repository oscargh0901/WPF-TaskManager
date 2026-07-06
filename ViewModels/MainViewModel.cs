using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPF_TaskManager.Models;

namespace WPF_TaskManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _newTaskTitle = string.Empty;

        public ObservableCollection<TaskItem> Tasks { get; } = new();

        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set => SetField(ref _newTaskTitle, value);
        }

        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }

        public MainViewModel()
        {
            AddTaskCommand = new RelayCommand(_ => AddTask(), _ => !string.IsNullOrWhiteSpace(NewTaskTitle));
            RemoveTaskCommand = new RelayCommand(task => RemoveTask(task as TaskItem), task => task is TaskItem);
        }

        private void AddTask()
        {
            Tasks.Add(new TaskItem { Title = NewTaskTitle.Trim() });
            NewTaskTitle = string.Empty;
        }

        private void RemoveTask(TaskItem? task)
        {
            if (task is not null)
                Tasks.Remove(task);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return;

            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
