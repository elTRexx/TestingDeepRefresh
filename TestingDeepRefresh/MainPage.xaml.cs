using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TestingDeepRefresh.Sources.Models;
using TestingDeepRefresh.Sources.Utilities;
using TestingDeepRefresh.Sources.ViewModels;

namespace TestingDeepRefresh
{
  public partial class MainPage : ContentPage
  {
    private DeepObservableCollection<StringPOCO> _myStringPOCOs = new();
    public DeepObservableCollection<StringPOCO> MyStringPOCOs => _myStringPOCOs;

    private ObservableCollection<StringVM> _myStringVMs = new();
    public ObservableCollection<StringVM> MyStringVMs => _myStringVMs;

    private static MainPage? _instance;
    public static MainPage? Instance => _instance;

    public ICommand UpdatingCommand => new Command(Update);
    public ICommand EditStringVMCommand => new Command<StringVM>(EditStringVM);

    public MainPage()
    {
      InitializeComponent();

      BindingContext = this;

      _instance = this;

      _PopulateMyStringVMs();
    }

    private void _PopulateMyStringVMs()
    {
      Trace.WriteLine($"Calling {nameof(_PopulateMyStringVMs)}...");

      _myStringVMs.Clear();
      _myStringVMs.Add(StringVM.Create("One"));
      _myStringVMs.Add(StringVM.Create("Two"));
      _myStringVMs.Add(StringVM.Create("Three"));
    }

    private void Update(object o)
    {
      Trace.WriteLine($"Calling {nameof(Update)}...");

      foreach (var stringVM in _myStringVMs)
      {
        stringVM.UpdateAssociatedPOCO();
      }

      foreach (var stringPOCO in _myStringPOCOs)
        Trace.WriteLine(stringPOCO.DTO?.Data);
    }

    private void HardUpdate(object o)
    {
      Trace.WriteLine($"Calling {nameof(HardUpdate)}...");

      _myStringPOCOs.Clear();
      foreach (var stringVM in _myStringVMs)
      {
        _myStringPOCOs.Add(new StringPOCO(stringVM.Name));
      }
    }

    private void EditStringVM(StringVM stringVM)
    {
      Trace.WriteLine($"Calling {nameof(EditStringVM)}...");

      stringVM.UpdateAssociatedPOCO();
    }
  }

}
