using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace storage_app.Utils
{
    public class ShowMessage
    {
        public static void DefaultMessage(string message)
        {
            MessageBox.Show(message, "Alert", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        public static void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);
        }

        public static bool QuestionMessage(string message)
        {
            MessageBoxResult result = MessageBox.Show(message, "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            return result == MessageBoxResult.OK;
        }
    }
}
