using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramInvitesGenerator.Services.Abstractions
{
    public interface IDocumentGenerator
    {
        Task<byte[]> GenerateFromStringsAsync(string[][] table);
        Task<byte[]> GenerateFromObjectsAsync<T>(IEnumerable<T> objects);
    }
}