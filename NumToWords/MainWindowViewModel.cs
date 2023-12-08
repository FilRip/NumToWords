using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NumToWords;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty()]
    private string _numPhone, _zeroValue, _oneValue;
    [ObservableProperty()]
    private ObservableCollection<string> _result = [];

    public MainWindowViewModel()
    {
        ZeroValue = "+";
        OneValue = "_";
    }

    [RelayCommand()]
    private async Task Generate()
    {
        Result.Clear();
        await Task.Delay(10);
        GenerateWordsRecursive(NumPhone, 0, "");
    }

    private readonly Dictionary<char, string> NumToChar = new()
    {
        {'2', "ABC"},
        {'3', "DEF"},
        {'4', "GHI"},
        {'5', "JKL"},
        {'6', "MNO"},
        {'7', "PQRS"},
        {'8', "TUV"},
        {'9', "WXYZ"},
    };

    private void GenerateWordsRecursive(string sequence, int index, string word)
    {
        if (index == sequence.Length)
            Result.Add(word);
        else
        {
            char number = sequence[index];
            string letters;
            if (number == '1')
                letters = OneValue;
            else if (number == '0')
                letters = ZeroValue;
            else
                letters = NumToChar[number];
            foreach (char letter in letters)
                GenerateWordsRecursive(sequence, index + 1, word + letter);
        }
    }
}
