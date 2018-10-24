using System.Windows;
using PointsClient.Helpers.Interface;

namespace PointsClient.Helpers
{
    public class DialogService : DialogServiceInterface
    {
        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
