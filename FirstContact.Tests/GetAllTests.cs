using NSubstitute;
using NSubstitute.Core;


namespace FirstContact.FirstContact.Tests;

public class GetAllTest
{
    [Fact]
    public void GetAll_WhenCalled_Returnes_EveryContactCorrectlyMappedToTheResponse()
    {
        var repository = Substitute.For<IContactRepository>();
        repository.GetAll().Returns(new List<Contact>
        {
            new Contact("Bob") { Id = 1},
            new Contact("Frank") { Id = 2}
        });

        var service = new ContactService(repository);

        var result = service.GetAll().ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Bob", result[0].Name);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Frank", result[1].Name);

    }

    [Fact]
    public void GetAll_IfRepositoryReturnsEmptyListTheResponseisAlsoEmpty()
    {
        var repository = Substitute.For<IContactRepository>();
        repository.GetAll().Returns(new List<Contact>());

        var service = new ContactService(repository);

        var result = service.GetAll();

       Assert.Empty(result);
    }
}