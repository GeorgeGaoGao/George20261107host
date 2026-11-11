using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Core.Extension
{
    public static class DialogServiceExtension
    {
        public static void ShowDialog<T>(this IDialogService dialogService,Action<IDialogResult> callback,DialogParameters param = null)
        {
            dialogService.ShowDialog(typeof(T).Name, param ?? new DialogParameters(), callback);
        }
    }
}
