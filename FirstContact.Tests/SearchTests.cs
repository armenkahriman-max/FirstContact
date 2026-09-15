using NSubstitute;

namespace FirstContact.FirstContact.Tests;

public class SearchTests
{
    [Fact]
    public void SearchWhenCalled_PassesExactSearchTermToRepository()
    {
        var repository = Substitute.For<IContactRepository>();
        repository.Search(Arg.Any<string>()).Returns(new List<Contact>());
        var service = new ContactService(repository);

        service.Search("Bob");

        repository.Received(1).Search("Bob");
    }

    [Fact]
    public void Search_WhenRepositoryFindsContacts_MapsEveryContactToResponse()
    {
        var repository = Substitute.For<IContactRepository>();
        repository.Search("Bob").Returns(new List<Contact>
        {
            new Contact("Bob Lance")  { Id = 1}
        });

        var service = new ContactService(repository);

        var result = service.Search("Bob").ToList();

        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Bob Lance", result[0].Name);

    }
    [Fact]
    public void SearchWhenCalled_If_NoContactsAreFoundTheResponseIsEmpty()
    {
        var repository = Substitute.For<IContactRepository>();
        repository.Search(Arg.Any<String>()).Returns(new List<Contact>());

        var service = new ContactService(repository);

        var result = service.Search("");

        Assert.Empty(result);
    }

}