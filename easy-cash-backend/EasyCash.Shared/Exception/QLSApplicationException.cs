

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using FluentValidation.Results;

namespace EasyCash.Shared.Exceptions
{
    public class EasyCashException : Exception
    {
        public string StatusCode { get; }
        public string StatusMessage { get; set; }
        [Display(Name = "Message")]
        public string MainMessage { get; set; }
        public IDictionary<string, string[]> ValidationErrors { get; }


        public string FormattedError { get; }


        protected EasyCashException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        public EasyCashException(string message) : base(message)
        {
            StatusCode = StatusCodes.INVALID_REQUEST;
            StatusMessage = message;
            MainMessage = message;
        }


        public EasyCashException(List<ValidationFailure> failures)
            : this("One or more validation failures have occurred.")
        {
            ValidationErrors = new Dictionary<string, string[]>();
            IEnumerable<string> propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (string propertyName in propertyNames)
            {
                string[] propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                FormattedError += String.Join(",", failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)) + ", ";

                ValidationErrors.Add(propertyName, propertyFailures);
            }
        }


    }

}
