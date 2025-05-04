using BaseLibrary.Responses;

namespace Client.Library.Services.Contracts
{
    public interface IGenericServieInterface<T>
    {
        Task<List<T>> GetAll(string baseUrl);
        Task<T> GetById(int id, string baseUrl);
        Task<GeneralResponse> Insert(T item , string baseUrl);
        Task<GeneralResponse> Update(T item , string baseUrl);
        Task<GeneralResponse> DeleteById(int id,string baseUrl);
    }
}
