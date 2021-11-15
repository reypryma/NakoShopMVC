using NakoShopMVC.Data.Base;
using NakoShopMVC.Models;
using NakoShopMVC.Data.ViewModels;
using System.Threading.Tasks;

namespace NakoShopMVC.Data.Services
{
    public interface IMoviesService:IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM data);
        Task UpdateMovieAsync(NewMovieVM data);
    }
}
