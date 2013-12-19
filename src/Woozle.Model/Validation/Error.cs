namespace Woozle.Model.Validation
{
    public class Error
    {
        public Error(string field, string message)
        {
            this.Field = field;
            this.Message = message;
        }

        public string Field { get; set; }

        public string Message { get; set; }
    }
}
