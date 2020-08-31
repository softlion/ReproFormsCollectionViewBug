using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ReproApp
{
    public class ItemViewModel
    {
        public string Text { get; set; }
    }

    public class MainPageViewModel : INotifyPropertyChanged
    {
        private bool isListVisible;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ItemViewModel> Items { get; }
        public ICommand TestCommand { get; }

        public bool IsListVisible
        {
            get => isListVisible;
            set
            {
                isListVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsListVisible)));
            }
        }

        public MainPageViewModel()
        {
            Items = new ObservableCollection<ItemViewModel>();
            TestCommand = new Command(async () =>
            {
                var items = CreateItems(10, 0).ToList().First();
                var items2 = CreateItems(10, 0).ToList().Skip(1).First();

                for (var i = 0; i < 10000; i++)
                {
                    await Task.Delay(1);
                    Items.Add(items);
                    IsListVisible = true;

                    await Task.Delay(2);
                    Items.Add(items2);
                    IsListVisible = false;
                    await Task.Delay(2);

                    Items.Clear();
                    IsListVisible = true;
                    await Task.Delay(2);
                    
                    Items.Add(items);
                    Items.Add(items2);
                    await Task.Delay(2);
                }   

            });
        }

        private IEnumerable<ItemViewModel> CreateItems(int count, int batch)
        {
            var i = 0;
            while (count-- > 0)
                yield return new ItemViewModel { Text = $"Item {i++} Batch {batch}" };
        }
    }
}
