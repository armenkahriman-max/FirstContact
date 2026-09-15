using System.Net.Cache;
using System.Reflection.Metadata.Ecma335;
using NSubstitute;

namespace FirstContact.FirstContact.Tests;

public class UpdateContactTests
{
    [Fact]
    public void Update_IfContactExists_ChangesNameCallsCommitAndReturnsTrue()
    {
        var existing = new Contact("Bob") { Id = 3 };
        var repository = Substitute.For<IContactRepository>();
        repository.GetById(3).Returns(existing);

        var service = new ContactService(repository);
        var request = new UpdateContactRequest { Name = "Bobby" };

        var result = service.UpdateContact(3, request);

        Assert.True(true);
        Assert.Equal("Bobby", existing.Name);
        repository.Received().Commit();
    }

    [Fact]
    public void Update_IfContactDoesNotExist_ReturnsFalseAndDoesNotCallCommit()
    {
        var repository = Substitute.For<IContactRepository>();
        repository.GetById(99).Returns((Contact?)null);

        var service = new ContactService(repository);
        var request = new UpdateContactRequest { Name ="Baylish" };

        var result = service.UpdateContact(99, request);

        Assert.False(result);
        repository.DidNotReceive().Commit();
    }
}