using FluentValidation.Results;

namespace AspNetBase.Extensions {

    public static class FluentValidationExts {

        public static string ExtractMessages(this IEnumerable<ValidationFailure> failures) {
            return string.Join(Environment.NewLine, failures.Select(x => x.ErrorMessage));
        }

    }
}