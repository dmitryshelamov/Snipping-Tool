using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SnippingTool.Annotations;

namespace SnippingTool.ViewModels
{
    public class HotKeyHelper : INotifyPropertyChanged
    {
        private ModifierKeys _mod1;
        private ModifierKeys _mod2;
        private Key _key;
        public ObservableCollection<ModifierKeys> ModifierKeyses1 { get; set; }
        public ObservableCollection<ModifierKeys> ModifierKeyses2 { get; set; }

        public HotKeyHelper()
        {
            ModifierKeyses1 = new ObservableCollection<ModifierKeys>()
            {
                ModifierKeys.None,
                ModifierKeys.Alt,
                ModifierKeys.Control,
                ModifierKeys.Shift
            };

            ModifierKeyses2 = new ObservableCollection<ModifierKeys>()
            {
                ModifierKeys.None,
                ModifierKeys.Alt,
                ModifierKeys.Control,
                ModifierKeys.Shift
            };
        }

        public Key Key
        {
            get { return _key; }
            set
            {
                _key = value;
                OnPropertyChanged();
            }
        }

        public ModifierKeys Mod1
        {
            get { return _mod1; }
            set
            {
                if (value != ModifierKeys.None)
                {
                    ModifierKeyses2.Remove(value);
                    if (_mod1 != ModifierKeys.None)
                        ModifierKeyses2.Add(_mod1);
                }
                else
                {
                    ModifierKeyses2.Add(_mod1);
                }
                _mod1 = value;
                OnPropertyChanged();
            }
        }

        public ModifierKeys Mod2
        {
            get { return _mod2; }
            set
            {
                if (value != ModifierKeys.None)
                {
                    ModifierKeyses1.Remove(value);
                    if (_mod2 != ModifierKeys.None)
                        ModifierKeyses1.Add(_mod2);
                }
                else
                {
                    ModifierKeyses1.Add(_mod2);
                }
                _mod2 = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
