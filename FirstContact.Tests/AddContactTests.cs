

using NSubstitute;

namespace FirstContact.FirstContact.Tests;

public class AddContactTest
{
    [Fact]
    public void AddContact_WhenCalled_PassesContactWithRequestedNameToRepository()
    {
        var repository = Substitute.For<IContactRepository>();
        var service = new ContactService(repository);
        var request = new CreateContactRequest { Name = "Ada" };

        service.AddContact(request);

        repository.Received(1).Add(Arg.Is<Contact>(c => c.Name == "Ada"));

    }

    [Fact]
    public void AddContact_WhenRepositoryAssignsId_ReturnIdAndName()
    {
        var repository = Substitute.For<IContactRepository>();
        repository
        .When(r => r.Add(Arg.Any<Contact>()))
        .Do(call => call.Arg<Contact>().Id = 42);

        var service = new ContactService(repository);
        var request = new CreateContactRequest { Name = "Ada" };

        CreateContactResponse result = service.AddContact(request);

        Assert.Equal(42, result.Id);
        Assert.Equal("Ada", result.Name);
    }


}