using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzurePim.Pages;

public class IndexModel : PageModel
{
    private readonly CaeClaimsChallengeService _caeClaimsChallengeService;

    public IndexModel(CaeClaimsChallengeService caeClaimsChallengeService)
    {
        _caeClaimsChallengeService = caeClaimsChallengeService;
    }

    public IActionResult OnGet()
    {
        // if CAE claim missing in id token, the required claims challenge is returned
        var claimsChallenge = _caeClaimsChallengeService
            .CheckForRequiredAuthContextIdToken(AuthContextId.C4, HttpContext);

        if (claimsChallenge != null)
        {
            var properties = new AuthenticationProperties { RedirectUri = "/index" };

            properties.Items["claims"] = claimsChallenge;
            return Challenge(properties);
        }

        return Page();
    }
}