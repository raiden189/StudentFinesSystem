
namespace StudentFinesSystem.Validations
{
    public interface IValidity
    {
        IEnumerable<string> Errors { get; }
        bool IsValid { get; }
        bool Validate();
    }
}