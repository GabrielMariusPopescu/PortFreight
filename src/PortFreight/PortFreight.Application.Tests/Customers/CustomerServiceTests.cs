namespace PortFreight.Application.Tests.Customers;

[ExcludeFromCodeCoverage]
public class CustomerServiceTests
{
    private readonly Mock<IGenericRepository<Customer>> _repository;
    private readonly ICustomerService _service;

    private readonly Customer _customer = new RandomisedCustomerBuilder(seed:12345)
        .WithId(Guid.NewGuid())
        .WithName("Jon Doe")
        .WithEmail("administrator@portfreight.co.uk")
        .WithPhone("1234567890")
        .WithShipments([])
        .Build();

    public CustomerServiceTests()
    {
        _repository = new Mock<IGenericRepository<Customer>>();
        _service = new CustomerService(_repository.Object);
    }

    [Fact]
    public async Task GetAllCustomersAsyncReturnsAllCustomers()
    {
        // Arrange
        var customers = new List<Customer>
        {
            _customer,
            _customer,
            _customer
        };

        _repository
            .Setup(repository => repository.GetAllAsync())
            .ReturnsAsync(customers);

        // Act
        var result = await _service.GetAllCustomersAsync();

        // Assert
        result.Should().BeEquivalentTo(customers);
    }

    [Fact]
    public async Task GetAllCustomersAsyncReturnsNoCustomers()
    {
        // Arrange
        List<Customer> customers = [];
        if (customers == null)
        {
            throw new ArgumentNullException(nameof(customers));
        }

        _repository
            .Setup(repository => repository.GetAllAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _service.GetAllCustomersAsync();

        // Assert
        result.Should().BeEquivalentTo(customers);
    }

    [Fact]
    public async Task GetCustomerAsyncReturnsCustomerWhenExists()
    {
        // Arrange
        _repository
            .Setup(repository => repository.GetByIdAsync(_customer.Id))
            .ReturnsAsync(_customer);

        // Act
        var result = await _service.GetCustomerAsync(_customer.Id);

        // Assert
        result.Should().Be(_customer);
    }

    [Fact]
    public async Task GetCustomerAsyncReturnsNullWhenNotFound()
    {
        // Arrange
        _repository
            .Setup(repository => repository.GetByIdAsync(_customer.Id))
            .ReturnsAsync((Customer?)null);

        // Act
        var result = await _service.GetCustomerAsync(_customer.Id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateCustomerAsyncAddsCustomerAndReturnsIt()
    {
        // Arrange
       _repository
            .Setup(repository => repository.AddAsync(_customer))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateCustomerAsync(_customer);

        // Assert
        result.Should().Be(_customer);
        _repository.Verify(repository => repository.AddAsync(_customer), Times.Once);
    }

    [Fact]
    public async Task UpdateCustomerAsyncReturnsTrueWhenUpdateSucceeds()
    {
        // Arrange
       _repository
            .Setup(repository => repository.GetByIdAsync(_customer.Id))
            .ReturnsAsync(_customer);

        _repository
            .Setup(repository => repository.Update(_customer))
            .Returns(true);

        // Act
        var result = await _service.UpdateCustomerAsync(_customer);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCustomerAsyncReturnsFalseWhenUpdateFails()
    {
        // Arrange
        _repository
            .Setup(repository => repository.GetByIdAsync(_customer.Id))
            .ReturnsAsync(_customer);

        _repository
            .Setup(repository => repository.Update(_customer))
            .Returns(false);

        // Act
        var result = await _service.UpdateCustomerAsync(_customer);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteCustomerAsyncReturnsTrueWhenDeleteSucceeds()
    {
        // Arrange
        _repository
            .Setup(repository => repository.GetByIdAsync(_customer.Id))
            .ReturnsAsync(_customer);

        _repository
            .Setup(repository => repository.Delete(_customer))
            .Returns(true);

        // Act
        var result = await _service.DeleteCustomerAsync(_customer.Id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteCustomerAsyncReturnsFalseWhenDeleteDoesNotSucceeds()
    {
        // Arrange
        _repository
            .Setup(repository => repository.GetByIdAsync(_customer.Id))
            .ReturnsAsync(_customer);

        _repository
            .Setup(repository => repository.Delete(_customer))
            .Returns(false);

        // Act
        var result = await _service.DeleteCustomerAsync(_customer.Id);

        // Assert
        result.Should().BeFalse();
    }
}