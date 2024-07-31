using CommunityToolkit.Mvvm.ComponentModel;
using TestingDeepRefresh.Sources.Models;

namespace TestingDeepRefresh.Sources.ViewModels
{
  public partial class StringVM : ObservableObject
  {
    [ObservableProperty]
    string _name;

    public Guid ID { get; init; }

    public StringVM(string name, Guid id)
    {
      _name = name;
      ID=id;
    }

    public StringVM(string name)
  : this(name, Guid.NewGuid())
    { }

    public static StringVM Create(string name)
    {
      var poco = StringPOCO.Create(name);

      if (poco == default)
        return new StringVM(name);

      var mainPage = MainPage.Instance;
      if (mainPage != null)
      {
        mainPage.MyStringPOCOs.Add(poco);
      }

      return new StringVM(name, poco.ID);
    }

    public StringPOCO? GetAssociatedPOCO()
    {
      var mainPage = MainPage.Instance;
      if (mainPage == default)
        return default;

      return mainPage.MyStringPOCOs.FirstOrDefault(poco => poco.ID == ID);
    }

    public bool UpdateAssociatedPOCO()
    {
      var foundPOCO = GetAssociatedPOCO();
      if (foundPOCO?.DTO?.Data == default)
        return false;

      foundPOCO.DTO.Data = Name;
      return true;
    }
  }
}
