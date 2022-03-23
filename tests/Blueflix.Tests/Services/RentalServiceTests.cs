using Xunit;
using Moq;
using Blueflix.Application.Services.Implementations;
using System.Threading.Tasks;
using Blueflix.Application.Repositories.Contracts;
using System;
using System.Threading;
using Blueflix.Application.Entities;
using System.Linq;

namespace Blueflix.Tests.Services
{
    public class RentalServiceTests
    {
        [Fact]
        public async Task Should_Rented_MovieAsync()
        {
            var rentedMock = BuildRented();
            var rentMock = BuildRentMovie();
            var clientId = rentedMock.ClientId;
            var imdbId = rentMock.ImdbId;

            var rentalRepository = new Mock<IRentalRepository>();

            rentalRepository.Setup(x => x.RentedMovies(rentedMock.ImdbId, CancellationToken.None))
                .ReturnsAsync(rentedMock);

            rentalRepository.Setup(x => x.RentMovie(rentedMock.ImdbId, rentedMock.ClientId, CancellationToken.None))
                .ReturnsAsync(rentMock);

            var rentalService = new RentalService(rentalRepository.Object);
            var rented = await rentalService.Rent(rentedMock.ImdbId, clientId, CancellationToken.None);

            Assert.Equal(rented.LeaseDate, DateTime.Today);
            Assert.Equal(rented.ReturnDate, DateTime.Today.AddDays(2));
            Assert.Equal(imdbId, rented.ImdbId);
        }

        [Fact]
        public async Task The_Movie_Is_Rented()
        {
            var rentedMock = BuildRented();
            rentedMock.ReturnDate = DateTime.Today;
            var clientId = rentedMock.ClientId;

            var rentalRepository = new Mock<IRentalRepository>();

            rentalRepository.Setup(x => x.RentedMovies(rentedMock.ImdbId, CancellationToken.None))
                .ReturnsAsync(rentedMock);

            var rentalService = new RentalService(rentalRepository.Object);
            var rented = await rentalService.Rent(rentedMock.ImdbId, clientId, CancellationToken.None);

            Assert.NotEmpty(rented.Notifications);
            Assert.Equal("The film is not available for rental", rented.Notifications.FirstOrDefault());
            Assert.True(rented.HasErros());
        }

        public Rented BuildRented()
        {
            var rented = new Rented();
            rented.ImdbId = Guid.NewGuid().ToString();
            rented.ClientId = Guid.NewGuid();
            rented.LeaseDate = DateTime.Now.AddDays(-2);
            rented.ReturnDate = DateTime.Now.AddDays(-1);
            return rented;
        }

        public Rented BuildRentMovie()
        {
            var rented = BuildRented();
            rented.LeaseDate = DateTime.Today;
            rented.ReturnDate = DateTime.Today.AddDays(2);
            return rented;
        }
    }
}
