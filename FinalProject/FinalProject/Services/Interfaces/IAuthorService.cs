using FinalProject.Models;

namespace FinalProject.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAll();
        Task<Author> GetById(int? id);
    }
}
