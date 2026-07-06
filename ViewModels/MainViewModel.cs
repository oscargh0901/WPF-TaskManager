using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPF_TaskManager.Models;
using WPF_TaskManager.Services;

namespace WPF_TaskManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ITaskStorageService _storage;
        private string _newTaskTitle = string.Empty;

        public ObservableCollection<TaskItem> Tasks { get; } = new();

        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set => SetField(ref _newTaskTitle, value);
        }

        public int TotalCount => Tasks.Count;

        public int CompletedCount => Tasks.Count(t => t.IsCompleted);

        public string SummaryText => $"{CompletedCount} de {TotalCount} completadas";

        public ICommand AddTaskCommand { get; }
        public ICommand RemoveTaskCommand { get; }

        public MainViewModel()
            : this(new JsonTaskStorageService())
        {
        }

        public MainViewModel(ITaskStorageService storage)
        {
            _storage = storage;

            AddTaskCommand = new RelayCommand(_ => AddTask(), _ => !string.IsNullOrWhiteSpace(NewTaskTitle));
            RemoveTaskCommand = new RelayCommand(task => RemoveTask(task as TaskItem), task => task is TaskItem);

            foreach (var task in _storage.Load())
            {
                Tasks.Add(task);
                task.PropertyChanged += OnTaskPropertyChanged;
            }

            Tasks.CollectionChanged += OnTasksCollectionChanged;
        }

        private void AddTask()
        {
            var title = NewTaskTitle.Trim();
            if (string.IsNullOrWhiteSpace(title))
                return;

            Tasks.Add(new TaskItem { Title = title });
            NewTaskTitle = string.Empty;
        }

        private void RemoveTask(TaskItem? task)
        {
            if (task is not null)
                Tasks.Remove(task);
        }

        private void OnTasksCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems is not null)
            {
                foreach (TaskItem task in e.NewItems)
                    task.PropertyChanged += OnTaskPropertyChanged;
            }

            if (e.OldItems is not null)
            {
                foreach (TaskItem task in e.OldItems)
                    task.PropertyChanged -= OnTaskPropertyChanged;
            }

            _storage.Save(Tasks);
            OnPropertyChanged(nameof(TotalCount));
            OnPropertyChanged(nameof(CompletedCount));
            OnPropertyChanged(nameof(SummaryText));
        }

        private void OnTaskPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            _storage.Save(Tasks);

            if (e.PropertyName == nameof(TaskItem.IsCompleted))
            {
                OnPropertyChanged(nameof(CompletedCount));
                OnPropertyChanged(nameof(SummaryText));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Equals(field, value))
                return;

            field = value;
            OnPropertyChanged(propertyName);
        }
    }
}
