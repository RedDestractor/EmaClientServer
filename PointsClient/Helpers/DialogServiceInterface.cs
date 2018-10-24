using System.Windows;

namespace PointsClient.Helpers.Interface
{
    public interface DialogServiceInterface
    {
        MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
    }
}
