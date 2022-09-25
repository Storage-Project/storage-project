using storage_app.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace storage_app.ViewModels
{
    internal class SearchActionViewModel : ViewModelBase
    {
        private Searcher? _filteredSerchCommand;
        public Searcher FilteredSerchCommand
        {
            get
            {
                if (_filteredSerchCommand == null)
                    _filteredSerchCommand = new Searcher(_ => {  });
                return _filteredSerchCommand;
            }
            set
            {
                _filteredSerchCommand = value;
            }
        }
    }

    internal class Searcher : CommandExecutor
    {
        public Searcher(Action<object?> action) : base(action) { }
    }
}
