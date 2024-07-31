using CommunityToolkit.Mvvm.ComponentModel;

namespace TestingDeepRefresh.Sources.Models
{
  public partial class StringPOCO : ObservableObject
  {
    [ObservableProperty]
    StringDTO _dTO;

    public Guid ID { get; init; }

    public StringPOCO(StringDTO dto)
      : this(dto, Guid.NewGuid())
    { }

    public StringPOCO(StringDTO dto, Guid id)
    {
      DTO = dto;
      ID = id;
      dto.PropertyChanged += (sender, EventArgs) =>
      {
        OnPropertyChanged(nameof(DTO));
      };
    }

    public StringPOCO(string dtoAsString)
      : this(new StringDTO(dtoAsString))
    { }
    public static StringPOCO Create(string dtoAsString)
    {
      return new StringPOCO(dtoAsString);
    }
  }
}
