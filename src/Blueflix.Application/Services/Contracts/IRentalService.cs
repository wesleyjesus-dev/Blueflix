using Blueflix.Application.Entities;

namespace Blueflix.Application.Services.Contracts
{
    public interface IRentalService
    {
        public ValueTask<Rented> Rent(string imdbId, Guid clientId, CancellationToken cancellation);
    }
}
