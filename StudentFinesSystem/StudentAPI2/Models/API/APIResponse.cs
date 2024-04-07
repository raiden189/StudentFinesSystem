namespace StudentAPI2.Models.API
{
    public class APIResponse<T>
    {
        public T Data { get; set; }

        public List<Error> ErrorList { get; set; }
    }

    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
