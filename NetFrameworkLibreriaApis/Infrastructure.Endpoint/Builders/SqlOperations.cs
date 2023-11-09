namespace Infrastructure.Endpoint.Builders
{
    public class SqlOperations
    {
        public enum SqlWriteOperation
        {
            Create,
            Update,
            Delete
        }

        public enum SqlReadOperation
        {
            Select, 
            SelectById 
        }
    }
}
