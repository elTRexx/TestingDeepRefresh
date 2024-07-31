using CommunityToolkit.Mvvm.ComponentModel;

namespace TestingDeepRefresh.Sources.Models
{
  public partial class StringDTO : ObservableObject
  {
    [ObservableProperty]
    string _data;

    public StringDTO(string data)
    {
      _data = data;
    }
  }
}
