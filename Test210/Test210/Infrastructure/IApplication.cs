
namespace Test210.Infrastructure
{
    public interface IApplication
    {
        (string FirstName, string LastName, string FullName) TestTuples(string firstName, string lastName);
    }
}
