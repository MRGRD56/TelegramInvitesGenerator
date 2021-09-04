using System;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;

namespace TelegramInvitesGenerator.Extensions
{
    public static class TelegramApi
    {
        public static async Task ExecuteAsync(Task method, Action<Exception> onException = null)
        {
            await ExecuteAsync(() => method, onException);
        }
        
        public static async Task ExecuteAsync(Func<Task> method, Action<Exception> onException = null)
        {
            try
            {
                await method();
            }
            catch (ApiRequestException exception)
            {
                onException?.Invoke(exception);
                
                if (exception.ErrorCode == 429)
                {
                    var delaySeconds = exception.Parameters.RetryAfter;
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                    await ExecuteAsync(method);
                }
            }
        }
        
        public static async Task<T> ExecuteAsync<T>(Task<T> method, Action<Exception> onException = null)
        {
            return await ExecuteAsync(() => method, onException);
        }
        
        public static async Task<T> ExecuteAsync<T>(Func<Task<T>> method, Action<Exception> onException = null)
        {
            T result = default;
            
            try
            {
                result = await method();
            }
            catch (ApiRequestException exception)
            {
                onException?.Invoke(exception);
                
                if (exception.ErrorCode == 429)
                {
                    var delaySeconds = exception.Parameters.RetryAfter;
                    await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
                    await ExecuteAsync(method);
                }
            }

            return result;
        }
    }
}