using System.Collections;
using System.ComponentModel;

namespace WpfNotifications.Models;

public class BaseEntity : INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errors = new();
    public bool HasErrors => _errors.Count > 0;
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            return _errors.Values;
        }

        return _errors.TryGetValue(propertyName, out var errors) ? errors : null;
    }

    private void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    protected void AddError(string propertyName, string error)
    {
        AddErrors(propertyName, [error]);
    }

    private void AddErrors(string propertyName, List<string> errors)
    {
        if (errors is null || errors.Count == 0)
        {
            return;
        }

        var changed = false;
        if (!_errors.ContainsKey(propertyName))
        {
            _errors.Add(propertyName, []);
            changed = true;
        }

        foreach (var error in errors)
        {
            if (_errors[propertyName].Contains(error)) continue;
            _errors[propertyName].Add(error);
            changed = true;
        }

        if (changed)
        {
            OnErrorsChanged(propertyName);
        }
    }

    protected void ClearErrors(string propertyName = "")
    {
        if (string.IsNullOrEmpty(propertyName))
        {
            _errors.Clear();
        }
        else
        {
            _errors.Remove(propertyName);
        }

        OnErrorsChanged(propertyName);
    }
}
