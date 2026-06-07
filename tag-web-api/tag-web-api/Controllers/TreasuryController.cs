using Microsoft.AspNetCore.Mvc;
using TAGWEBAPI.Integrations.ModernTreasury;

namespace TAGWEBAPI.Controllers;

[ApiController]
[Route("api/treasury")]
public sealed class TreasuryController : ControllerBase
{
    private readonly IModernTreasuryLedgerService modernTreasuryLedgerService;

    public TreasuryController(IModernTreasuryLedgerService modernTreasuryLedgerService)
    {
        this.modernTreasuryLedgerService = modernTreasuryLedgerService;
    }

    [HttpPost("stripe-ledger")]
    public async Task<ActionResult<ModernTreasuryLedgerResponse>> PostStripeLedger(
        [FromBody] StripeLedgerPrototypeRequest request,
        CancellationToken cancellationToken)
    {
        if (!this.ModelState.IsValid)
        {
            return this.ValidationProblem(this.ModelState);
        }

        try
        {
            var response = await this.modernTreasuryLedgerService
                .PostStripeLedgerAsync(request, cancellationToken)
                .ConfigureAwait(false);

            return this.Ok(response);
        }
        catch (ArgumentException argumentException)
        {
            return this.BadRequest(argumentException.Message);
        }
        catch (InvalidOperationException invalidOperationException)
        {
            return this.Problem(detail: invalidOperationException.Message, statusCode: 500, title: "Modern Treasury integration error");
        }
    }

    [HttpPost("stripe-event")]
    public async Task<ActionResult<ModernTreasuryLedgerResponse>> PostStripeEvent(
        [FromBody] StripeEventAccountingRequest request,
        CancellationToken cancellationToken)
    {
        if (!this.ModelState.IsValid)
        {
            return this.ValidationProblem(this.ModelState);
        }

        try
        {
            var response = await this.modernTreasuryLedgerService
                .PostStripeEventAsync(request, cancellationToken)
                .ConfigureAwait(false);

            return this.Ok(response);
        }
        catch (ArgumentException argumentException)
        {
            return this.BadRequest(argumentException.Message);
        }
        catch (InvalidOperationException invalidOperationException)
        {
            return this.Problem(detail: invalidOperationException.Message, statusCode: 500, title: "Modern Treasury integration error");
        }
    }
}
