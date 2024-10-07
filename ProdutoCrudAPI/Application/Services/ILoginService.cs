using ProdutoCrudAPI.Application.Dtos;

namespace ProdutoCrudAPI.Application.Services
{
    public interface ILoginService
    {
        public string Login(LoginDto loginDto);
    }
}
