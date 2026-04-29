using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.Services
{
    public enum DialogType
    {
        InfoType,
        ErrorType,
        QuestionType
    }

    public enum DialogResult
    {
        Ok,
        Yes,
        No
    }

    public interface IDialogService
    {
        DialogResult ShowDialog(string message, string title, DialogType type);
    }
}
