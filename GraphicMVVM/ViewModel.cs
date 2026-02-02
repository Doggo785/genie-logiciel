using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GraphicMVVM;

public class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected   void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    private string _inputText = string.Empty;
    private string _outputText = string.Empty;

    public string InputText
    {
        get => _inputText;
        set
        {
            if (_inputText != value)
            {
                _inputText = value;
                OnPropertyChanged();
            }
        }
    }

    public string OutputText
    {
        get => _outputText;
        set
        {
            if (_outputText != value)
            {
                _outputText = value;
                OnPropertyChanged();
            }
        }
    }
    public void ConvertToUpper()
    {
        if (!String.IsNullOrEmpty(InputText))
        {
            OutputText = InputText.ToUpper();
        }

    }
}