namespace ECommerceAPI.Application.Features.Queries.Role.GetRoles
{
    public class GetRolesQueryResponse
    {
        public IDictionary<string, string>  Datas { get; set; }
        public int TotalRoleCount { get; set; }
    }
}