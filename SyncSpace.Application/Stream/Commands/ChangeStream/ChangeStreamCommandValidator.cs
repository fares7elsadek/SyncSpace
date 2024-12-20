using FluentValidation;

namespace SyncSpace.Application.Stream.Commands.ChangeStream;

public class ChangeStreamCommandValidator:AbstractValidator<ChangeStreamCommand>
{
    public ChangeStreamCommandValidator()
    {
        RuleFor(x => x.VideoUrl)
            .Must(LinkMustBeAUri)
            .WithMessage("Link '{PropertyValue}' must be a valid URI. eg: http://www.SomeWebSite.com");
    }
    private static bool LinkMustBeAUri(string link)
    {
        if (string.IsNullOrWhiteSpace(link))
        {
            return false;
        }
        Uri outUri;
        return Uri.TryCreate(link, UriKind.Absolute, out outUri)
               && (outUri.Scheme == Uri.UriSchemeHttp || outUri.Scheme == Uri.UriSchemeHttps);
    }
}
