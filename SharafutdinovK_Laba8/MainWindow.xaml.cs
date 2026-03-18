using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharafutdinovK_Laba8
{

    public partial class MainWindow : Window
    {
        private ObservableCollection<string> inventory = new ObservableCollection<string>();
        private ObservableCollection<string> history = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
            InventoryList.ItemsSource = inventory;
            HistoryList.ItemsSource = history;
            CommandInput.Focus();
        }

        private void CommandInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string input = CommandInput.Text.Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    ProcessCommand(input);
                }
                CommandInput.Clear();
            }
        }

        private void ProcessCommand(string input)
        {
            string[] parts = input.Split(new[] { ' ' }, 2);
            string command = parts[0].ToLower();
            string argument = parts.Length > 1 ? parts[1] : string.Empty;

            try
            {
                switch (command)
                {
                    case "add":
                        if (string.IsNullOrWhiteSpace(argument)) throw new Exception("Укажите предмет");
                        inventory.Insert(0, argument);
                        Log($"Добавлено: {argument}");
                        break;

                    case "use":
                        if (inventory.Count == 0) throw new Exception("Рюкзак пуст");
                        string usedItem = inventory[0];
                        inventory.RemoveAt(0);
                        Log($"Использовано: {usedItem}");
                        break;

                    case "top":
                        if (inventory.Count == 0) throw new Exception("Рюкзак пуст");
                        Log($"Следующий на очереди: {inventory[0]}");
                        break;

                    case "clear":
                        inventory.Clear();
                        Log("Инвентарь очищен");
                        break;

                    default:
                        Log($"Ошибка: неизвестная команда '{command}'");
                        break;
                }
            }
            catch (Exception ex)
            {
                Log($"Ошибка: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            history.Add($"[{DateTime.Now:HH:mm:ss}] {message}");
            HistoryList.ScrollIntoView(HistoryList.Items[HistoryList.Items.Count - 1]);
        }

    
    }
}
