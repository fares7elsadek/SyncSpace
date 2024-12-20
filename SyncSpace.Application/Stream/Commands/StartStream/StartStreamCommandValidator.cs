

using FluentValidation;

namespace SyncSpace.Application.Stream.Commands.StartStream;

public class StartStreamCommandValidator:AbstractValidator<StartStreamCommand>
{
    public StartStreamCommandValidator()
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
