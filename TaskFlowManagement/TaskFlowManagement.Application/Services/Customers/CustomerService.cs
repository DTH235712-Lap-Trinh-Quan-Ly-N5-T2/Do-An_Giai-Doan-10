using TaskFlowManagement.Core.Entities;
using TaskFlowManagement.Core.Interfaces;
using TaskFlowManagement.Core.Interfaces.Services;

namespace TaskFlowManagement.Core.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomerService(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public Task<List<Customer>> GetAllAsync() => _customerRepo.GetAllAsync();
        public Task<List<Customer>> SearchAsync(string keyword) => _customerRepo.SearchAsync(keyword);
        public Task<Customer?> GetWithProjectsAsync(int id) => _customerRepo.GetWithProjectsAsync(id);

        public async Task<(bool Success, string Message, Customer? Data)> CreateAsync(Customer customer)
        {
            try
            {
                await _customerRepo.AddAsync(customer);
                return (true, "Thêm khách hàng thành công.", customer);
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi thêm khách hàng: {ex.Message}", null);
            }
        }

        public async Task<(bool Success, string Message)> UpdateAsync(Customer customer)
        {
            try
            {
                await _customerRepo.UpdateAsync(customer);
                return (true, "Cập nhật khách hàng thành công.");
            }
            catch (Exception ex)
            {
                return (false, $"Lỗi cập nhật: {ex.Message}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            try
            {
                await _customerRepo.DeleteAsync(id);
                return (true, "Xóa khách hàng thành công.");
            }
            catch (Exception ex)
            {
                return (false, "Không thể xóa vì khách hàng còn dự án liên quan.\nChi tiết: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
