using Blueflix.Application.Entities;

namespace Blueflix.Application.Repositories.Contracts
{
    public interface IRentalRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imdbId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ValueTask<Rented> RentedMovies(string imdbId, CancellationToken cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imdbId"></param>
        /// <param name="clientId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ValueTask<Rented> RentMovie(string imdbId, Guid clientId, CancellationToken cancellationToken);
    }
}
