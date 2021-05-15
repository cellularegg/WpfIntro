using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfIntro.BusinessLayer;
using WpfIntro.Models;

namespace WpfIntro.ViewModels
{
    public class MediaFolderVM : ViewModelBase
    {
        private ICommand _searchCommand;
        private ICommand _clearCommand;
        private ICommand _randomGenerateItemCommand;
        private ICommand _randomGenerateLogCommand;

        private MediaItem _currentItem;
        private IWpfIntroFactory _wpfIntroFactory;
        private string _searchName;

        public ICommand SearchCommand => _searchCommand ?? new RelayCommand(Search);
        public ICommand ClearCommand => _clearCommand ?? new RelayCommand(Clear);
        public ICommand RandomGenerateItemCommand => _randomGenerateItemCommand ?? new RelayCommand(RandomGenerateItem);
        public ICommand RandomGenerateLogCommand => _randomGenerateLogCommand ?? new RelayCommand(RandomGenerateLog);

        private void RandomGenerateItem(object commandParameter)
        {
            MediaItem generatedItem = _wpfIntroFactory.CreateItem(NameGenerator.GenerateName(6),
                NameGenerator.GenerateName(15), DateTime.Now);
            Items.Add(generatedItem);
        }

        private void RandomGenerateLog(object commandParameter)
        {
            MediaLog generatedLog = _wpfIntroFactory.CreateItemLog(NameGenerator.GenerateName(25), _currentItem);
        }

        public ObservableCollection<MediaItem> Items { get; set; }

        public MediaItem CurrentItem
        {
            get { return _currentItem; }
            set
            {
                if ((_currentItem != value) && (value != null))
                {
                    _currentItem = value;
                    RaisePropertyChangedEvent(nameof(CurrentItem));
                }
            }
        }

        public string SearchName
        {
            get { return _searchName; }
            set
            {
                if (_searchName != value)
                {
                    _searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName));
                }
            }
        }

        public MediaFolderVM()
        {
            this._wpfIntroFactory = WpfIntroFactory.GetInstance();
            InitListBox();
        }

        private void InitListBox()
        {
            Items = new ObservableCollection<MediaItem>();
            FillListBox();
        }

        private void FillListBox()
        {
            foreach (MediaItem mediaItem in _wpfIntroFactory.GetItems())
            {
                Items.Add(mediaItem);
            }
        }

        private void Search(object commandParameter)
        {
            IEnumerable foundItems = this._wpfIntroFactory.Search(SearchName);
            Items.Clear();
            foreach (MediaItem foundItem in foundItems)
            {
                Items.Add(foundItem);
            }
        }


        private void Clear(object commandParameter)
        {
            Items.Clear();
            SearchName = "";
            FillListBox();
        }
    }
}