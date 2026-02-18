namespace PortFreight.Application.Tests;

public class CustomerServiceTests
{
    private readonly Mock<IGenericRepository<Customer>> _repository;
    private readonly ICustomerService _service;

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
            new() { Id = Guid.NewGuid(), Name = "A", Email = "a@a.com", Phone = "111" },
            new() { Id = Guid.NewGuid(), Name = "B", Email = "b@b.com", Phone = "222" }
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
        var id = Guid.NewGuid();
        var expected = new Customer
        {
            Id = id,
            Name = "Gabriel",
            Email = "gabriel@example.com",
            Phone = "123456",
            Shipments = []
        };

        _repository
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(expected);

        // Act
        var result = await _service.GetCustomerAsync(id);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public async Task GetCustomerAsyncReturnsNullWhenNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        _repository
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync((Customer?)null);

        // Act
        var result = await _service.GetCustomerAsync(id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateCustomerAsyncAddsCustomerAndReturnsIt()
    {
        // Arrange
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = "New Customer",
            Email = "new@example.com",
            Phone = "555"
        };

        _repository
            .Setup(repository => repository.AddAsync(customer))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _service.CreateCustomerAsync(customer);

        // Assert
        result.Should().Be(customer);
        _repository.Verify(repository => repository.AddAsync(customer), Times.Once);
    }

    [Fact]
    public async Task UpdateCustomerAsyncReturnsTrueWhenUpdateSucceeds()
    {
        // Arrange
        var id = Guid.NewGuid();
        var customer = new Customer
        {
            Id = id,
            Name = "Updated",
            Email = "u@example.com",
            Phone = "999"
        };

        _repository
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(customer);

        _repository
            .Setup(repository => repository.Update(customer))
            .Returns(true);

        // Act
        var result = await _service.UpdateCustomerAsync(customer);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCustomerAsyncReturnsFalseWhenUpdateFails()
    {
        // Arrange
        var id = Guid.NewGuid();
        var customer = new Customer
        {
            Id = id,
            Name = "Updated",
            Email = "u@example.com",
            Phone = "999"
        };

        _repository
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(customer);

        _repository
            .Setup(repository => repository.Update(customer))
            .Returns(false);

        // Act
        var result = await _service.UpdateCustomerAsync(customer);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteCustomerAsyncReturnsTrueWhenDeleteSucceeds()
    {
        // Arrange
        var id = Guid.NewGuid();
        var customer = new Customer
        {
            Id = id,
            Name = "Updated",
            Email = "u@example.com",
            Phone = "999"
        };

        _repository
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(customer);

        _repository
            .Setup(repository => repository.Delete(customer))
            .Returns(true);

        // Act
        var result = await _service.DeleteCustomerAsync(id);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteCustomerAsyncReturnsFalseWhenDeleteDoesNotSucceeds()
    {
        // Arrange
        var id = Guid.NewGuid();
        var customer = new Customer
        {
            Id = id,
            Name = "Updated",
            Email = "u@example.com",
            Phone = "999"
        };

        _repository
            .Setup(repository => repository.GetByIdAsync(id))
            .ReturnsAsync(customer);

        _repository
            .Setup(repository => repository.Delete(customer))
            .Returns(false);

        // Act
        var result = await _service.DeleteCustomerAsync(id);

        // Assert
        result.Should().BeFalse();
    }
}