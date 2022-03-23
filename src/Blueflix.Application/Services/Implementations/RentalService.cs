using Blueflix.Application.Entities;
using Blueflix.Application.Repositories.Contracts;
using Blueflix.Application.Services.Contracts;

namespace Blueflix.Application.Services.Implementations
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;
        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async ValueTask<Rented> Rent(string imdbId, Guid clientId, CancellationToken cancellation)
        {
            var rented = await _rentalRepository.RentedMovies(imdbId, cancellation);
            if (rented.AvailableForLease())
            {
                return await _rentalRepository.RentMovie(imdbId, clientId, cancellation);
            }
            return rented;
        }
    }
}
