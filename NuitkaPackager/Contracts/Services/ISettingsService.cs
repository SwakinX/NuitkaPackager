using System.Runtime.CompilerServices;

namespace NuitkaPackager.Contracts.Services;

public interface ISettingsService
{
    static bool EnableIcon { get; set;}
    static string PyFilePath{ get; set; }
        
    Task<T> GetValueAsync<T>(T? value= default, [CallerMemberName] string? key =null);

    Task SetValueAsync<T>(T value, [CallerMemberName] string? key = null);
}
