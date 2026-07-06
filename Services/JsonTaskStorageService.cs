using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WPF_TaskManager.Models;

namespace WPF_TaskManager.Services
{
    public class JsonTaskStorageService : ITaskStorageService
    {
        private readonly string _filePath;

        public JsonTaskStorageService()
            : this(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "WPF-TaskManager",
                "tasks.json"))
        {
        }

        public JsonTaskStorageService(string filePath)
        {
            _filePath = filePath;
        }

        public List<TaskItem> Load()
        {
            if (!File.Exists(_filePath))
                return new List<TaskItem>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        public void Save(IEnumerable<TaskItem> tasks)
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            var json = JsonSerializer.Serialize(tasks.ToList(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
