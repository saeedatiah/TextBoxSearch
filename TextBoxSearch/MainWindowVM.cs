using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TextBoxSearch
{
    public class MainWindowVM :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Props

        public AsyncCommand NavToList { set; get; }
        public AsyncCommand NavToTextBox { set; get; }
        public AsyncCommand NavToNext { set; get; }
        public AsyncCommand TexetChanged { set; get; }
        public AsyncCommand SelelctedItem { set; get; }


        private bool foucuTextBox;
        public bool FoucuTextBox
        {
            get { return foucuTextBox; }
            set {
                foucuTextBox = value;
                if (foucuTextBox)
                    FoucuListbox = false;


                OnPropertyChanged(nameof(FoucuTextBox)); }
        } 
        private bool foucuListbox;
        public bool FoucuListbox
        {
            get { return foucuListbox; }
            set {
                foucuListbox = value;
               if(foucuListbox)
                    FoucuTextBox = false;
                OnPropertyChanged(nameof(FoucuListbox)); }
        }

        private bool openPop;

        public bool OpenPop
        {
            get { return openPop; }
            set { openPop = value; OnPropertyChanged(nameof(OpenPop)); }
        }

        private ObservableCollection<Items> items;

        public ObservableCollection<Items> Items
        {
            get { return items; }
            set { items = value; OnPropertyChanged(nameof(Items)); }
        }
        private ObservableCollection<Items> filteredItems;

        public ObservableCollection<Items> FilteredItems
        {
            get { return filteredItems; }
            set { filteredItems = value; OnPropertyChanged(nameof(FilteredItems)); }
        }

        private Items selectedItem;

        public Items SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; OnPropertyChanged(nameof(SelectedIndex)); }
        }

        private string txtSearchItem;

        public string TxtSearchItem
        {
            get { return txtSearchItem; }
            set { txtSearchItem = value; OnPropertyChanged(nameof(TxtSearchItem)); }
        } 






        #endregion

        public MainWindowVM()
        {
            NavToList = new AsyncCommand(() => NavToListAsync());
            NavToTextBox = new AsyncCommand(() => NavToTextBoxAsync());
            NavToNext=new AsyncCommand(()=>NavToNextAsync());
            TexetChanged = new AsyncCommand(()=> TexetChangedAsync());
            SelelctedItem = new AsyncCommand(()=> SelelctedItemAsync());
            Items = new ObservableCollection<Items>()
            {
                new Items{Code = "112" ,Name="aa" },
                new Items{Code = "155" ,Name="bb" },
                new Items{Code = "1775" ,Name="cc" },
                new Items{Code = "140" ,Name="d" },
                new Items{Code = "140" ,Name="ee" },
                new Items{Code = "987" ,Name="vv" },
                new Items{Code = "987" ,Name="ae" },
                new Items{Code = "987" ,Name="asdd" },
            };

            FilteredItems = Items;
        }
        async Task NavToListAsync()
        {
            OpenPop = true;
            FoucuListbox = true;
            if (SelectedItem == null)
                return;
            TxtSearchItem = SelectedItem.Name;
        }
        async Task NavToTextBoxAsync()
        {
            if(SelectedIndex == 0)
                FoucuTextBox = true;
        }
        async Task NavToNextAsync()
        {
            OpenPop = false;
        }
        async Task TexetChangedAsync()
        {
            FilteredItems = new ObservableCollection<Items>(Items.Where(i=>i.Name.ToLower().Contains(TxtSearchItem)|| i.Code.ToLower().Contains(TxtSearchItem)).ToList());
        }
        async Task SelelctedItemAsync()
        {
            SelectedItem = Items.Where(i => i.Name.ToLower().Contains(TxtSearchItem) || i.Code.ToLower().Contains(TxtSearchItem)).FirstOrDefault() ?? Items.FirstOrDefault();
            TxtSearchItem = SelectedItem.Code+" - "+ SelectedItem.Name;
        }



    }

    public class Items
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Code +" - "+Name;
        }
    }
}
