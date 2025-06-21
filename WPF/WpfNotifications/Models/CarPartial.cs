using System.ComponentModel;

namespace WpfNotifications.Models;

public partial class Car : BaseEntity, IDataErrorInfo
{
    public string Error { get; }

    public string this[string columnName]
    {
        get
        {
            ClearErrors(columnName);
            switch (columnName)
            {
                case nameof(Id):
                    break;
                case nameof(Make):
                    CheckMakeAndColor();
                    if (Make == "ModelT")
                    {
                        AddError(nameof(Make), "Too Old");
                    }
                    break;
                case nameof(Color):
                    CheckMakeAndColor();
                    break;
                case nameof(PetName):
                    break;
            }

            return string.Empty;
        }
    }

    internal bool CheckMakeAndColor()
    {
        if (Make == "Chevy" && Color == "Pink")
        {
            AddError(nameof(Make), $"{Make}'s don't come in {Color}");
            AddError(nameof(Color), $"{Make}'s don't come in {Color}");
            return true;
        }

        return false;
    }
}
