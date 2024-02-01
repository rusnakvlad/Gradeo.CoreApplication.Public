namespace Gradeo.CoreApplication.Application.Common.Interfaces;

public interface ICacheService
{
    
    TResult GetOrCreateUserScopeAsync<T, TResult>(string key, Func<T, TResult> valueFunc, T parameter);
    void Remove(string key);
}