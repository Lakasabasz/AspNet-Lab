namespace Podstawy_widoków.Areas.Admin.DTOs;

public class UpdateRoleRequest
{
    public string UserId { get; set; }
    public bool User { get; set; }
    public bool Operator { get; set; }
    public bool Admin { get; set; }
}