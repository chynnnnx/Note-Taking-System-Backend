using System;
using System.Linq;

namespace FirstAngular.Application.Common.Helpers
{
    public static class UpdateHelper
    {
     
        public static bool HasChanges(params (object? oldVal, object? newVal)[] values)
            => values.Any(v => !Equals(v.oldVal, v.newVal));

       
        public static string GetMessage(string entityName, bool hasChanges)
            => hasChanges
                ? $"{entityName} successfully updated."
                : $"No changes detected, {entityName} was not updated.";
    }
}
