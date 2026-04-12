using TaskFlowManagement.Core.Entities;

namespace TaskFlowManagement.Core.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();
        Task<List<Customer>> SearchAsync(string keyword);
        Task<Customer?> GetWithProjectsAsync(int id);
        Task<(bool Success, string Message, Customer? Data)> CreateAsync(Customer customer);
        Task<(bool Success, string Message)> UpdateAsync(Customer customer);
        Task<(bool Success, string Message)> DeleteAsync(int id);
    }
}
