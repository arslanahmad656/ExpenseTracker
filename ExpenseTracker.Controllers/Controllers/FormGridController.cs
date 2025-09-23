using ExpenseTracker.Contracts.Logging.Generic;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Controllers.RouteNames;
using ExpenseTracker.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers.Controllers;

[ApiController]
[Route(FormGridRoutes.Base)]
[Authorize]
public class FormGridController
(
    ILoggerManager<FormGridController> logger,
    IServiceManager serviceManager
)
: ControllerBase
{
    [HttpPost(FormGridRoutes.GetFormGridData)]
    public async Task<IActionResult> GetGridFormData
    (
        int pageNumber = 0,
        string? orderBy = null,
        int itemsPerPage = 0,
        [FromBody] IEnumerable<SearchFilter>? filters = null
    )
    {
        logger.LogInfo("Filtering for form grid {pageNumber} {orderBy} {filters}", pageNumber, orderBy ?? "", filters ?? []);

        var (Entries, Total) = await serviceManager.FormService.Search(pageNumber, itemsPerPage, orderBy, filters).ConfigureAwait(false);

        return Ok(new
        {
            totalCount = Total,
            items = Entries
        });
    }


    [HttpGet(FormGridRoutes.GetStructure)]
    public IActionResult GridStructure()
    {
        var columns = new List<GridColumn>
        {
            new("formId", "Form ID", false, false),
            new("trackingId", "Tracking ID", true, false),
            new("title", "Title", true, true),
            new("currencyCode", "Currency", false, true),
            new("amount", "Amount", false, true),
            new("status", "Status", false, true),
            new("actions", "Actions", false, false),
        };

        return Ok(columns.Select(c => new
        {
            key = c.Key,
            displayName = c.DisplayName,
            isSortable = c.IsSortable,
            isSearchable = c.IsSearchable,
        }));
    }
}

file record GridColumn(string Key, string DisplayName, bool IsSearchable, bool IsSortable);