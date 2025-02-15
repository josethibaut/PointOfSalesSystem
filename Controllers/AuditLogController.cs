using Microsoft.AspNetCore.Mvc;
namespace PointOfSalesSystem.Controllers 

{ 
    public class AuditLogController : Controller 

    { 
        public IActionResult ViewLogs() => View();

    } 

}